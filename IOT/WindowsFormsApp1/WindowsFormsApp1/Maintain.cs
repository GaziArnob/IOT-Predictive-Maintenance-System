using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Maintain : Form
    {
        public Maintain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            workers w1 = new workers();
            w1.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Work_order w1 = new Work_order();
            w1.Show();
        }

        private void button10_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Machine m1 = new Machine(); 
            m1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home h1 = new Home();
            h1.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sensor_Update sensor_Update = new Sensor_Update();
            sensor_Update.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
