using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project_Charlie
{
    public partial class PopupForm : Form
    {
        public PopupForm(Form1 form1)
        {
            InitializeComponent();
           // this code tells them what Fields they need to fill out 
            if (form1.FirstName == false)
            {textBox1.AppendText("Your Firstname \r\n");}
            if (form1.LastName == false)
            {textBox1.AppendText ("Your LastName \r\n");}
            if (form1.PhoneNumber == false)
            {textBox1.AppendText ("Your PhoneNumber \r\n");}
            if (form1.EmailAddress == false)
            {textBox1.AppendText("Your EmailAddress \r\n");}                    
            if (form1.DateSelected == false)
            {textBox1.AppendText("The Date of Meeting\r\n");}
            if (form1.TimeHoursselected == false)
            {textBox1.AppendText("The Hour of the Meeting\r\n");}
            if (form1.TimeMinutesSelected == false)
            {textBox1.AppendText("The Minute of the Meeting\r\n");}
            if (form1.MeetingpersonSelected == false)
            {textBox1.AppendText("The Person you are Meeting with \r\n");}
        }
        private void button1_Click(object sender, EventArgs e)
        {this.Close();}

        private void PopupForm_Load(object sender, EventArgs e)
        {

        }
    }
}
