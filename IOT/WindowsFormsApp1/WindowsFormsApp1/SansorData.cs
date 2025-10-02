using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class SansorData : Form
    {
        SerialPort port;
        string connStr = "Data Source=ARNOB\\SQLEXPRESS01;Initial Catalog=KK;Integrated Security=True";

        public SansorData()
        {
            InitializeComponent();  // ✅ must be called first

            // Chart setup
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.Title = "Time";
            chart1.ChartAreas[0].AxisY.Title = "Values";

            chart1.Series.Add("Voltage");
            chart1.Series["Voltage"].ChartType = SeriesChartType.Line;

            chart1.Series.Add("Current");
            chart1.Series["Current"].ChartType = SeriesChartType.Line;

            // Serial Port setup
            port = new SerialPort("COM4", 9600);
            port.DataReceived += Port_DataReceived;
            port.Open();
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string line = port.ReadLine();
            string[] parts = line.Split(',');

            if (parts.Length == 3)
            {
                double voltage = double.Parse(parts[0]);
                double current = double.Parse(parts[1]);
                int vibration = int.Parse(parts[2]);

                // ✅ Insert into SQL Server
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO SensorData (Voltage, [Current], Vibration) VALUES (@v, @c, @vi)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@v", voltage);
                        cmd.Parameters.AddWithValue("@c", current);
                        cmd.Parameters.AddWithValue("@vi", vibration);
                        cmd.ExecuteNonQuery();
                    }
                }

                // ✅ Update UI safely
                this.BeginInvoke(new Action(() =>
                {
                    label2.Text = voltage + " V";
                    label3.Text = current + " A";
                    label1.Text = (vibration == 1) ? "Vibration!" : "Stable";

                    // Chart update
                    chart1.Series["Voltage"].Points.AddY(voltage);
                    chart1.Series["Current"].Points.AddY(current);

                    if (chart1.Series["Voltage"].Points.Count > 50)
                    {
                        chart1.Series["Voltage"].Points.RemoveAt(0);
                        chart1.Series["Current"].Points.RemoveAt(0);
                    }
                }));
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            // Optional click handler
        }
    }
}
