using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Update : Form
    {
        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";

        public Update()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id2 = idtext1.Text.Trim();
            string newName = nametext1.Text.Trim();
            string newAge = agetext1.Text.Trim();
            string newAddress = addesstext1.Text.Trim();
            string email = emailtext1.Text.Trim();
            string phone = phonetext1.Text.Trim();  // Use phonetext1, not 'phone'
            string gender;

            if (Male1.Checked)
            {
                gender = Male1.Text;
            }
            else if (Female1.Checked)
            {
                gender = Female1.Text;
            }
            else
            {
                gender = Others1.Text;
            }

            if (string.IsNullOrWhiteSpace(newName) ||
                string.IsNullOrWhiteSpace(newAge) ||
                string.IsNullOrWhiteSpace(newAddress) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(gender))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"UPDATE Users 
                             SET Name = @Name, 
                                 Age = @Age, 
                                 Address = @Address, 
                                 Email = @Email, 
                                 Phone = @Phone, 
                                 Gender = @Gender
                             WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id2);
                    command.Parameters.AddWithValue("@Name", newName);
                    command.Parameters.AddWithValue("@Age", newAge);
                    command.Parameters.AddWithValue("@Address", newAddress);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Gender", gender);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      
                    }
                    else
                    {
                        MessageBox.Show("No record was updated. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadDataById(int id)
        {
            string query = @"SELECT Name, Age, Address, Email, Phone, Gender 
                             FROM Users 
                             WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        nametext1.Text = reader["Name"].ToString();
                        agetext1.Text = reader["Age"].ToString();
                        addesstext1.Text = reader["Address"].ToString();
                        emailtext1.Text = reader["Email"].ToString();
                        phonetext1.Text = reader["Phone"].ToString();

                        string gender = reader["Gender"].ToString();

                        if (gender == Male1.Text)
                        {
                            Male1.Checked = true;
                        }
                        else if (gender == Female1.Text)
                        {
                            Female1.Checked = true;
                        }
                        else
                        {
                            Others1.Checked = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found for the provided ID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(idtext1.Text.Trim(), out int userId))
            {
                LoadDataById(userId);
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Local_Database d1 = new Local_Database();
            d1.Show();
        }
    }
}
