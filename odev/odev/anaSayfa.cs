using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odev
{
    public partial class anaSayfa : Form
    {
        public anaSayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hafta1 hafta1 = new hafta1();
            this.Hide();
            hafta1.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hafta2 hafta2 = new hafta2();
            this.Hide();
            hafta2.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hafta3 hafta3 = new hafta3();
            this.Hide();
            hafta3.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hafta4 hafta4 = new hafta4();
            this.Hide();
            hafta4.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hafta5 hafta5 = new hafta5();
            this.Hide();
            hafta5.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            hafta6 hafta6 = new hafta6();
            this.Hide();
            hafta6.ShowDialog();
            this.Close();
        }
    }
}
