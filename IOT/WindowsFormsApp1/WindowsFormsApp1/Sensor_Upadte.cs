using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Sensor_Upadte : Form
    {
        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";

        public Sensor_Upadte()
        {
            InitializeComponent();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string id= idtext1.Text.Trim();
            // Get values from textboxes
            if (!int.TryParse(idtext1.Text.Trim(), out int sensorId))
            {
                MessageBox.Show("Please enter a valid Sensor ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newName = nametext1.Text.Trim();
            string type = sensortype.Text.Trim();
            string location = location123.Text.Trim();
            DateTime installedDate = dateTimePicker1.Value;

            if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("All fields must be filled out to register a new sensor.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Insert query with SensorID
            string query = @"INSERT INTO Sensor (SensorID, SensorName, SensorType, Location, InstalledDate)
                 VALUES (@SensorID, @SensorName, @SensorType, @Location, @InstalledDate)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SensorID", sensorId);
                command.Parameters.AddWithValue("@SensorName", newName);
                command.Parameters.AddWithValue("@SensorType", type);
                command.Parameters.AddWithValue("@Location", location);
                command.Parameters.AddWithValue("@InstalledDate", installedDate);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show(rowsAffected > 0 ? "Sensor registered successfully!" : "Failed to register sensor.",
                                "Info", MessageBoxButtons.OK, rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                ClearTextBoxes();
            }

        }

        // Optional empty event handlers
        private void phonetext_TextChanged(object sender, EventArgs e) { }
        private void emailtext_Click(object sender, EventArgs e) { }

        // Load Sensor by ID
        private void LoadSensorById(int id)
        {
            string query = @"
                SELECT SensorName, SensorType, Location, InstalledDate
                FROM Sensor
                WHERE SensorID = @SensorID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SensorID", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    idtext1.Text = id.ToString();
                    nametext1.Text = reader["SensorName"].ToString();
                    sensortype.Text = reader["SensorType"].ToString();
                    location123.Text = reader["Location"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["InstalledDate"]);
                }
                else
                {
                    MessageBox.Show("No sensor found with the provided ID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                reader.Close();
            }
        }

        // Load by button
        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(idtext1.Text.Trim(), out int sensorId))
            {
                LoadSensorById(sensorId);
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Update sensor
        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(idtext1.Text.Trim(), out int sensorId))
            {
                MessageBox.Show("Invalid Sensor ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newName = nametext1.Text.Trim();
            string type = sensortype.Text.Trim();
            string location = location123.Text.Trim();
            DateTime installedDate = dateTimePicker1.Value;

            if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"UPDATE Sensor
                             SET SensorName = @SensorName,
                                 SensorType = @SensorType,
                                 Location = @Location,
                                 InstalledDate = @InstalledDate
                             WHERE SensorID = @SensorID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SensorID", sensorId);
                command.Parameters.AddWithValue("@SensorName", newName);
                command.Parameters.AddWithValue("@SensorType", type);
                command.Parameters.AddWithValue("@Location", location);
                command.Parameters.AddWithValue("@InstalledDate", installedDate);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show(rowsAffected > 0 ? "Record updated successfully!" : "No record was updated.",
                                "Info", MessageBoxButtons.OK, rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
        }

        // Delete sensor
        private void button4_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(idtext1.Text.Trim(), out int sensorId))
            {
                MessageBox.Show("Please enter a valid SensorID to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this sensor record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            string query = "DELETE FROM Sensor WHERE SensorID = @SensorID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SensorID", sensorId);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show(rowsAffected > 0 ? "Record deleted successfully!" : "No record found to delete.",
                                "Info", MessageBoxButtons.OK, rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                ClearTextBoxes();
            }
        }

        // Clear all input fields
        private void ClearTextBoxes()
        {
            idtext1.Clear();
            nametext1.Clear();
            sensortype.Clear();
            location123.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        // Navigate back
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
