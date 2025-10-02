using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Machine_updatecs : Form
    {
        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";

        public Machine_updatecs()
        {
            InitializeComponent();
        }

        // ✅ Load machine data by ID
        private void LoadMachineById(int id)
        {
            string query = @"SELECT MachineID, MachineName, Power, Force, ControlSystem, Fuel, Output 
                             FROM Machine 
                             WHERE MachineID = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    idtext1.Text = reader["MachineID"].ToString();
                    nametext1.Text = reader["MachineName"].ToString();
                    powertext1.Text = reader["Power"].ToString();
                    forcetext1.Text = reader["Force"].ToString();
                    controlsystem.Text = reader["ControlSystem"].ToString();
                    fuel1.Text = reader["Fuel"].ToString();
                    Output1.Text = reader["Output"].ToString();
                }
                else
                {
                    MessageBox.Show("No machine found with this ID.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                reader.Close();
            }
        }

        // ✅ Update machine
        private void button1_Click(object sender, EventArgs e)
        {
            string id2 = idtext1.Text.Trim();
            string newName = nametext1.Text.Trim();
            string power = powertext1.Text.Trim();
            string force = forcetext1.Text.Trim();
            string control_system = controlsystem.Text.Trim();
            string fuel = fuel1.Text.Trim();
            string output = Output1.Text.Trim();

            if (string.IsNullOrWhiteSpace(id2) || string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Machine ID and Name are required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"UPDATE Machine
                             SET MachineName = @MachineName,
                                 Power = @Power,
                                 Force = @Force,
                                 ControlSystem = @ControlSystem,
                                 Fuel = @Fuel,
                                 Output = @Output
                             WHERE MachineID = @MachineID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MachineID", id2);
                command.Parameters.AddWithValue("@MachineName", newName);
                command.Parameters.AddWithValue("@Power", power);
                command.Parameters.AddWithValue("@Force", force);
                command.Parameters.AddWithValue("@ControlSystem", control_system);
                command.Parameters.AddWithValue("@Fuel", fuel);
                command.Parameters.AddWithValue("@Output", output);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                MessageBox.Show(rowsAffected > 0 ?
                    "Record updated successfully!" :
                    "No record was updated. Please check Machine ID.",
                    rowsAffected > 0 ? "Success" : "Error",
                    MessageBoxButtons.OK,
                    rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
        }

        // ✅ Load machine by ID
        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(idtext1.Text.Trim(), out int userId))
            {
                LoadMachineById(userId);
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ✅ Insert new machine
        private void button3_Click(object sender, EventArgs e)
        {
            string id2 = idtext1.Text.Trim();
            string newName = nametext1.Text.Trim();
            string power = powertext1.Text.Trim();
            string force = forcetext1.Text.Trim();
            string control_system = controlsystem.Text.Trim();
            string fuel = fuel1.Text.Trim();
            string output = Output1.Text.Trim();

            if (string.IsNullOrWhiteSpace(id2) || string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Machine ID and Name are required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"INSERT INTO Machine
                             (MachineID, MachineName, Power, Force, ControlSystem, Fuel, Output)
                             VALUES
                             (@MachineID, @MachineName, @Power, @Force, @ControlSystem, @Fuel, @Output)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MachineID", id2);
                command.Parameters.AddWithValue("@MachineName", newName);
                command.Parameters.AddWithValue("@Power", power);
                command.Parameters.AddWithValue("@Force", force);
                command.Parameters.AddWithValue("@ControlSystem", control_system);
                command.Parameters.AddWithValue("@Fuel", fuel);
                command.Parameters.AddWithValue("@Output", output);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                MessageBox.Show(rowsAffected > 0 ?
                    "Machine added successfully!" :
                    "Failed to add machine.",
                    rowsAffected > 0 ? "Success" : "Error",
                    MessageBoxButtons.OK,
                    rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            if (!int.TryParse(idtext1.Text.Trim(), out int machineId))
            {
                MessageBox.Show("Please enter a valid Machine ID to delete.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this machine?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM Machine WHERE MachineID = @MachineID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MachineID", machineId);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    MessageBox.Show(rowsAffected > 0 ?
                        "Machine deleted successfully!" :
                        "No machine found with this ID.",
                        rowsAffected > 0 ? "Success" : "Error",
                        MessageBoxButtons.OK,
                        rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                    if (rowsAffected > 0) ClearTextBoxes();
                }
            }
            // Your delete logic, or whatever you want this button to do
            MessageBox.Show("Button 5 clicked!");
        }



        // ✅ Delete machine
        private void button5_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(idtext1.Text.Trim(), out int machineId))
            {
                MessageBox.Show("Please enter a valid Machine ID to delete.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this machine?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM Machine WHERE MachineID = @MachineID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MachineID", machineId);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    MessageBox.Show(rowsAffected > 0 ?
                        "Machine deleted successfully!" :
                        "No machine found with this ID.",
                        rowsAffected > 0 ? "Success" : "Error",
                        MessageBoxButtons.OK,
                        rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                    if (rowsAffected > 0) ClearTextBoxes();
                }
            }
        }

        // ✅ Back button
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Machine_Database f1 = new Machine_Database();
            f1.Show();
        }

        // ✅ Clear input fields
        private void ClearTextBoxes()
        {
            idtext1.Clear();
            nametext1.Clear();
            powertext1.Clear();
            forcetext1.Clear();
            controlsystem.Clear();
            fuel1.Clear();
            Output1.Clear();
        }
    }
}
