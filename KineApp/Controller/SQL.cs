using AM.Widget.WPF;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using KineApp.Model;
using System.Linq;
using Google.Protobuf.WellKnownTypes;
using LiveChartsCore.SkiaSharpView.Painting;
using Newtonsoft.Json.Linq;
using System.Configuration.Provider;

namespace KineApp.Controller
{
    class SQL
    {
        public static SQL Connection = new SQL();
        public static bool SQLisActivated = true;

        /// <summary>
        /// Connection variable
        /// </summary>
        string ID = "root";
        string Password = "EVA2017";
        string Database = "center";
        string IP = "127.0.0.1";
        int Port;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="ID"></param>
        /// <param name="Password"></param>
        /// <param name="Port"></param>
        public void Init(string IP, string ID, string Password, int Port)
        {
            this.IP = IP;
            this.ID = ID;
            this.Password = Password;
            this.Port = Port;
        }

        /// <summary>
        /// Returns the string used to connect to database
        /// </summary>
        /// <param name="TimeOut"> Set the time(Seconde) to wait while trying to establish a connection before terminating the attempt and generating an error.</param>
        /// <returns></returns>
        internal string getConnectionQuery(int TimeOut = 15)
        {
            return "SERVER = " + IP + "; DATABASE = " + Database + "; UID = " + ID + "; PASSWORD =" + Password + "; SslMode=none; Connection Timeout= " + TimeOut;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="date"></param>
        internal void AddQueryInLog(string value, DateTime date)
        {

        }

        #region patient
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal List<Patient> GetPatients()
        {
            List<Patient> L_Patients = new List<Patient>();
            string query = "";
            try
            {
                if (SQLisActivated)
                {
                    // Connect to location
                    MySqlConnection connection = new MySqlConnection(getConnectionQuery(3));
                    connection.Open();

                    // Get table
                    query = "SELECT * FROM " + Database + ".patients";

                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = null;
                    dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        int Id = (int)dataReader["id"];
                        string FirstName = dataReader["firstname"].ToString();
                        string LastName = dataReader["lastname"].ToString();
                        string Address = dataReader["address"].ToString();
                        string CIN = dataReader["cin"].ToString();
                        DateTime DateOfBirth = (DateTime)dataReader["dateofbirth"];
                        string Phone = dataReader["phone"].ToString();
                        int Height = (int)dataReader["height"];
                        int Weight = (int)dataReader["weight"];
                        char Gender = dataReader["gender"].ToString()[0];

                        L_Patients.Add(new Patient(Id, FirstName, LastName, Address, CIN, Phone, DateOfBirth, Gender, Height, Weight));
                    }

                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                Log.Write(ex.ToString() + "\n" + query, LogStatus.Critical);
                return null;
            }

            return L_Patients;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPatient"></param>
        internal bool AddPatient(Patient newPatient)
        {
            bool IsSuccess = false;
            string query = "INSERT INTO `" + Database + "`.`patients`(`firstname`, `lastname`, `address`, `cin`, `dateofbirth`, `phone`, `height`, `weight`, `gender`) " +
                "VALUES ('" + newPatient.FirstName + "', '" + newPatient.LastName + "', '" + newPatient.Address + "', '" + newPatient.CIN + "', '" + newPatient.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + newPatient.Phone + "', '" + newPatient.Height + "', '" + newPatient.Weight + "', '" + newPatient.Gender + "'); ";

            try
            {
                if (SQLisActivated)
                {
                    query += "select last_insert_id();";

                    // Connect to location
                    MySqlConnection connection = new MySqlConnection(getConnectionQuery());
                    connection.Open();

                    // Update DB
                    MySqlCommand InsertQuery = connection.CreateCommand();
                    InsertQuery.CommandText = query;
                    InsertQuery.ExecuteNonQuery();
                    int ID = (int)InsertQuery.LastInsertedId;

                    connection.Close();
                    IsSuccess = true;
                }
            }
            catch (MySqlException ex)
            {
                Log.Write(ex.ToString() + "\n" + query, LogStatus.Error);
            }

            if (!IsSuccess) AddQueryInLog(query, DateTime.Now);

            return IsSuccess;
        }
        #endregion

        #region record

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        internal void GetRecord(Patient patient)
        {
            string query = "";
            try
            {
                if (SQLisActivated)
                {

                    // Connect to location
                    MySqlConnection connection = new MySqlConnection(getConnectionQuery(3));
                    connection.Open();

                    query = "SELECT * FROM " +  Database + ".record where patientid = " + patient.Id + ";";

                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    patient.AllRecords.Clear();

                    int id = -1, price = 0, numberprescribedsession = 0;
                    string follow = "", balancesheet = "", title = "";
                    DateTime begin = DateTime.MinValue, end = DateTime.MinValue;
                    while (dataReader.Read())
                    {
                        id = (int)dataReader["id"];
                        title = dataReader["title"].ToString();
                        begin = (DateTime)dataReader["begin"];
                        end = dataReader["end"].ToString() != "" ? (DateTime)dataReader["end"] : DateTime.MinValue;
                        numberprescribedsession = (int)dataReader["numberprescribedsession"];
                        price = (int)dataReader["price"];
                        follow = dataReader["follow"].ToString();
                        balancesheet = dataReader["balancesheet"].ToString();

                        Record record = new Record();

                        if (id > 0 /*&& patient.CurrentRecord == null*/ && end == DateTime.MinValue)
                        {
                            record.InitializeDB(id, title, begin, numberprescribedsession, price, follow, balancesheet);
                            patient.CurrentRecord = record;
                        }
                        else
                            record.InitializeDB(id, title, begin, numberprescribedsession, price, follow, balancesheet, end);

                        patient.AllRecords.Add(id, record);
                    }

                    dataReader.Close();                 
                }
            }
            catch (MySqlException ex)
            {
                Log.Write(ex.ToString() + "\n" + query, LogStatus.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="L_Patients"></param>
        internal void GetRecord(List<Patient> L_Patients)
        {
            foreach (var Value in L_Patients)
            {
                GetRecord(Value);
                GetSessions(Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool CreateRecord(Patient patient, string title, int price, int numberprescribedsession, string follow, string balancesheet)
        {
            try
            {
                // Connect to location
                MySqlConnection connection = new MySqlConnection(getConnectionQuery());
                connection.Open();

                // Update DB
                MySqlCommand InsertQuery = connection.CreateCommand();
                InsertQuery.CommandText = "INSERT INTO " + Database + ".record (patientid, title, price, numberprescribedsession, follow, balancesheet, begin) VALUES(@patientid, @title, @price, @numberprescribedsession, @follow, @balancesheet, now())";

                InsertQuery.Parameters.AddWithValue("@patientid", patient.Id);
                InsertQuery.Parameters.AddWithValue("@title", title);
                InsertQuery.Parameters.AddWithValue("@price", price);
                InsertQuery.Parameters.AddWithValue("@numberprescribedsession", numberprescribedsession);
                InsertQuery.Parameters.AddWithValue("@balancesheet", balancesheet);
                InsertQuery.Parameters.AddWithValue("@follow", follow);

                InsertQuery.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), LogStatus.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        internal bool CloseRecord(Record record)
        {
            try
            {
                // Connect to location
                MySqlConnection connection = new MySqlConnection(getConnectionQuery());
                connection.Open();

                // Update DB
                MySqlCommand InsertQuery = connection.CreateCommand();
                InsertQuery.CommandText = "UPDATE `" + Database + "`.`record` SET end=now() WHERE id =@id";
                InsertQuery.Parameters.AddWithValue("@id", record.Id);

                InsertQuery.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), LogStatus.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        internal bool UpdateRecord(Patient patient, string title, int price, int numberprescribedsession, string follow, string balancesheet)
        {
            try
            {
                // Connect to location
                MySqlConnection connection = new MySqlConnection(getConnectionQuery());
                connection.Open();

                // Update DB
                MySqlCommand InsertQuery = connection.CreateCommand();
                InsertQuery.CommandText = "UPDATE `" + Database + "`.`record` SET price=@price, title=@title, numberprescribedsession=@numberprescribedsession, follow=@follow, balancesheet=@balancesheet WHERE id =@id";
                
                InsertQuery.Parameters.AddWithValue("@id", patient.CurrentRecord.Id);
                InsertQuery.Parameters.AddWithValue("@title", title);
                InsertQuery.Parameters.AddWithValue("@price", price);
                InsertQuery.Parameters.AddWithValue("@numberprescribedsession", numberprescribedsession);
                InsertQuery.Parameters.AddWithValue("@follow", follow);
                InsertQuery.Parameters.AddWithValue("@balancesheet", balancesheet);

                InsertQuery.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), LogStatus.Error);
                return false;
            }
            return true;
        }
        #endregion

        #region Billing
        public DiscountEnum getDiscountType(string type)
        {
            switch (type)
            {
                case "Argent":
                    return DiscountEnum.Argent;

                case "Percentage":
                    return DiscountEnum.Percentage;

                default:
                    return DiscountEnum.None;
            }
        }

        internal bool PayrollUpdate(int id, DiscountEnum type, double discount, string Comment, double amount)
        {
            bool IsSuccess = false;

            // Update local table name
            /*
            string query = "INSERT INTO `center`.`transaction` (`date`, `price`, `discount`, `discounttype`, `ispaied`) " +
                "VALUES ('" + DateTime.Now.ToString("yyyy-MM-dd") +"', '" + amount + "', '" + discount + "', '"+ type.ToString() + "', '0');"*/
            string query = "UPDATE `" + Database + "`.`transaction` SET `ispaied` = 1, `price`=" + amount + ", `comment`='" + Comment + "', `date`=now(), `discount`=" + discount + ", `discounttype`='" + type.ToString() + "' WHERE `id` = '" + id + "'";

            try
            {
                if (SQLisActivated)
                {
                    // Connect to location
                    MySqlConnection connection = new MySqlConnection(getConnectionQuery(3));
                    connection.Open();

                    // Alter DB
                    MySqlCommand InsertQuery = connection.CreateCommand();
                    InsertQuery.CommandText = query;
                    InsertQuery.ExecuteNonQuery();

                    connection.Close();
                    IsSuccess = true;
                }
            }
            catch (MySqlException ex)
            {
                Log.Write(ex.ToString() + "\n" + query, LogStatus.Error);
            }

            if (!IsSuccess) AddQueryInLog(query, DateTime.Now);
            return IsSuccess;
        }
        #endregion

        #region appoitement
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DateStart"></param>
        /// <param name="DateEnd"></param>
        /// <param name="D_Filter"></param>
        /// <returns></returns>
        public List<Meeting> GetAppointements()
        {
            List<Meeting> MeeetingsData = new List<Meeting>();
            string query = "SELECT * FROM " + Database + ".appointements;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(getConnectionQuery()))
                {
                    connection.Open();
                    using (MySqlCommand cmdSel = new MySqlCommand(query, connection))
                    {
                        //Get the DataReader from the comment using ExecuteReader
                        MySqlDataReader myReader = cmdSel.ExecuteReader();

                        //Id, PatientID, EventName, Notes, Begin, End, Color, Location, RecurrenceData, RecurrenceRule.
                        while (myReader.Read())
                        {
                            int record = (int)myReader[1];
                            List<Patient> patients = Data.L_Patients.Where(var => var.CurrentRecord != null && var.CurrentRecord.Id == record).ToList();

                            if (patients.Count > 0)
                            {
                                Meeting meet = new Meeting((int)myReader[0]);
                                //EventName, Begin, End, Location, Notes.
                                meet.UpdateMeeting(myReader[2].ToStringOrEmpty(), (DateTime)myReader[4], (DateTime)myReader[5], myReader[7].ToStringOrEmpty(), myReader[3].ToStringOrEmpty());
                                meet.PatientID = record;
                                meet.PatientName = patients[0].LastName + " " + patients[0].FirstName;

                                var converter = new BrushConverter();
                                meet.Color = (Brush)converter.ConvertFromString((string)myReader[6]);
                                meet.SetRecurrenceRules(myReader[8].ToStringOrEmpty());

                                MeeetingsData.Add(meet);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString() + "\n" + query, LogStatus.Error);
            }

            return MeeetingsData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Lastname"></param>
        /// <param name="Adress"></param>
        /// <param name="Phone"></param>
        public bool UpdateAppointement(Meeting Value)
        {
            try
            {
                // Connect to location
                MySqlConnection connection = new MySqlConnection(getConnectionQuery());
                connection.Open();

                // Update DB
                MySqlCommand InsertQuery = connection.CreateCommand();
                string AppoitementColor = ((Value.Color != null) ? Value.Color.ToString() : Brushes.Black.ToString());
                if ((int)Value.MeetingID != -1)
                {
                    InsertQuery.CommandText = "UPDATE `" + Database + "`.`appointements` SET recordid=@recordid, title=@Name, description=@Notes, begin=@BeginDate, end=@EndDate, color=@Color, location=@Location, recurrencedata=@RecurrenceData WHERE id =@id";
                    InsertQuery.Parameters.AddWithValue("@id", Value.MeetingID);
                }
                else
                {
                    InsertQuery.CommandText = "INSERT INTO " + Database + ".appointements (recordid, title, description, begin, end, color, location, recurrencedata) VALUES(@recordid, @Name, @Notes, @BeginDate, @EndDate, @Color, @Location, @RecurrenceData)";
                }

                InsertQuery.Parameters.AddWithValue("@recordid", Value.PatientID);
                InsertQuery.Parameters.AddWithValue("@Name", Value.EventName);
                InsertQuery.Parameters.AddWithValue("@Notes", Value.Notes);
                InsertQuery.Parameters.AddWithValue("@BeginDate", Value.Begin);
                InsertQuery.Parameters.AddWithValue("@EndDate", Value.End);
                InsertQuery.Parameters.AddWithValue("@Color", Value.Color);
                InsertQuery.Parameters.AddWithValue("@Location", Value.Location);
                InsertQuery.Parameters.AddWithValue("@RecurrenceData", Value.RecurrenceDataValue);

                InsertQuery.ExecuteNonQuery();
                if (Value.MeetingID != null) Value.Create((int)InsertQuery.LastInsertedId);
                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        public bool DeleteAppointement(int Value)
        {
            string query = "Delete From `" + Database + "`.`appointement` Where `id` = " + Value;

            try
            {
                // Connect to location
                MySqlConnection connection = new MySqlConnection(getConnectionQuery());
                connection.Open();

                // Update DB
                MySqlCommand InsertQuery = connection.CreateCommand();
                InsertQuery.CommandText = query;
                InsertQuery.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString() + "\n" + query);
                return false;
            }
            return true;
        }

        #endregion

        #region Session
        internal bool AddSession(Patient selectedPatient, string title, string price, Meeting meet, double sessionTime, string description)
        {
            try
            {
                int result = CreateTransactionId(int.Parse(price));

                // Connect to location
                MySqlConnection connection = new MySqlConnection(getConnectionQuery());
                connection.Open();

                // Update DB
                MySqlCommand InsertQuery = connection.CreateCommand();
                InsertQuery.CommandText = "INSERT INTO " + Database + ".session (recordid, appoitementid, transactionid, timeinminute, date, rate, title, description) VALUES(@recordid, @appoitementid, @transactionid, @timeinminute, now(), @rate, @title, @description)";

                InsertQuery.Parameters.AddWithValue("@recordid", selectedPatient.CurrentRecord.Id);
                InsertQuery.Parameters.AddWithValue("@appoitementid", meet.MeetingID);
                InsertQuery.Parameters.AddWithValue("@transactionid", result);
                InsertQuery.Parameters.AddWithValue("@timeinminute", sessionTime);
                InsertQuery.Parameters.AddWithValue("@rate", price);
                InsertQuery.Parameters.AddWithValue("@title", title);
                InsertQuery.Parameters.AddWithValue("@description", description);

                InsertQuery.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), LogStatus.Error);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        public void GetSessions(Patient patient)
        {
            string query = "";

            try
            {
                if (SQLisActivated && patient.CurrentRecord != null)
                {
                    var list = GetAppointements();

                    patient.CurrentRecord.ListOfSession.Clear();

                    // Connect to location
                    MySqlConnection connection = new MySqlConnection(getConnectionQuery(3));
                    connection.Open();

                    query = "SELECT session.*, discount, discounttype, ispaied, transaction.date as transactiondate  FROM " +  Database + ".session join " +  Database + ".transaction on session.transactionid = transaction.id where recordid = " + patient.CurrentRecord.Id + ";";

                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Session session = new Session();
                        int idtransaction = (int)dataReader["transactionid"];
                        int idappoitement = (int)dataReader["appoitementid"];
                        int idsession = (int)dataReader["id"];
                        int rate = (int)((double)dataReader["rate"]);
                        int discount = (int)((double)dataReader["discount"]);
                        string discounttype = dataReader["discounttype"].ToString();
                        DateTime date = (DateTime)dataReader["date"];
                        DateTime transactiondate = (DateTime)dataReader["transactiondate"];
                        bool isPaied = (bool)dataReader["ispaied"];
                        string title = dataReader["title"].ToString();
                        string description = dataReader["description"].ToString();
                        int timeinmunite = (int)dataReader["timeinminute"];

                        session.Id = idsession;
                        session.SessionTime = TimeSpan.FromMinutes(timeinmunite);
                        session.Title = title;
                        session.Description = description;
                        session.Date = date;
                        session.Bill = new Transaction() { Id = idtransaction, Date = transactiondate, Rate = rate };
                        session.appoint = list.Where(var => (int)var.MeetingID == idappoitement).ToArray()[0];
                        if (isPaied) session.Bill.Pay(getDiscountType(discounttype), discount, true);

                        patient.CurrentRecord.ListOfSession.Add(session);
                    }
                    dataReader.Close();
                }
            }
            catch (MySqlException ex)
            {
                Log.Write(ex.ToString() + "\n" + query);
            }
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private int CreateTransactionId(int price)
        {
            try
            {

                // Connect to location
                MySqlConnection connection = new MySqlConnection(getConnectionQuery());
                connection.Open();

                // Update DB
                MySqlCommand InsertQuery = connection.CreateCommand();
                InsertQuery.CommandText = "INSERT INTO " + Database + ".transaction (date, price, discount, discounttype, ispaied, comment) VALUES(now(), @price, 0, 'None', 0, '')";
                InsertQuery.Parameters.AddWithValue("@price", price);

                InsertQuery.ExecuteNonQuery();
                int ID = (int)InsertQuery.LastInsertedId;

                connection.Close();

                return ID;
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), LogStatus.Error);
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedPatient"></param>
        /// <param name="type"></param>
        /// <param name="filename"></param>
        /// <param name="providerName"></param>
        /// <returns></returns>
        internal bool AddFileNameToPatient(Patient selectedPatient, string type, string filename, string providerName)
        {
            try
            {
                // Connect to location
                MySqlConnection connection = new MySqlConnection(getConnectionQuery());
                connection.Open();

                // Update DB
                MySqlCommand InsertQuery = connection.CreateCommand();
                InsertQuery.CommandText = "INSERT INTO " + Database + ".files (patientid, recordid, type, filename, providername) VALUES(@patientid, @recordid, @type, @filename, @providername)";
                InsertQuery.Parameters.AddWithValue("@patientid", selectedPatient.Id);
                InsertQuery.Parameters.AddWithValue("@recordid", selectedPatient.CurrentRecord.Id);
                InsertQuery.Parameters.AddWithValue("@type", type);
                InsertQuery.Parameters.AddWithValue("@filename", filename);
                InsertQuery.Parameters.AddWithValue("@providername", providerName);

                InsertQuery.ExecuteNonQuery();
                int ID = (int)InsertQuery.LastInsertedId;

                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                Log.Write(ex.ToString(), LogStatus.Error);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l_Patients"></param>
        internal void GetPatientFiles(List<Patient> l_Patients)
        {
            foreach (var Value in l_Patients)
            {
                string query = "";
                try
                {
                    if (SQLisActivated)
                    {

                        // Connect to location
                        MySqlConnection connection = new MySqlConnection(getConnectionQuery(3));
                        connection.Open();

                        query = "SELECT * FROM " +  Database + ".files where patientid = " + Value.Id + ";";

                        //Create Command
                        MySqlCommand cmd = new MySqlCommand(query, connection);

                        //Create a data reader and Execute the command
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        Value.AdditionalFiles.Clear();

                        int recordid = -1;
                        string type, filename, providername;

                        while (dataReader.Read())
                        {
                            recordid = (int)dataReader["recordid"];
                            type = dataReader["type"].ToString();
                            filename = dataReader["filename"].ToString();
                            providername = dataReader["providername"].ToString();

                            
                            Value.AdditionalFiles.Add(new PatientFiles(recordid, type, filename, providername));
                        }

                        dataReader.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    Log.Write(ex.ToString() + "\n" + query, LogStatus.Error);
                }
            }
        }
    }
}
