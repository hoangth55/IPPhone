using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;

namespace IPPhone
{
    public partial class Administrator : Form
    {
        //private SqlDataAdapter da = new SqlDataAdapter();
        //private DataTable dt = new DataTable("IPPhone");
        private SqlConnection con;
        private DataTable dtFilterPartition = new DataTable("Partition");
        private SqlDataAdapter da = new SqlDataAdapter();

        //private DataTable dt = new DataTable("InfoUser");
        // private DataTable dtEncryption = new DataTable("InfoEncryption");
        //private SqlDataAdapter da = new SqlDataAdapter();

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

        public Administrator()
        {
            InitializeComponent();
        }

        private void pushCallRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            connect();

            // Set filter options and filter index.
            openFileDialog1.Filter = "CSV File (.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = true;

            string full_path;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                full_path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                //MessageBox.Show(full_path, "OKE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //--------Push to database---------------------
                System.IO.StreamReader myFile = new System.IO.StreamReader(full_path);
                string myString = myFile.ReadLine();

                //System.Console.WriteLine(myString);

                string[] split = myString.Split(new Char[] { ',' });

                int callingPartyNumberIndex = Array.IndexOf(split, "callingPartyNumber");
                //Console.WriteLine(callingPartyNumberIndex);

                int finalCalledPartyNumberIndex = Array.IndexOf(split, "finalCalledPartyNumber");
                //Console.WriteLine(finalCalledPartyNumberIndex);

                int dateTimeConnectIndex = Array.IndexOf(split, "dateTimeConnect");
                // Console.WriteLine(dateTimeConnectIndex);

                int dateTimeDisconnectIndex = Array.IndexOf(split, "dateTimeDisconnect");
                //Console.WriteLine(dateTimeDisconnectIndex);

                int finalCalledPartyNumberPartitionIndex = Array.IndexOf(split, "finalCalledPartyNumberPartition");
                // Console.WriteLine(finalCalledPartyNumberPartitionIndex);

                int durationIndex = Array.IndexOf(split, "duration");
                //Console.WriteLine(durationIndex);
                int authCodeDescriptionIndex = Array.IndexOf(split, "authCodeDescription");
                int count = 0;

                while (!myFile.EndOfStream)
                {

                    string record = myFile.ReadLine();

                    string[] fields = record.Split(new Char[] { ',' });

                    string callingPartyNumber = fields[callingPartyNumberIndex];
                    string finalCalledPartyNumber = fields[finalCalledPartyNumberIndex];
                    DateTime dateTimeConnect = FromUnixTime(Convert.ToDouble(fields[dateTimeConnectIndex]));
                    DateTime dateTimeDisconnect = FromUnixTime(Convert.ToDouble(fields[dateTimeDisconnectIndex]));
                    string finalCalledPartyNumberPartition = fields[finalCalledPartyNumberPartitionIndex];
                    string duration = fields[durationIndex];
                    string authCodeDescription = fields[authCodeDescriptionIndex];

                    int callingPartyNumberID;
                    int finalCalledPartyNumberPartitionID;
                    double totalCharging;
                    

                    //--------------Check to get only Calling with Duration > 0 -----------------
                    if (Convert.ToInt32(duration) > 0)
                    {

                        //------------Check to get info about Partion Calling and Number's InFor

                        //---------------Get Partition-------------
                        SqlCommand getCommand = new SqlCommand();
                        getCommand.Connection = con;
                        getCommand.CommandType = CommandType.Text;
                        getCommand.CommandText = @"Select   Partition.ID as N'ID',
                                                        Name as N'Name' from Partition 
                                            where (Name = '" + finalCalledPartyNumberPartition + "')";

                        da.SelectCommand = getCommand;
                        dtFilterPartition.Clear();
                        da.Fill(dtFilterPartition);

                        if (dtFilterPartition.Rows.Count > 0)
                        {
                            DataRow[] dtrow = dtFilterPartition.Select();
                            finalCalledPartyNumberPartitionID = Convert.ToInt32(dtrow[0]["ID"].ToString());
                            dtFilterPartition.Clear();
                            //-----------------Finish get partition---------------

                            //---------------------Info Number-------------------
                            getCommand = new SqlCommand();
                            getCommand.Connection = con;
                            getCommand.CommandType = CommandType.Text;
                            getCommand.CommandText = @"Select   InfoNumberPhone.ID as N'ID',
                                                        PhoneNumber as N'PhoneNumber',
                                                        Department_Name as N'Department_Name',
                                                        Department_Des as N'Department_Des'
                                                    from InfoNumberPhone 
                                            where (PhoneNumber = '" + callingPartyNumber + "')";

                            da.SelectCommand = getCommand;
                            dtFilterPartition.Clear();
                            da.Fill(dtFilterPartition);

                            //-------------Checking to match InfoNumber---------
                            if (dtFilterPartition.Rows.Count > 0)
                            {
                                DataRow[] dtrow2 = dtFilterPartition.Select();
                                callingPartyNumberID = Convert.ToInt32(dtrow2[0]["ID"].ToString());
                                //-------------------Checking to get Full Information of Calling: authCodeDescription
                                if (authCodeDescription == "")
                                {
                                    authCodeDescription = dtrow2[0]["Department_Name"].ToString();
                                }
                                //-------------------Finish to get full Information

                                dtFilterPartition.Clear();
                                //-------------------Finish get calling ID--------------------

                                //-----------Computing the total charging for each calling----------
                                getCommand = new SqlCommand();
                                getCommand.Connection = con;
                                getCommand.CommandType = CommandType.Text;
                                getCommand.CommandText = @"Select   PriceList.ID as N'ID',
                                                        Category as N'Category',
                                                        NumberHeader as N'NumberHeader',
                                                        Minute as N'Minute',
                                                        Block6 as N'Block6',
                                                        Second as N'Second',
                                                        Type as N'Type'
                                                    from PriceList 
                                            where (Category = '" + finalCalledPartyNumberPartition + "')";

                                da.SelectCommand = getCommand;
                                dtFilterPartition.Clear();
                                da.Fill(dtFilterPartition);
                                if (dtFilterPartition.Rows.Count > 0 )
                                {
                                    DataRow[] dtrow3 = dtFilterPartition.Select();
                                    // case (dtrow3[0]["Category"].ToString()):
                                    //String type_call = dtrow3[0]["Category"].ToString();
                                    int type_call = Convert.ToInt32(dtrow3[0]["Type"].ToString());
                                    if (type_call == 0)
                                    {
                                        int _minute = Convert.ToInt32(duration) / 60;
                                        int temp = Convert.ToInt32(duration) % 60;
                                        int _block = temp / 6;
                                        int _second = temp % 6;

                                        totalCharging = _minute * Convert.ToDouble(dtrow3[0]["Minute"].ToString()) +
                                            _block * Convert.ToDouble(dtrow3[0]["Block6"].ToString()) +
                                            _second * Convert.ToDouble(dtrow3[0]["Second"].ToString());

                                    }
                                    else
                                    {
                                        int _minute = Convert.ToInt32(duration) / 60;
                                        if (Convert.ToDouble(duration) % 60 > 0)
                                            _minute++;

                                        totalCharging = _minute * Convert.ToDouble(dtrow3[0]["Minute"].ToString());

                                    }
                                    
                                }
                                else //--------If not found in PriceList Info, it will return 0;
                                {
                                    totalCharging = 0;
                                }


                                //--------- Finish compute charging---------------

                                //-----------Push each call to server--------------
                                count++;
                                string sql = "INSERT INTO CallRecord(CallingPartyNumber_ID, AuthCodeDescription,FinalCalledPartyNumber, DateTimeConnect, DateTimeDisconnect, FinalCalledPartyNumberPartition_ID, Duration, TotalCharging) values (@CallingPartyNumber_ID_, @AuthCodeDescription_,@FinalCalledPartyNumber_, @DateTimeConnect_,@DateTimeDisconnect_, @FinalCalledPartyNumberPartition_ID_, @Duration_, @TotalCharging_)";
                                //con.Open();
                                SqlCommand cmd = new SqlCommand();
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = sql;
                                cmd.Parameters.AddWithValue("@CallingPartyNumber_ID_", callingPartyNumberID);
                                cmd.Parameters.AddWithValue("@FinalCalledPartyNumber_", finalCalledPartyNumber);
                                cmd.Parameters.AddWithValue("@DateTimeConnect_", dateTimeConnect);
                                cmd.Parameters.AddWithValue("@DateTimeDisconnect_", dateTimeDisconnect);
                                cmd.Parameters.AddWithValue("@FinalCalledPartyNumberPartition_ID_", finalCalledPartyNumberPartitionID);
                                cmd.Parameters.AddWithValue("@Duration_", duration);
                                cmd.Parameters.AddWithValue("@TotalCharging_", totalCharging);
                                cmd.Parameters.AddWithValue("@AuthCodeDescription_", authCodeDescription);
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
                            else
                            {
                                continue;
                            }//----------------------Finish to checking Info Number---------------

                        }
                        else
                        {
                            continue;
                        }//------------Finish to check to matching Partition

                    }//--------------Finish to check Calling with Duration > 0 -----------------

                }
                disconnect();
                //System.Console.WriteLine(callingPartyNumber + " " + finalCalledPartyNumber + " " + dateTimeConnect + "\n");

                myFile.Close();
                System.Console.ReadLine();
                MessageBox.Show("Finish!", "Finish total " + count + "records!", MessageBoxButtons.OK, MessageBoxIcon.None);

            }



        }


        private void updateInfoNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoNumber _infoNumber = new InfoNumber();
            _infoNumber.Show();
        }

        private void updatePartitionNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoPartition _partitionInfo = new InfoPartition();
            _partitionInfo.Show();
        }

        private void dataGridView_Administrator_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }

        public DateTime FromUnixTime(Double unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

    }
    
}
