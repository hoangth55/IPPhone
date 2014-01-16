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
        private SqlConnection con; 
        private SqlDataAdapter da = new SqlDataAdapter();

        private DataTable dtPartition = new DataTable("Partition");
        private DataTable dtInfoNumberPhone = new DataTable("InfoNumberPhone");
        private DataTable dtPriceList = new DataTable("PriceList");

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

                //-----------Create table Partion from Database--------------
                SqlCommand cmdPartition = new SqlCommand();
                cmdPartition.Connection = con;
                cmdPartition.CommandType = CommandType.Text;
                cmdPartition.CommandText = @"Select   Partition.ID as N'ID',
                                                        Name as N'Name' from Partition";

                da.SelectCommand = cmdPartition;
                dtPartition.Clear();
                da.Fill(dtPartition);
                int rowPartition = dtPartition.Rows.Count;
                DataRow[] dtrowPartition = dtPartition.Select();

                List<PartitionInfo> PartitionTable = new List<PartitionInfo>();

                for (int i = 0; i < rowPartition; i++)
                {
                    PartitionInfo partitionTable = new PartitionInfo();
                    partitionTable.Id = Convert.ToInt32(dtrowPartition[i]["ID"].ToString());
                    partitionTable.Name = dtrowPartition[i]["Name"].ToString();
                    PartitionTable.Add(partitionTable);
                }

                int totalPartitionTable = PartitionTable.Count;
                //-------------Finish get partition------------

                //-------------Create table InfoNumberPhone from Database--------------
                SqlCommand cmdInfoNumber = new SqlCommand();
                cmdInfoNumber.Connection = con;
                cmdInfoNumber.CommandType = CommandType.Text;
                cmdInfoNumber.CommandText = @"Select   InfoNumberPhone.ID as N'ID',
                                                        PhoneNumber as N'PhoneNumber',
                                                        Department_Name as N'Department_Name',
                                                        Department_Des as N'Department_Des'
                                                    from InfoNumberPhone";

                da.SelectCommand = cmdInfoNumber;
                dtInfoNumberPhone.Clear();
                da.Fill(dtInfoNumberPhone);

                int rowInfoNumber = dtInfoNumberPhone.Rows.Count;
                DataRow[] dtrowInfoNumber = dtInfoNumberPhone.Select();

                List<InfoNumberPhone> InfoNumberTable = new List<InfoNumberPhone>();

                for (int i = 0; i < rowInfoNumber; i++)
                {
                    InfoNumberPhone infoNumberTable = new InfoNumberPhone();
                    infoNumberTable.Id = Convert.ToInt32(dtrowInfoNumber[i]["ID"].ToString());
                    infoNumberTable.PhoneNumber = dtrowInfoNumber[i]["PhoneNumber"].ToString();
                    infoNumberTable.DepartmentName = dtrowInfoNumber[i]["Department_Name"].ToString();
                    infoNumberTable.DepartmentDes = dtrowInfoNumber[i]["Department_Des"].ToString();
                    InfoNumberTable.Add(infoNumberTable);
                }

                int totalInfoNumberTable = InfoNumberTable.Count;


                //-------------Finish get InfoNumberPhone------------

                //-------------Create table PriceList from Database--------------
                SqlCommand cmdPriceList = new SqlCommand();
                cmdPriceList.Connection = con;
                cmdPriceList.CommandType = CommandType.Text;
                cmdPriceList.CommandText = @"Select   PriceList.ID as N'ID',
                                                        Category as N'Category',
                                                        NumberHeader as N'NumberHeader',
                                                        Minute as N'Minute',
                                                        Block6 as N'Block6',
                                                        Second as N'Second',
                                                        Type as N'Type'
                                                    from PriceList";

                da.SelectCommand = cmdPriceList;
                dtPriceList.Clear();
                da.Fill(dtPriceList);

                int rowPriceList = dtPriceList.Rows.Count;
                DataRow[] dtrowPriceList = dtPriceList.Select();

                List<PriceList> PriceListTable = new List<PriceList>();

                for (int i = 0; i < rowPriceList; i++)
                {
                    PriceList priceListTable = new PriceList();
                    priceListTable.Category = dtrowPriceList[i]["Category"].ToString();
                    priceListTable.Minute = dtrowPriceList[i]["Minute"].ToString();
                    priceListTable.Block6 = dtrowPriceList[i]["Block6"].ToString();
                    priceListTable.Second = dtrowPriceList[i]["Second"].ToString();
                    priceListTable.Type = dtrowPriceList[i]["Type"].ToString();
                    PriceListTable.Add(priceListTable);
                }

                int totalPriceListTable = PriceListTable.Count;
                //-------------Finish get PriceList------------


                //-------------Create table Number International from Database--------------
                List<NumberInternational> NumberInternationalTablie = new List<NumberInternational>();

                //-------------Finish create table Number International----------------------
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

                        //---------------Checking to match Partition-------------
                        /*SqlCommand getCommand = new SqlCommand();
                        getCommand.Connection = con;
                        getCommand.CommandType = CommandType.Text;
                        getCommand.CommandText = @"Select   Partition.ID as N'ID',
                                                        Name as N'Name' from Partition 
                                            where (Name = '" + finalCalledPartyNumberPartition + "')";

                        da.SelectCommand = getCommand;
                        dtFilterPartition.Clear();
                        da.Fill(dtFilterPartition);
                        */
                        bool partitionFound = false;
                        int partitionIndex = 0; 
                        for (int partitionCount = 0; partitionCount < totalPartitionTable; partitionCount++)
                        {
                            if (PartitionTable[partitionCount].Name == finalCalledPartyNumberPartition)
                            {
                                partitionIndex = partitionCount;
                                partitionFound = true;
                                break;
                            }
                        }

                        if (partitionFound)
                        {
                            DataRow[] dtrow = dtPartition.Select();
                            finalCalledPartyNumberPartitionID = PartitionTable[partitionIndex].Id;  
                            //-----------------Finish get partition---------------

                            //---------------------Info Number-------------------
                            /*SqlCommand getCommand = new SqlCommand();
                            getCommand.Connection = con;
                            getCommand.CommandType = CommandType.Text;
                            getCommand.CommandText = @"Select   InfoNumberPhone.ID as N'ID',
                                                        PhoneNumber as N'PhoneNumber',
                                                        Department_Name as N'Department_Name',
                                                        Department_Des as N'Department_Des'
                                                    from InfoNumberPhone 
                                            where (PhoneNumber = '" + callingPartyNumber + "')";

                            da.SelectCommand = getCommand;
                            dtPartition.Clear();
                            da.Fill(dtPartition);
                            */

                            bool InfoNumberFound = false;
                            int InfoNumberIndex = 0;
                            for (int infoNumberCount = 0; infoNumberCount < totalInfoNumberTable; infoNumberCount++)
                            {
                                if (InfoNumberTable[infoNumberCount].PhoneNumber == callingPartyNumber)
                                {
                                    InfoNumberIndex = infoNumberCount;
                                    InfoNumberFound = true;
                                    break;
                                }
                            }

                            //-------------Checking to match InfoNumber---------
                            if (InfoNumberFound)
                            {
                                DataRow[] dtrow2 = dtPartition.Select();
                                callingPartyNumberID = InfoNumberTable[InfoNumberIndex].Id;
                                //-------------------Checking to get Full Information of Calling: authCodeDescription
                                if (authCodeDescription == "")
                                {
                                    authCodeDescription = InfoNumberTable[InfoNumberIndex].DepartmentName;
                                }
                                //-------------------Finish to get full Information

                                dtPartition.Clear();
                                //-------------------Finish get calling ID--------------------

                                //-----------Computing the total charging for each calling----------
                                /*getCommand = new SqlCommand();
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
                                dtPartition.Clear();
                                da.Fill(dtPartition);
                                 */

                                bool PriceListFound = false;
                                int PriceListIndex = 0;
                                for (int priceListCount = 0; priceListCount < totalPriceListTable; priceListCount++)
                                {
                                    if (PriceListTable[priceListCount].Category == finalCalledPartyNumberPartition)
                                    {
                                        PriceListIndex = priceListCount;
                                        PriceListFound = true;
                                        break;
                                    }
                                }

                                if (PriceListFound)
                                {
                                    //DataRow[] dtrow3 = dtPartition.Select();
                                    // case (dtrow3[0]["Category"].ToString()):
                                    //String type_call = dtrow3[0]["Category"].ToString();
                                    int type_call = Convert.ToInt32(PriceListTable[PriceListIndex].Type);
                                    if (type_call == 0)
                                    {
                                        int _minute = Convert.ToInt32(duration) / 60;
                                        int temp = Convert.ToInt32(duration) % 60;
                                        int _block = temp / 6;
                                        int _second = temp % 6;

                                        totalCharging = _minute * Convert.ToDouble(PriceListTable[PriceListIndex].Minute) +
                                            _block * Convert.ToDouble(PriceListTable[PriceListIndex].Block6) +
                                            _second * Convert.ToDouble(PriceListTable[PriceListIndex].Second);

                                    }
                                    else
                                    {
                                        int _minute = Convert.ToInt32(duration) / 60;
                                        if (Convert.ToDouble(duration) % 60 > 0)
                                            _minute++;
                                        else { }

                                        if (type_call == 2)
                                        {
                                            /*/----------Notice............
                                            bool found = false;
                                            for (int i = 0; i < dtPartition.Rows.Count; i++)
                                            {
                                                if (PriceListTable[PriceListIndex].NumberHeader.IndexOf(finalCalledPartyNumber) != -1)
                                                {
                                                    totalCharging = _minute * Convert.ToDouble(dtrow3[i]["Minute"].ToString());
                                                    found = true;
                                                    break;
                                                }

                                            }
                                            List<NumberInternational> test;
                                            if (!found)
                                                for (int i = 0; i < dtPartition.Rows.Count; i++)
                                                    if (dtrow3[i]["NumberHeader"].ToString() == "0")
                                                        totalCharging = _minute * Convert.ToDouble(dtrow3[i]["Minute"].ToString());
                                            continue;
                                        }


                                           */

                                        }
                                        totalCharging = _minute * Convert.ToDouble(PriceListTable[PriceListIndex].Minute);

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

    public class NumberInternational
    {
        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string minute;

        public string Minute
        {
            get { return minute; }
            set { minute = value; }
        }
    }

    public class PartitionInfo
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class InfoNumberPhone
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        private string departmentName;

        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }
        private string departmentDes;

        public string DepartmentDes
        {
            get { return departmentDes; }
            set { departmentDes = value; }
        }



    }
    public class PriceList
    {
        private string category;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        private string minute;

        public string Minute
        {
            get { return minute; }
            set { minute = value; }
        }
        private string block6;

        public string Block6
        {
            get { return block6; }
            set { block6 = value; }
        }
        private string second;

        public string Second
        {
            get { return second; }
            set { second = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
