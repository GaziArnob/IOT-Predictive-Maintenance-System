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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Machine machine = new Machine();
            machine.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Maintain maintain = new Maintain();
            maintain.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            DataBase database = new DataBase();
            database.Show();
        }
    }
}
