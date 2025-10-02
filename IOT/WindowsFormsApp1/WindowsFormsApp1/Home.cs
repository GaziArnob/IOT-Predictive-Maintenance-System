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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBase d1 = new DataBase();
            d1.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBase d2 = new DataBase();
            d2.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1(); 
                
               f1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Maintain m1 = new Maintain();
            m1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Machine m1 = new Machine();
            m1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();    
            Machine m2 = new Machine();
            m2.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Maintain m2 = new Maintain();   
            m2.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            SansorData s1 = new SansorData();   
            s1.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 s2 = new Form3();
            s2.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            MachineDashboardApp m1 = new MachineDashboardApp(); 
            m1.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Hide();
            MachineDashboardApp m1 = new MachineDashboardApp();
            m1.Show();
        }
    }
}
