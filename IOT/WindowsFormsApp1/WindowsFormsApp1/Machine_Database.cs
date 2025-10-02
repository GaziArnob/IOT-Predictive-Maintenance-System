using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Machine_Database : Form
    {
        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";
        int id;

        public Machine_Database()
        {
            InitializeComponent();
            LoadMachineData(); 
        }

        public Machine_Database(int i)
        {
            id = i;
            InitializeComponent();
            LoadMachineData();
        }

        private void LoadMachineData()
        {
            string query = "SELECT Machineid, Machinename, Power, Force, ControlSystem, Fuel, Output FROM Machine";
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

        private void Machine_Database_Load(object sender, EventArgs e)
        {
            LoadMachineData();
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
                SELECT MachineID, MachineName, Power, Force, ControlSystem, Fuel, Output 
                FROM Machine
                WHERE CAST(MachineID AS NVARCHAR) LIKE @searchTerm
                   OR MachineName LIKE @searchTerm";

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
            LoadMachineData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Machine_updatecs mu = new Machine_updatecs();
            mu.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBase dataBase = new DataBase();
            dataBase.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBase dataBase = new DataBase();
            dataBase.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Machine md = new Machine();
            md.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home h1 = new Home();
            h1.Show();
        }

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void button6_Click(object sender, EventArgs e) {
            this.Hide();
            Maintain maintain = new Maintain();
            maintain.Show();
        }
        private void button7_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { 
         }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void Machine_Database_Load_1(object sender, EventArgs e) { }
    }
}
