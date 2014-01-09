using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IPPhone
{
    public partial class Guest : Form
    {
        private SqlConnection con;
        private DataTable dt = new DataTable("CallRecord");
        private SqlDataAdapter da = new SqlDataAdapter();

        public Guest()
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
            //CallRecord(, , , , , ) values (@CallingPartyNumber_ID_, @FinalCalledPartyNumber_, @DateTimeConnect_,@DateTimeDisconnect_, @FinalCalledPartyNumberPartition_ID_, @Duration_)";
                            //con.Open();
            command.CommandText = @"Select 
                                        CallRecord.ID as N'ID',
                                        CallingPartyNumber_ID as N'CallingPartyNumber_ID', 
                                        FinalCalledPartyNumber as N'FinalCalledPartyNumber',
                                        DateTimeConnect as N'DateTimeConnect',
                                        DateTimeDisconnect as N'DateTimeDisconnect',
                                        FinalCalledPartyNumberPartition_ID as N'FinalCalledPartyNumberPartition_ID',
                                        Duration as N'Duration'
                                    from CallRecord";
            da.SelectCommand = command;
            da.Fill(dt);
            dataGridView_Guest.DataSource = dt;
        }

        private void dataBinding()
        {
            

        }

        private void Guest_Load(object sender, EventArgs e)
        {
            connect();
            getdata();
            dataBinding();
        }

        private void dataGridView_Guest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
