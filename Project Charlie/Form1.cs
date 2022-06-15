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
using System.Globalization;

namespace Project_Charlie
{
    
        
    //DrawItem event handler for your ListBox
   
    public partial class Form1 : Form
    {
        string conn =
                @"Data Source=DESKTOP-P39M3QI\SQLEXPRESS;Initial Catalog=Visitorinfo;Integrated Security=True
                ;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Form1()
        {
            InitializeComponent();
           
            Listboxload();

        }


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
            /*Firstname,Surname,Mobile,Email,Meeting_Date,Meeting_Time,Meeting_Aim,Staff_ID*/
        }
        
        public void Listboxload()
        {

            //OthermeetingsRead();
            dateTimePicker1.ValueChanged += new System.EventHandler(DateTimePicker1_ValueChanged);
            numericUpDown1.ValueChanged += new System.EventHandler(NumericUpDown1_ValueChanged);
            numericUpDown2.ValueChanged += new System.EventHandler(NumericUpDown2_ValueChanged);
            domainUpDown1.SelectedItemChanged += new System.EventHandler(DomainUpDown1_SelectedItemChanged);
            //InitializeComponent();
            SqlConnection con = new SqlConnection(conn);
            string sqlquery = "select * from Visitor";
            SqlCommand command = new SqlCommand(sqlquery, con);
            command.Parameters.Clear();
            con.Open();
            SqlDataReader sReader;
            sReader = command.ExecuteReader();
            while (sReader.Read())
            {  string MeetingWith = null;
                string Meeting_Date = Convert.ToString(sReader["Meeting_Date"] );
               
                
                SqlConnection con2 = new SqlConnection(conn);
                string sqlquery2 = "select * from Staff";
                SqlCommand command2 = new SqlCommand(sqlquery2, con2);
                command2.Parameters.Clear();
                con2.Open();
                SqlDataReader sReader2;
                sReader2 = command2.ExecuteReader();
                while (sReader2.Read())
                {
                    string vSID = (Convert.ToString(sReader2["Staff_ID"]));
                    string sSID = (Convert.ToString(sReader["Staff_ID"]));
                    if (vSID==sSID) 
                    {
                         MeetingWith = (Convert.ToString(sReader2["Meeting_With"]));
                    }
                }             
                listBox1.Items.Add
                    (  (sReader["Visitor_ID"]) +"-"+ (sReader["Firstname"])+ "-" +
                    (sReader["Surname"]) + "-" +
                   (sReader["Mobile"]) + "-" + (sReader["Email"])+ "-" +
                    (Meeting_Date.Remove(10))+ "-" +
                     (sReader["Meeting_Time"])+ "-" + (sReader["Meeting_Aim"])+ "-" +
                     MeetingWith);
               
            }
            sReader.Close();
            
            con.Close();
        }
        private void  NameID()
        {
            
          
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
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
       
        public int SID = 0;
        public void StaffIDfinder()
        {
            
            
            string MeetingWith = domainUpDown1.Text;
            SqlConnection con = new SqlConnection(conn);
            string sqlquery3 = "select Meeting_With from Staff";
            SqlCommand command = new SqlCommand(sqlquery3, con);
            command.Parameters.Clear();
            con.Open();
            SqlDataReader sReader;
            sReader = command.ExecuteReader();            
            List<string> MW = new List<string>();           
            int SIDINT = 1;
            while (sReader.Read())
            {
                MW.Add(Convert.ToString(sReader["Meeting_With"]));
                if (MW.Contains(MeetingWith))
                { break;}
                else
                {SIDINT++ ;}                       
            }
            SID = SIDINT;
            con.Close();
        }


        public void LoadToDatabase()
        {
             StaffIDfinder();
            string PhoneNumber = textBox3.Text;
            string Email = "'" + textBox4.Text + "'" ;
            
            string timeformathelper = ""; 
            string timeformathelper2 = "";           
            if (numericUpDown1.Value <= 9)
            { timeformathelper = "0"; }
            if (numericUpDown2.Value <= 9)
            { timeformathelper2 = "0"; }
           
            string TimeFormated = "'" + (timeformathelper + numericUpDown1.Value.ToString() + ":"
                 + timeformathelper2 + numericUpDown2.Value.ToString())+ "'";
            string MeetingAim = "'" + button1.Text+ "'" ;
            string Date = "'" + dateTimePicker1.Value.ToString("yyyy-MM-dd")+"'" ;
            
            SqlConnection con = new SqlConnection(conn);
            con.Open();
           
            string StaffIDNum = Convert.ToString(SID);
            string sqlqueryInsert =
                "insert into Visitor (Firstname,Surname,Mobile,Email,Meeting_Date,Meeting_Time,Meeting_Aim,Staff_ID)"
                + "values(" +"'"+ FName +"'"+"," +"'"+ LName + "'"+"," + PhoneNumber + ","
                + Email + "," + Date + "," + TimeFormated + ","
                + MeetingAim + "," + StaffIDNum + ");";
            SqlCommand CommandInsert = new SqlCommand(sqlqueryInsert, con);
            CommandInsert.ExecuteNonQuery();
        }
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
                            //string timeformathelper2 = "";
                            //string timeformathelper = "";
                            //if (numericUpDown1.Value <= 9)
                            //{ timeformathelper = "0"; }
                            //if (numericUpDown2.Value <= 9)
                            //{ timeformathelper2 = "0"; }




                            //listBox1.Items.Add(FName + "" + LName);
                            //listBox1.Items.Add("Meeting at: " + timeformathelper + numericUpDown1.Value.ToString() +
                            //        ":" + timeformathelper2 + numericUpDown2.Value.ToString());
                            //listBox1.Items.Add("Meeting on " + dateTimePicker1.Value.ToString("dd : MM : yyyy"));
                            //listBox1.Items.Add("Meeting with: " + domainUpDown1.Text);
                            //listBox1.Items.Add(button1.Text);
                            LoadToDatabase();
                            //OthermeetingsWrite();
                            //ClearAll();
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
        private short Meeting_ID;

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
                                   
                                    if (Login == true)
                                    {
                                        if (MeetingAim == true)
                                        {
                                            //string timeformathelper2 = "";
                                            //string timeformathelper = "";
                                            //if (numericUpDown1.Value <= 9)
                                            //{ timeformathelper = "0"; }
                                            //if (numericUpDown2.Value <= 9)
                                            //{ timeformathelper2 = "0"; }
                                            //listBox1.Items.Add(FName + "" + LName);
                                            //listBox1.Items.Add("Meeting at: " + timeformathelper + numericUpDown1.Value.ToString() +
                                            //        ":" + timeformathelper2 + numericUpDown2.Value.ToString());
                                            //listBox1.Items.Add("Meeting on " + dateTimePicker1.Value.ToString("dd : MM : yyyy"));
                                            //listBox1.Items.Add("Meeting with: " + domainUpDown1.Text);
                                            //listBox1.Items.Add(button1.Text);
                                            LoadToDatabase();
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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            StaffIDfinder();
            SqlConnection con = new SqlConnection(conn);
            string PhoneNumber = textBox3.Text;
            string Email = "'" + textBox4.Text + "'";
            string timeformathelper = "";
            string timeformathelper2 = "";
            if (numericUpDown1.Value <= 9)
            { timeformathelper = "0"; }
            if (numericUpDown2.Value <= 9)
            { timeformathelper2 = "0"; }
            string TimeFormated = "'" + (timeformathelper + numericUpDown1.Value.ToString() + ":"
                 + timeformathelper2 + numericUpDown2.Value.ToString()) + "'";
            string MeetingAim = "'" + button1.Text + "'";
            string Date = "'" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
            SqlConnection con1 = new SqlConnection(conn);
            con1.Open();
            string FirstName = textBox1.Text;
            string LastName = textBox2.Text;
            string StaffIDNum = Convert.ToString(SID);
            string sqlqueryUpdate =
                "Update Visitor set  " +
                "Staff_ID =' " +
                StaffIDNum +
                "',Firstname =' " +
               FirstName +
                "',Surname =' " +
                LastName +
                "',Mobile =' " +
                PhoneNumber +
                "',Email = " +
                Email+
                ",Meeting_Date = " +
                Date +
                ",Meeting_Aim = " +
               MeetingAim +
                ",Meeting_Time = " +
                TimeFormated +              
                "Where Visitor_ID =" 
                + Meeting_ID;
            SqlCommand CommandUpdate = new SqlCommand(sqlqueryUpdate, con);
            con.Open();
            CommandUpdate.ExecuteNonQuery();
            con.Close();            
            listBox1.Items.Clear();
           ClearAll();
            Listboxload();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            string sqlqueryDelete =
                "Delete From Visitor where Visitor_ID  = " + Meeting_ID;
            SqlCommand commandDelete = new SqlCommand(sqlqueryDelete, con);
            con.Open();
            SqlDataReader sReader;
            commandDelete.ExecuteNonQuery();
            con.Close();
            listBox1.Items.Clear();
            ClearAll();
            Listboxload();
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {

            var selectedvalue = listBox1.SelectedItem;
            if (selectedvalue != null)
            { MessageBox.Show(selectedvalue.ToString()); }
            string Data = listBox1.SelectedItem.ToString();
             string[] Field_Data = Data.Split('-');
            Meeting_ID = Int16.Parse(Field_Data[0]);
            SqlConnection con  = new SqlConnection(conn);
            string sqlquery2 =
                "select Visitor.Visitor_ID, Visitor.Staff_ID,Visitor.Firstname," +
                "Visitor.Surname,Visitor.Mobile,Visitor.Email,Visitor.Meeting_Date," +
                "Visitor.Meeting_Aim,Visitor.Meeting_Time,Visitor.Staff_ID,Staff.Staff_ID " +
                ",Staff.Meeting_With from Visitor,Staff where Staff.Staff_ID " +
                "= Visitor.Staff_ID and Visitor_ID  = " + Meeting_ID;
            SqlCommand command =  new SqlCommand(sqlquery2, con);
            con.Open();
            SqlDataReader sReader;                  
            sReader = command.ExecuteReader();
             while (sReader.Read())
             {
                textBox1.Text = sReader["Firstname"].ToString();
                textBox2.Text = sReader["Surname"].ToString();
                textBox3.Text = sReader["Mobile"].ToString();
                textBox4.Text = sReader["Email"].ToString();
                dateTimePicker1.Value = DateTime.Parse(sReader["Meeting_Date"].ToString());
                   
                button1.Text = sReader["Meeting_Aim"].ToString();
                domainUpDown1.SelectedIndex = Convert.ToInt32(sReader["Staff_ID"]);
                string time = sReader["Meeting_Time"].ToString();
                string[] Times = time.Split(':');
                short  time1 = Int16.Parse(Times[0]);
                short time2 = Int16.Parse(Times[1]);
                
                (numericUpDown1).Value = time1;
                (numericUpDown2).Value = time2;

             }




        }
    }
}
