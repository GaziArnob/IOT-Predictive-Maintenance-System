using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Work_order_update : Form
    {
        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";

        public Work_order_update()
        {
            InitializeComponent();
        }

        // 🔹 Load a single Work_Order by ID
        private void LoadWorkOrderById(int id)
        {
            string query = @"
                SELECT OrderID, MachineID, WorkerID, WorkDate, Shift, DurationHours, Remarks 
                FROM Work_Order 
                WHERE OrderID = @OrderID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderID", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    orderid.Text = reader["OrderID"].ToString();
                    machineid.Text = reader["MachineID"].ToString();
                    workid.Text = reader["WorkerID"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["WorkDate"]);
                    shift.Text = reader["Shift"].ToString();
                    duration.Text = reader["DurationHours"].ToString();
                    remarks.Text = reader["Remarks"].ToString();
                }
                else
                {
                    MessageBox.Show("No work order found with the provided ID.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                reader.Close();
            }
        }

        // 🔹 Update existing work order
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out int orderInt, out int machineInt, out int workerInt, out decimal durationDecimal))
                return;

            string query = @"
                UPDATE Work_Order
                SET WorkerID = @WorkerID,
                    MachineID = @MachineID,
                    WorkDate = @WorkDate,
                    Shift = @Shift,
                    DurationHours = @DurationHours,
                    Remarks = @Remarks
                WHERE OrderID = @OrderID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@WorkerID", workerInt);
                command.Parameters.AddWithValue("@MachineID", machineInt);
                command.Parameters.AddWithValue("@WorkDate", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@Shift", shift.Text.Trim());
                command.Parameters.AddWithValue("@DurationHours", durationDecimal);
                command.Parameters.AddWithValue("@Remarks", remarks.Text.Trim());
                command.Parameters.AddWithValue("@OrderID", orderInt);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show(rowsAffected > 0 ? "Record updated successfully!" : "No record updated.",
                    "Result", MessageBoxButtons.OK, rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
        }

        // 🔹 Load by ID button
        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(orderid.Text.Trim(), out int userId))
                LoadWorkOrderById(userId);
            else
                MessageBox.Show("Please enter a valid numeric ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // 🔹 Insert new work order
        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out int orderInt, out int machineInt, out int workerInt, out decimal durationDecimal))
                return;

            string query = @"INSERT INTO Work_Order
                (OrderID, MachineID, WorkerID, WorkDate, Shift, DurationHours, Remarks)
                VALUES (@OrderID, @MachineID, @WorkerID, @WorkDate, @Shift, @DurationHours, @Remarks)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderID", orderInt);
                command.Parameters.AddWithValue("@WorkerID", workerInt);
                command.Parameters.AddWithValue("@MachineID", machineInt);
                command.Parameters.AddWithValue("@WorkDate", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@Shift", shift.Text.Trim());
                command.Parameters.AddWithValue("@DurationHours", durationDecimal);
                command.Parameters.AddWithValue("@Remarks", remarks.Text.Trim());

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show(rowsAffected > 0 ? "Record inserted successfully!" : "Failed to insert record.",
                    "Result", MessageBoxButtons.OK, rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
        }

        // 🔹 Delete work order
        private void button5_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(orderid.Text.Trim(), out int orderInt))
            {
                MessageBox.Show("Please enter a valid OrderID to delete.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this work order?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM Work_Order WHERE OrderID = @OrderID";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderID", orderInt);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show("No record found to delete.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // 🔹 Back button
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Work_order w1 = new Work_order();
            w1.Show();
        }

        // 🔹 Validation helper
        private bool ValidateInputs(out int orderInt, out int machineInt, out int workerInt, out decimal durationDecimal)
        {
            orderInt = machineInt = workerInt = 0;
            durationDecimal = 0;

            string order = orderid.Text.Trim();
            string machine = machineid.Text.Trim();
            string worker = workid.Text.Trim();
            string dura = duration.Text.Trim();

            if (!int.TryParse(order, out orderInt) ||
                !int.TryParse(machine, out machineInt) ||
                !int.TryParse(worker, out workerInt) ||
                !decimal.TryParse(dura, out durationDecimal))
            {
                MessageBox.Show("Invalid input. Please check numeric fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // 🔹 Clear form
        private void ClearTextBoxes()
        {
            orderid.Clear();
            machineid.Clear();
            workid.Clear();
            shift.Clear();
            duration.Clear();
            remarks.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void orderid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
