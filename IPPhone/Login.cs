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
    public partial class Login : Form
    {
        private SqlConnection con;
        private DataTable dtFilterUser = new DataTable("UserInfo");
        private SqlDataAdapter da = new SqlDataAdapter();

        public Login()
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            connect();
            //Login
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.Text;
            command.CommandText = @"Select * from UserInfo
                                            where (Username = @Username)
                                            And (Password = @Password)";
            command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = txtName.Text;
            command.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = txtPass.Text;

            da.SelectCommand = command;


            da.Fill(dtFilterUser);
            DataRow[] dtrow = dtFilterUser.Select();

            if (dtFilterUser.Rows.Count > 0) 
            {
                if (dtrow[0]["Restriction"].ToString() == "0")
                {
                    Administrator _form1 = new Administrator();
                    _form1.Show();
                    Hide();
                }
                else
                {
                    Guest _guest = new Guest();
                    _guest.Show();
                    Hide();
                }
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại", "ĐĂNG NHẬP", MessageBoxButtons.OK);
            }
            disconnect();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }


    }
}
