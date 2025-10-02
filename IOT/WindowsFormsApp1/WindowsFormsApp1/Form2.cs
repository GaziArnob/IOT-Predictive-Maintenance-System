using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {

        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";
        int id;
        public Form2()
        {
            InitializeComponent();
            LoadSensorData();
        }

        public Form2(int i)
        {
            id = i;
            InitializeComponent();
            LoadSensorData();
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

        private void LoadSensorData()
        {
            string query = "SELECT SensorID, SensorName, SensorType, Location, InstalledDate, MachineID FROM Sensor";
            FillDataGridView(query);
        }

        

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Please enter a search term.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"
               SELECT SensorID, SensorName, SensorType, Location, InstalledDate, MachineID
FROM Sensor
WHERE CAST(SensorID AS NVARCHAR) LIKE @searchTerm
   OR SensorName LIKE @searchTerm
   OR SensorType LIKE @searchTerm
   OR Location LIKE @searchTerm
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sensor_Upadte sensor_Upadte = new Sensor_Upadte();
            sensor_Upadte.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            LoadSensorData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBase dataBase = new DataBase();
            dataBase.Show();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Machine m1 = new Machine();
            m1.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Maintain maintain = new Maintain();
              
            maintain.Show();
        }

        private void button4_Click(object sender, EventArgs e)
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
    }
}
