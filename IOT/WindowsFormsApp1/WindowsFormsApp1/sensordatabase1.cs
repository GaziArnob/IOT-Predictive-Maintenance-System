using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class sensordatabase1 : Form
    {
        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";
        int id;

        public sensordatabase1()
        {
            InitializeComponent();
            // Hook the Load event correctly
            this.Load += sensordatabase1_Load;
        }

        // Correct Load event handler
        private void sensordatabase1_Load(object sender, EventArgs e)
        {
            LoadSensorData();
        }

        private void LoadSensorData()
        {
            string query = "SELECT SensorId, SensorName, SensorType, Unit, Value, Status FROM Sensor";
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
                SELECT SensorId, SensorName, SensorType, Unit, Value, Status
                FROM Sensor
                WHERE CAST(SensorId AS NVARCHAR) LIKE @searchTerm
                   OR SensorName LIKE @searchTerm
                   OR SensorType LIKE @searchTerm
                   OR Unit LIKE @searchTerm";

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

        }
    }
}
