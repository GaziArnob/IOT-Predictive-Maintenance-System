using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class workers : Form
    {
        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";
        int selectedWorkerId = 0;

        public workers()
        {
            InitializeComponent();
            this.Load += Workers_Load;
            this.dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void Workers_Load(object sender, EventArgs e)
        {
            LoadWorkers();
        }

        private void LoadWorkers()
        {
            string query = "SELECT * FROM Workers";
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dt;
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["WorkerId"].Value != null)
                {
                    selectedWorkerId = Convert.ToInt32(row.Cells["WorkerId"].Value);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedWorkerId == 0)
            {
                MessageBox.Show("Please select a row first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this profile?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                string query = "DELETE FROM Workers WHERE WorkerId = @WorkerId"; 

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@WorkerId", selectedWorkerId); 
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Profile deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        selectedWorkerId = 0;
                        LoadWorkers();
                    }
                    else
                    {
                        MessageBox.Show("No profile was found to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Please enter a search term.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"
                SELECT * FROM Workers 
                WHERE CAST(WorkerId AS NVARCHAR) LIKE @searchTerm";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchValue + "%");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    dataGridView1.DataSource = dataTable;

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No matching rows found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            LoadWorkers();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new DataBase().Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Maintain().Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Machine().Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form1().Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Home().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            worker_update w1 = new worker_update();
            w1.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            this.Hide();
            Form3 f1 = new Form3();
            f1.Show();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: can be used for future features
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Maintain m1 = new Maintain();
            m1.Show();
        }
    }
}
