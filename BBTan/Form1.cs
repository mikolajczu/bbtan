using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBTan
{
    public partial class Form1 : Form
    {
        BBTan bbtan;
        public Form1()
        {
            InitializeComponent();
            groupBox2.Hide();
            button3.Hide();
            bbtan = new BBTan(pictureBox1.Width, pictureBox1.Height);
            bbtan.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            bbtan.Show(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(bbtan.Move())
                timer1.Stop();
            if (bbtan.End())
            {
                label7.Text = Convert.ToString(bbtan.lvl - 2);
                pictureBox1.Hide();
                groupBox2.Show();
                button3.Show();
                button2.Show();
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (button1.Enabled == false)
            {
                timer1.Start();
                bbtan.Shot(e.X, e.Y);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Hide();
            button1.Enabled = false;
            button1.Hide();
            button2.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Show();
            groupBox2.Hide();
            button3.Hide();
            button2.Hide();
            bbtan.Start();
        }
    }
}
