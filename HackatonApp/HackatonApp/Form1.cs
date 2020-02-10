using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HackatonApp
{

    public partial class IntroForm : Form
    {
        int x = 5;
        int y = 141;
        public IntroForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.SetBounds(x, 141, 1, 1);
            x++;
            if (x >= 800)
            {
                x = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SmartScanner smartScanner = new SmartScanner();
            smartScanner.ShowDialog();
            this.Close();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.SaddleBrown;
            button1.ForeColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
        }
    }
}
