using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
namespace Project_Charlie
{
    
    public partial class Form1 : Form
    {
        string conn =
                @"Data Source=DESKTOP-P39M3QI\SQLEXPRESS;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
       

        
        public bool Imput = false;
        static string Filename()
        {
            string fileName = string.Format(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                + @"\Employee information Project Charlie");
            return fileName;
        }
        static string FilenameMeetingInfo()
        {
            string FilenameMeetings = string.Format(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                + @"\Meeting information Project Charlie");
            return FilenameMeetings;
        }
        //private void OthermeetingsRead()
        //{
        //    string FilenameMeetings = FilenameMeetingInfo();
        //    listBox1.Items.Add(File.ReadAllLines(FilenameMeetings));

        //}
        private void OthermeetingsWrite()
        {
            string FilenameMeetings = FilenameMeetingInfo();
            using (TextWriter tw = File.AppendText(FilenameMeetings))
            {
                foreach (string item in listBox1.Items)
                    tw.WriteLine(item);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //OthermeetingsRead();
            dateTimePicker1.ValueChanged += new System.EventHandler(DateTimePicker1_ValueChanged);
            numericUpDown1.ValueChanged += new System.EventHandler(NumericUpDown1_ValueChanged);
            numericUpDown2.ValueChanged += new System.EventHandler(NumericUpDown2_ValueChanged);
            domainUpDown1.SelectedItemChanged += new System.EventHandler(DomainUpDown1_SelectedItemChanged);
            InitializeComponent();
            SqlConnection con = new SqlConnection(conn);
            string sqlquery = " use Visitorinfo;select[Staff_ID],[Meeting_With] from Staff";
            SqlCommand command = new SqlCommand(sqlquery, con);
            command.Parameters.Clear();
            
                con.Open();
          
            SqlDataReader sReader;
            
            
            sReader = command.ExecuteReader();

            while (sReader.Read())
            {
                listBox1.Items.Add((sReader["[Staff_ID]"] + " -" + sReader["[Meeting_With]"] + " " + ")"));
            }
       

            sReader.Close(); // Calling close() method
            con.Close();
        }
        public bool DateSelected = false;       
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {DateSelected = true;}       
        public bool TimeHoursselected = false;
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {TimeHoursselected = true;}
        public bool TimeMinutesSelected = false;
        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {TimeMinutesSelected = true;}
        public bool MeetingpersonSelected = false;
        private void DomainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {MeetingpersonSelected = true;}
       //
        public void ClearAll()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            ResetTimePicker();
            domainUpDown1.ResetText();
            ResetNumericUpDown1();
            ResetNumericUpDown2();            
            return;
        }  
        public DateTimePicker ResetTimePicker()
        { dateTimePicker1.Value = DateTime.Now; return dateTimePicker1;}
        public NumericUpDown ResetNumericUpDown1()
        {numericUpDown1.Value = 0; return numericUpDown1;}
        public NumericUpDown ResetNumericUpDown2()
        {numericUpDown2.Value = 0; return numericUpDown2;}
       //
        public void Button1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            if (MeetingpersonSelected == false)
            { PopupForm popup = new PopupForm(this); popup.ShowDialog(); }
            else if (DateSelected == false)
            { PopupForm popup = new PopupForm(this); popup.ShowDialog(); }
            else if (TimeHoursselected == false)
            { PopupForm popup = new PopupForm(this); popup.ShowDialog(); }
            else if (TimeMinutesSelected == false)
            { PopupForm popup = new PopupForm(this); popup.ShowDialog(); }
            else
            {
                f3.ShowDialog();
                this.Show();
                MeetingAim = true;
            }
            string PhoneNumberValidater = textBox3.Text;
            string Email = textBox4.Text;
           
            if (ValidatePhoneNumber(PhoneNumberValidater) == true)
            {
                if (validateEmail(Email) == true)
                {                   
                    if (Login == true)
                    {
                        if (MeetingAim == true)
                        {
                            string timeformathelper2 = "";
                            string timeformathelper = "";
                            if (numericUpDown1.Value <= 9)
                            { timeformathelper = "0"; }
                            if (numericUpDown2.Value <= 9)
                            { timeformathelper2 = "0"; }
                            listBox1.Items.Add(FName + "" + LName);
                            listBox1.Items.Add("Meeting at: " + timeformathelper + numericUpDown1.Value.ToString() +
                                    ":" + timeformathelper2 + numericUpDown2.Value.ToString());
                            listBox1.Items.Add("Meeting on " + dateTimePicker1.Value.ToString("dd : MM : yyyy"));
                            listBox1.Items.Add("Meeting with: " + domainUpDown1.Text);
                            listBox1.Items.Add(button1.Text);
                            OthermeetingsWrite();
                            ClearAll();
                        }
                    }
                }                
            }                  
        
        }
        public string FName;
        public string LName;       
        public bool ValidatePhoneNumber(string PhoneNumberValidater)
        {
            if (PhoneNumberValidater == null)
            {
                return false;
            }
            if (new PhoneAttribute().IsValid(PhoneNumberValidater) )
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }
        public bool validateEmail(string email)
        {
            if (email == null)
            {
                return false;
            }
            if (new EmailAddressAttribute().IsValid(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool FirstName = false;
        public bool LastName= false;
        public bool PhoneNumber = false;
        public bool EmailAddress = false;
        public bool MeetingAim = false;
        public bool Login = false;

        private void Button2_Click(object sender, EventArgs e)
        {      
            string fileName = Filename();          
            if(textBox1.Text.Length > 0)
            { FName =  textBox1.Text; FirstName = true;}            
            if (textBox2.Text.Length > 0)
            { LName = textBox2.Text; LastName = true;}
            if (textBox3.Text.Length > 0)
            {PhoneNumber = true;}
            if (textBox4.Text.Length > 0)
            { EmailAddress = true;}
            string PhoneNumberValidater = textBox3.Text;
            string Email =textBox4.Text ;
            if (FirstName == true)
            {
                if (LastName == true)
                {
                    if (ValidatePhoneNumber(PhoneNumberValidater) == true)
                    {
                        if (PhoneNumber == true)
                        {
                            if (EmailAddress == true)
                            {
                                if (validateEmail(Email) == true)
                                {
                                    Login = true;
                                    File.AppendAllText(fileName,
                                        textBox1.Text + " " + textBox2.Text +
                                   "\t" + textBox3.Text + "\t" + textBox4.Text);
                                    if (Login == true)
                                    {
                                        if (MeetingAim == true)
                                        {
                                            string timeformathelper2 = "";
                                            string timeformathelper = "";
                                            if (numericUpDown1.Value <= 9)
                                            { timeformathelper = "0"; }
                                            if (numericUpDown2.Value <= 9)
                                            { timeformathelper2 = "0"; }
                                            listBox1.Items.Add(FName + "" + LName);
                                            listBox1.Items.Add("Meeting at: " + timeformathelper + numericUpDown1.Value.ToString() +
                                                    ":" + timeformathelper2 + numericUpDown2.Value.ToString());
                                            listBox1.Items.Add("Meeting on " + dateTimePicker1.Value.ToString("dd : MM : yyyy"));
                                            listBox1.Items.Add("Meeting with: " + domainUpDown1.Text);
                                            listBox1.Items.Add(button1.Text);
                                            OthermeetingsWrite();
                                            ClearAll();
                                        }
                                    }
                                }
                                else
                                {
                                    string emailvalid = "You have Entered an Invalid Email Address \n" +
                                     "Please Enter a Valid Email Address ";
                                    string caption = "Error Invalid Email Address";
                                    MessageBox.Show(emailvalid, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else
                    {
                        string PhoneNumbervalid = "You have Entered an Invalid PhoneNumber \n" +
                             "Please Enter a Valid PhoneNumber ";
                        string caption = "Error Invalid PhoneNumber";

                        MessageBox.Show(PhoneNumbervalid, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
            }
            else
            {
                PopupForm popup = new PopupForm(this);
                popup.ShowDialog();
            }     
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
