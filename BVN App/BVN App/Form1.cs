using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Diagnostics;
using System.Net;

namespace Bvn_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide();
            var f2 = new cruds();
            f2.Closed += (s, args) => this.Close();
            f2.Show();
            timer1.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
