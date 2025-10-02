using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Work_order : Form
    {
        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";
        int id;

        public Work_order()
        {
            InitializeComponent();
            LoadWorkOrders();
        }

        public Work_order(int i)
        {
            id = i;
            InitializeComponent();
            LoadWorkOrders();
        }

        private void Work_order_Load(object sender, EventArgs e)
        {
            LoadWorkOrders();
        }

        private void LoadWorkOrders()
        {
            string query = "SELECT * FROM Work_Order";
            FillDataGridView(query);
        }

        private void FillDataGridView(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                dataGridView1.DataSource = dataTable;
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
                SELECT * 
                FROM Work_Order
                WHERE CAST(OrderID AS NVARCHAR) LIKE @searchTerm
                  ";

            using (SqlConnection connection = new SqlConnection(connectionString))
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

        private void button11_Click(object sender, EventArgs e)
        {
            LoadWorkOrders();
        }

        // Navigation Buttons (placeholders, connect to your actual forms)
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new DataBase().Show();
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Work_order().Show(); // Or another relevant form
        }

        // Empty event handlers (optional: safe to remove if unused)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void button6_Click(object sender, EventArgs e) { }
        private void button7_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }

        private void button11_Click_1(object sender, EventArgs e)
        {
            LoadWorkOrders();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Work_order_update w1 = new Work_order_update();
            w1.Show();

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Home h1 = new Home();
            h1.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Machine m1 = new Machine();
            m1.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Maintain maintain = new Maintain();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            DataBase dataBase = new DataBase();
            dataBase.Show();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
           this.Hide();
            Maintain maintain1 = new Maintain();
            maintain1.Show();
        }
    }
}
