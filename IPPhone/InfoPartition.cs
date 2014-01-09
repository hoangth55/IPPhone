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
    public partial class InfoPartition : Form
    {
        private SqlConnection con;
        private DataTable dt = new DataTable("Partition");
        private SqlDataAdapter da = new SqlDataAdapter();

        
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



        public InfoPartition()
        {
            InitializeComponent();
        }

        private void getdata()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = @"Select 
                                        Partition.ID as N'ID',
                                        Name as N'Name'
                                    from Partition";
            da.SelectCommand = command;
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataBinding()
        {
            textID.DataBindings.Clear();
            textID.DataBindings.Add("Text", dataGridView1.DataSource, "ID");

            textName.DataBindings.Clear();
            textName.DataBindings.Add("Text", dataGridView1.DataSource, "Name");
            
        }

        private void PartitionInfo_Load(object sender, EventArgs e)
        {
            connect();
            getdata();
            dataBinding();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
            System.Windows.Forms.Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
