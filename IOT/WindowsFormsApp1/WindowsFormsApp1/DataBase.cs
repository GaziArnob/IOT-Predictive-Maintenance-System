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
    public partial class DataBase : Form
    {
        public DataBase()
        {
            InitializeComponent();
        }



        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            Local_Database d1 = new Local_Database();
            d1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Local_Database d1 = new Local_Database();
            d1.Show();
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

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Machine_Database md1 = new Machine_Database();
            md1.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Hide();
            Machine_Database md1 = new Machine_Database();
            md1.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f3 = new Form2();
            f3.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Maintain maintain = new Maintain();
            maintain.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
