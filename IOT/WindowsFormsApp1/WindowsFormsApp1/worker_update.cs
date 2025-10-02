using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class worker_update : Form
    {
        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";
        int id;

        public worker_update()
        {
            InitializeComponent();
        }

        // Load worker data by ID
        private void LoadWorkerById(int id)
        {
            string query = @"
                SELECT WorkerName, Phone, Email, Salary, Experience, Department
                FROM Workers
                WHERE WorkerID = @WorkerID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@WorkerID", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    nametext1.Text = reader["WorkerName"].ToString();
                    phonetext.Text = reader["Phone"].ToString();
                    email1.Text = reader["Email"].ToString();
                    exp.Text = reader["Salary"].ToString();
                    dept.Text = reader["Experience"].ToString();
                    salery.Text = reader["Department"].ToString();
                }
                else
                {
                    MessageBox.Show("No worker found with the provided ID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                reader.Close();
            }
        }

        // Update worker data
        private void button1_Click(object sender, EventArgs e)
        {
            string id2 = idtext1.Text.Trim();
            string newName = nametext1.Text.Trim();
            string phone = phonetext.Text.Trim();
            string email = email1.Text.Trim();
            string expe = exp.Text.Trim();
            string dep = dept.Text.Trim();
            string sal = salery.Text.Trim();

            if (string.IsNullOrWhiteSpace(newName) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(expe) || string.IsNullOrWhiteSpace(dep) ||
                string.IsNullOrWhiteSpace(sal))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"
                UPDATE dbo.Workers
                SET 
                    WorkerName = @WorkerName,
                    Phone = @Phone,
                    Email = @Email,
                    Salery = @Salery,
                    Experience = @Experience,
                    Department = @Department
                WHERE WorkerID = @WorkerID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@WorkerID", id2);
                command.Parameters.AddWithValue("@WorkerName", newName);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Salery", sal);
                command.Parameters.AddWithValue("@Experience", expe);
                command.Parameters.AddWithValue("@Department", dep);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No record was updated. Please check the Worker ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Load worker by ID from text box
        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(idtext1.Text.Trim(), out int userId))
            {
                LoadWorkerById(userId);
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric Worker ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // You can leave this empty or add some logic
        }


        // Back to worker list
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            workers w1 = new workers();
            w1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

            string id2 = idtext1.Text.Trim();
            string newName = nametext1.Text.Trim();
            string phone = phonetext.Text.Trim();
            string email = email1.Text.Trim();
            string expe = exp.Text.Trim();
            string dep = dept.Text.Trim();
            string sal = salery.Text.Trim();

            if (string.IsNullOrWhiteSpace(id2) || string.IsNullOrWhiteSpace(newName) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(expe) || string.IsNullOrWhiteSpace(dep) ||
                string.IsNullOrWhiteSpace(sal))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(id2, out int workerId))
            {
                MessageBox.Show("Worker ID must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(sal, out decimal salary))
            {
                MessageBox.Show("Salary must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(expe, out int experience))
            {
                MessageBox.Show("Experience must be a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            string query = "INSERT INTO Workers (WorkerID, WorkerName, Phone, Email ,Salary,Experience,Department) VALUES (@WorkerID, @WorkerName, @Phone, @Email ,@Salary,@Experience,@Department)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@WorkerID", id2);
                    command.Parameters.AddWithValue("@WorkerName", newName);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Salary", sal);
                    command.Parameters.AddWithValue("@Experience", expe);
                    command.Parameters.AddWithValue("@Department", dep);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                    }
                    else
                    {
                        MessageBox.Show("Failed to create the profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
