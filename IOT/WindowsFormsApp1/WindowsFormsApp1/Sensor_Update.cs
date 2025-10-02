using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Sensor_Update : Form
    {
        public Sensor_Update()
        {
            InitializeComponent();
            LoadWorkOrders();
        }

        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";

        // Load sensor record by ControlID
        private void LoadSensorByControlId(int controlId)
        {
            string query = @"
                SELECT ControlID, MachineID, SensorID, SensorStatus, SensorCommand, LastCheck
                FROM MachineSensorControl
                WHERE ControlID = @ControlID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ControlID", controlId);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["ControlID"].ToString();
                    textBox2.Text = reader["MachineID"].ToString();
                    textBox3.Text = reader["SensorID"].ToString();
                    textBox4.Text = reader["SensorStatus"].ToString();
                    textBox5.Text = reader["SensorCommand"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["LastCheck"]);
                }
                else
                {
                    MessageBox.Show("No record found for the provided ControlID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                reader.Close();
            }
        }

        // Search button
        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text.Trim(), out int controlId))
            {
                MessageBox.Show("ControlID must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadSensorByControlId(controlId);
            DisplayInDataGridView(controlId);
        }

        // Update button
        private void button2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text.Trim(), out int controlId) ||
                !int.TryParse(textBox2.Text.Trim(), out int machineId) ||
                !int.TryParse(textBox3.Text.Trim(), out int sensorId))
            {
                MessageBox.Show("ControlID, MachineID, and SensorID must be numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sensorStatus = textBox4.Text.Trim();
            string sensorCommand = textBox5.Text.Trim();
            DateTime lastCheck = dateTimePicker1.Value;

            if (string.IsNullOrWhiteSpace(sensorStatus) || string.IsNullOrWhiteSpace(sensorCommand))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"
                UPDATE MachineSensorControl
                SET 
                    MachineID = @MachineID,
                    SensorID = @SensorID,
                    SensorStatus = @SensorStatus,
                    SensorCommand = @SensorCommand,
                    LastCheck = @LastCheck
                WHERE ControlID = @ControlID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ControlID", controlId);
                command.Parameters.AddWithValue("@MachineID", machineId);
                command.Parameters.AddWithValue("@SensorID", sensorId);
                command.Parameters.AddWithValue("@SensorStatus", sensorStatus);
                command.Parameters.AddWithValue("@SensorCommand", sensorCommand);
                command.Parameters.AddWithValue("@LastCheck", lastCheck);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                    MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("No record was updated. Please check the ControlID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadWorkOrders(); // refresh DataGridView
        }

        // Delete button
        private void button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text.Trim(), out int controlId))
            {
                MessageBox.Show("Please select a valid ControlID to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this sensor record?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM MachineSensorControl WHERE ControlID = @ControlID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ControlID", controlId);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadWorkOrders();
                        ClearTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show("No record was found to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Refresh button
        private void button11_Click(object sender, EventArgs e)
        {
            LoadWorkOrders();
        }

        // Load all sensor records
        private void LoadWorkOrders()
        {
            string query = "SELECT * FROM MachineSensorControl";
            FillDataGridView(query);
        }

        private void DisplayInDataGridView(int controlId)
        {
            string query = "SELECT * FROM MachineSensorControl WHERE ControlID = @ControlID";
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@ControlID", controlId);
                con.Open();
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                dataGridView1.DataSource = dt;
            }
        }

        private void FillDataGridView(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, con))
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                dataGridView1.DataSource = dt;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Optional: add functionality here
        }


        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // Variables for other columns
            int machineId;
            int sensorId;
            string sensorStatus;
            string sensorCommand;
            DateTime lastCheck;

            // Assign values from textboxes
            if (!int.TryParse(textBox2.Text.Trim(), out machineId) ||
                !int.TryParse(textBox3.Text.Trim(), out sensorId))
            {
                MessageBox.Show("MachineID and SensorID must be numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sensorStatus = textBox4.Text.Trim();
            sensorCommand = textBox5.Text.Trim();
            lastCheck = dateTimePicker1.Value;

            if (string.IsNullOrWhiteSpace(sensorStatus) || string.IsNullOrWhiteSpace(sensorCommand))
            {
                MessageBox.Show("SensorStatus and SensorCommand cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Insert without ControlID
            string query = @"
        INSERT INTO MachineSensorControl 
        (MachineID, SensorID, SensorStatus, SensorCommand, LastCheck)
        VALUES (@MachineID, @SensorID, @SensorStatus, @SensorCommand, @LastCheck)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MachineID", machineId);
                command.Parameters.AddWithValue("@SensorID", sensorId);
                command.Parameters.AddWithValue("@SensorStatus", sensorStatus);
                command.Parameters.AddWithValue("@SensorCommand", sensorCommand);
                command.Parameters.AddWithValue("@LastCheck", lastCheck);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Sensor record inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadWorkOrders(); // Refresh DataGridView
                    ClearTextBoxes();
                }
                else
                {
                    MessageBox.Show("Failed to insert sensor record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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
            Machine machine = new Machine();
            machine.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.Show();
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
            DataBase database = new DataBase();
            database.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            Maintain maintain = new Maintain();
            maintain.Show();
        }
    }
}

    

