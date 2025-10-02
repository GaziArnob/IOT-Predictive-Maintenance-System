using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";
            //string connectionString = "data source=DESKTOP-C095TBV\\SQLEXPRESS01; database=KK; integrated security=SSPI";


            string id = idtext1.Text.Trim();
            string name = nametext1.Text.Trim();
            string age = agetext1.Text.Trim();
            string email = emailtext1.Text.Trim();
            string phone = phonetext1.Text.Trim();
            string password = passwordtext1.Text.Trim();
            string conpassword = passwordtext2.Text.Trim();
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

            DateTime DateOfBirth = dateTimePicker1.Value.Date;
            string address = addresstext.Text.Trim();

            if (password != conpassword)
            {
                MessageBox.Show("Passwords are not matched.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(age) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(conpassword))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!int.TryParse(age, out int parsedAge))
            {
                MessageBox.Show("Age must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO Users(Id, Name, Age, Address, Email, Phone, Gender, Password, DateOfBirth) VALUES (@Id, @Name, @Age, @Address ,@Email,@Phone ,@Gender,@Password,@DateOfBirth)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Age", parsedAge);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form1 f1 = new Form1();
                        f1.Show();
                    }
                    else
                    {
                        MessageBox.Show("Failed to create the profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
