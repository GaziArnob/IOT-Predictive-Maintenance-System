using System;
using System.IO.Ports;
using System.Windows.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class MachineDashboardApp : Form
    {
       
        string accountSid = "AC2d7b65c0612f8a2bc48ace8590db3161";
        string authToken = "8ae51f7ff026d7b9a4b1c562771e569b";
        string fromWhatsApp = "whatsapp:+14155238886"; 
        string toWhatsApp = "whatsapp:+8801643980679"; 

        
        string connectionString = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";

        private SerialPort serialPort;

        public MachineDashboardApp()
        {
            InitializeComponent();
        }

        private void MachineDashboardApp_Load(object sender, EventArgs e)
        {
            
            serialPort = new SerialPort();
            serialPort.BaudRate = 9600;   
            serialPort.DataReceived += serialPort_DataReceived;
        }

        
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.PortName = "COM3"; 
                    serialPort.Open();
                    lblStatus.Text = "✅ Connected to Arduino on " + serialPort.PortName;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "❌ Error: " + ex.Message;
            }
        }

      
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    lblStatus.Text = "🔌 Disconnected";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "❌ Error: " + ex.Message;
            }
        }

        
        

       
        

       
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.WriteLine("START");  
                lblStatus.Text = "✅ Start Command Sent!";
                LogActionToDB("Start Command Sent");
            }
            else
            {
                lblStatus.Text = "⚠️ Not Connected!";
            }
        }

       
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort.ReadLine();
                this.Invoke(new Action(() =>
                {
                    if (data.StartsWith("VIB:"))
                        lblVib.Text = "Vibration: " + data.Replace("VIB:", "");
                    else if (data.StartsWith("CUR:"))
                        lblCurrent.Text = "Current: " + data.Replace("CUR:", "");
                    else if (data.StartsWith("VOL:"))
                        lblVolt.Text = "Voltage: " + data.Replace("VOL:", "");

                   
                    SaveSensorDataToDB(lblVib.Text, lblCurrent.Text, lblVolt.Text, "Data Received");
                }));
            }
            catch { }
        }

       
        private void SaveSensorDataToDB(string vibText, string curText, string voltText, string status)
        {
            try
            {
                decimal vib = 0, cur = 0, volt = 0;

               
                decimal.TryParse(vibText.Replace("Vibration:", "").Trim(), out vib);
                decimal.TryParse(curText.Replace("Current:", "").Trim(), out cur);
                decimal.TryParse(voltText.Replace("Voltage:", "").Trim(), out volt);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO SensorLog (Vibration, [Current], Voltage, StatusMessage) VALUES (@vib, @cur, @volt, @status)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@vib", vib);
                        cmd.Parameters.AddWithValue("@cur", cur);
                        cmd.Parameters.AddWithValue("@volt", volt);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "❌ DB Error: " + ex.Message;
            }
        }


        private void lblStatus_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("Status label clicked!");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
               
                TwilioClient.Init(accountSid, authToken);

                
                if (string.IsNullOrWhiteSpace(txtMessage.Text))
                {
                    lblStatus.Text = "⚠️ Message is empty!";
                    return;
                }

               
                var message = MessageResource.Create(
                    body: txtMessage.Text,
                    from: new PhoneNumber(fromWhatsApp),  
                    to: new PhoneNumber(toWhatsApp)       
                );

                lblStatus.Text = $"✅ Message Sent! SID: {message.Sid}";

                
                if (message.Status != MessageResource.StatusEnum.Queued &&
                   message.Status != MessageResource.StatusEnum.Sent &&
                   message.Status != MessageResource.StatusEnum.Delivered)
                {
                    lblStatus.Text += " ⚠️ Check Twilio Console for errors!";
                }

               
                LogActionToDB("WhatsApp message sent");
            }
            catch (Twilio.Exceptions.ApiException apiEx)
            {
                lblStatus.Text = "❌ Twilio API Error: " + apiEx.Message;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "❌ Error: " + ex.Message;
            }
        }


        private void btnManualShutdown_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.WriteLine("SHUTDOWN");  
                lblStatus.Text = "⚠️ Manual Shutdown Sent!";
                LogActionToDB("Manual Shutdown Command Sent");
            }
            else
            {
                lblStatus.Text = "⚠️ Not Connected!";
            }
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            btnSend_Click(sender, e);
        }


        private void btnManualShatdown_Click(object sender, EventArgs e)
        {
            btnManualShutdown_Click(sender, e); 
        }

       
        private void LogActionToDB(string action)
        {
            SaveSensorDataToDB(lblVib.Text, lblCurrent.Text, lblVolt.Text, action);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home h = new Home();
            h.Show();
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
            Form3 f2 = new Form3();
            f2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Maintain m1 = new Maintain();   
            m1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBase d1 = new DataBase();
            d1.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
}
