using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Local_Database : Form
    {
        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";
        int selectedWorkerId = 0;

        public Local_Database()
        {
            InitializeComponent();
        }

        private void Local_Database_Load(object sender, EventArgs e)
        {
            LoadWorkers();
        }

        
        private void LoadWorkers()
        {
            string query = "SELECT Id, Name, Age, Address, Email, Phone, Gender, DateOfBirth FROM Users";
            FillDataGridView(query);
        }

        private void FillDataGridView(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dt;
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

            string query = @"SELECT Id, Name, Age, Address, Email, Phone, Gender, DateOfBirth 
                             FROM Users
                             WHERE CAST(Id AS NVARCHAR) LIKE @searchTerm 
                                OR Name LIKE @searchTerm
                                OR Email LIKE @searchTerm";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@searchTerm", "%" + searchValue + "%");
                con.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                    MessageBox.Show("No matching rows found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            LoadWorkers();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["Id"].Value != null && int.TryParse(row.Cells["Id"].Value.ToString(), out int id))
                {
                    selectedWorkerId = id;
                }
                else
                {
                    selectedWorkerId = 0;
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
                string query = "DELETE FROM Users WHERE Id = @Id";

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", selectedWorkerId);
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Update d1 = new Update();
            d1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBase d1 = new DataBase();
            d1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBase d1 = new DataBase();
            d1.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Machine m1 = new Machine();
            m1.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Maintain maintain = new Maintain();
            maintain.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            dataGridView1_CellClick(sender, e);
        }


        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }
    }
}
