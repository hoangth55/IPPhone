using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IPPhone
{
    public partial class InfoNumber : Form
    {
        private SqlConnection con;
        private DataTable dt = new DataTable("InfoNumberPhone");
        private SqlDataAdapter da = new SqlDataAdapter();

        public InfoNumber()
        {
            InitializeComponent();
        }

        private void connect()
        {
            String cn = "Data Source=ADVENTURE\\HUYHOANG; Initial Catalog = IPPhone; Persist Security Info = True; User ID = sa; Password = 123456";
            //String cn = "Data Source = (local); Initial Catalog = iPMAC; Integrated Security = True";
            try
            {
                con = new SqlConnection(cn);
                con.Open(); //Mo cket noi
                //MessageBox.Show("Successful!", "Connected DB Successful!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong the ket noi toi DB!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void disconnect()
        {
            con.Close();
            con.Dispose();
            con = null;
        }



        private void getdata()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = @"Select 
                                        InfoNumberPhone.ID as N'ID',
                                        PhoneNumber as N'PhoneNumber',
                                        Department_Name as N'Department_Name',
                                        Department_Des as N'Department_Des'
                                    from InfoNumberPhone";
            da.SelectCommand = command;
            da.Fill(dt);
         
            
            dataGridView1.DataSource = dt;
        }

        private void dataBinding()
        {
            textID.DataBindings.Clear();
            textID.DataBindings.Add("Text", dataGridView1.DataSource, "ID");

            textNumber.DataBindings.Clear();
            textNumber.DataBindings.Add("Text", dataGridView1.DataSource, "PhoneNumber");

            textName.DataBindings.Clear();
            textName.DataBindings.Add("Text", dataGridView1.DataSource, "Department_Name");

            textDes.DataBindings.Clear();
            textDes.DataBindings.Add("Text", dataGridView1.DataSource, "Department_Des");
            
        }

        private void InfoNumber_Load(object sender, EventArgs e)
        {
            connect();
            getdata();
            dataBinding();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
            System.Windows.Forms.Application.Exit();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            connect();

            openFileDialog1.Filter = "CSV File (.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = true;

            string full_path;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                full_path = System.IO.Path.GetFullPath(openFileDialog1.FileName);

                //--------Push to database---------------------
                System.IO.StreamReader myFile = new System.IO.StreamReader(full_path);
                string myString = myFile.ReadLine();

                //System.Console.WriteLine(myString);

                string[] split = myString.Split(new Char[] { ',' });

                int deviceNameIndex = Array.IndexOf(split, "Device Name");
                //Console.WriteLine(callingPartyNumberIndex);

                int DescriptionIndex = Array.IndexOf(split, "Description");
                //Console.WriteLine(finalCalledPartyNumberIndex);

                int NumberIndex = Array.IndexOf(split, "Directory Number 1");
                // Console.WriteLine(dateTimeConnectIndex);

                int count = 0;
                while (!myFile.EndOfStream)
                {
                    count++;
                    string record = myFile.ReadLine();

                    string[] fields = record.Split(new Char[] { ',' });

                    string deviceName = fields[deviceNameIndex];
                    string Description = fields[DescriptionIndex];
                    string Number = fields[NumberIndex];
                  


                    //-----------Push each call to server--------------
                    string sql = "INSERT INTO InfoNumberPhone(PhoneNumber, Department_Name, Department_Des) values (@PhoneNumber_, @Department_Name_, @Department_Des_)";
                    //con.Open();@PhoneNumber_, @Department_Name_, @Department_Des
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@PhoneNumber_", Number);
                    cmd.Parameters.AddWithValue("@Department_Name_", Description);
                    cmd.Parameters.AddWithValue("@Department_Des_", deviceName);

                    try
                    {
                        //con.Open();
                        int recordsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (System.Data.SqlClient.SqlException sqlException)
                    {
                        System.Windows.Forms.MessageBox.Show(sqlException.Message);
                    }

                }
                disconnect();

            }         
        }
       
    }
}