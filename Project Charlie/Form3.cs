using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Charlie
{
    public partial class Form3 : Form
    {
        System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["Form1"];
        public Form3()
        {InitializeComponent();}
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {((Form1)f).button1.Text = "Meeting Aim : Meeting ";}
            if (radioButton2.Checked)
            {((Form1)f).button1.Text = "Meeting Aim : Site Visit ";}
            if (radioButton3.Checked)
            {((Form1)f).button1.Text = "Meeting Aim : Sales appointment ";}
            if (radioButton4.Checked)
            {((Form1)f).button1.Text = "Meeting Aim : Student Interview ";}
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {this.Close();}
    }
}
