using AM.Widget.WPF;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyKinéApp
{
    class DataBase
    {
        //public static string SQLServerIP = "192.168.0.180";
        public static string SQLServerIP = "127.0.0.1";
        public static string ID = "Admin";
        public static string Database = "Database";
        public static string Password = "Wydad2005";
        public static string PatientTableName = "patients";
        public static string UserTableName = "utilisateurs";
        public static string PatientClinicalFollowUp = "patients_suiviclinique";
        public static string Appointement = "appointements";
        
        /// <summary>
        /// 
        /// </summary>
        public static void ReadFile()
        {
            try
            {
                var dat = File.ReadAllText("SQLConfig.txt").Split(';');
                SQLServerIP = dat[0];
                Database = dat[1];
            }
            catch (Exception)
            {}
        }

        /// <summary>
        /// Returns the string used to connect to database
        /// </summary>
        /// <returns></returns>
        internal static string getConnectionQuery()
        {
            return "SERVER = " + SQLServerIP + "; DATABASE = " + Database + "; UID = " + ID + "; PASSWORD =" + Password + "; SslMode=None";
        }

        ////////////////////////////////////////////////////////////// Add //////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Lastname"></param>
        /// <param name="Adress"></param>
        /// <param name="Phone"></param>
        public static bool AddPatient(Patient Value)
        {
            try
            {
                // Connect to location
                MySqlConnection connection = new MySqlConnection(getConnectionQuery());
                connection.Open();

                // Update DB
                MySqlCommand InsertQuery = connection.CreateCommand();
                InsertQuery.CommandText = "Insert into `" + Database + "`.`" + PatientTableName + "` (`Prenom`, `Nom`, `Tel`, `Addresse`, `CIN`, `Date de naissance`, `Poids`, `Taille`, `Sexe`) Values ('" + Value.Name + "'" + ", '" + Value.LastName + "'" + ", '" + Value.PhoneNumber + "'" + ", '" + Value.Address + "'" + ", '" + Value.CIN + "'" + ", '" + Value.Birthday.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ", " + Value.Weight + ", " + Value.Height + ", '" + Value.Gender + "'"+ ");";
                int rowCount = InsertQuery.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DateStart"></param>
        /// <param name="DateEnd"></param>
        /// <param name="D_Filter"></param>
        /// <returns></returns>
        public static bool CheckPassword(string Username, string Password)
        {
            int Value = -1;
            string sql = "SELECT * FROM " + Database + "." + UserTableName + " Where `utilisateur` = '" + Username + "' AND `mot de passe` = '" + Password + "';";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(getConnectionQuery()))
                {
                    connection.Open();
                    using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
                    {
                        //Execute the command
                        Value = Convert.ToInt32(cmdSel.ExecuteScalar());
                    }
                    connection.Close();
                }
            }
            catch (Exception) { }

            return Value > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DateStart"></param>
        /// <param name="DateEnd"></param>
        /// <param name="D_Filter"></param>
        /// <returns></returns>
        public static DataTable GetPatients(ref Dictionary<string, string> D_Filter)
        {
            DataTable dt = null;

            string sql = "";
            if (D_Filter.Count > 0)
            {
                sql = "SELECT * FROM " + Database + "." + PatientTableName + " Where " + string.Join(" AND ", D_Filter.Values) + ";";
            }
            else
            {
                sql = "SELECT * FROM " + Database + "." + PatientTableName + ";";
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(getConnectionQuery()))
                {
                    connection.Open();
                    using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
                    {
                        dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                        da.Fill(dt);
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                System.Windows.MessageBox.Show(ex.ToString());
            }

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DateStart"></param>
        /// <param name="DateEnd"></param>
        /// <param name="D_Filter"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetPatientsNames()
        {
            Dictionary<int, string> PatientsNames = new Dictionary<int, string>();

            string sql = "SELECT id, Prenom, Nom FROM " + Database + "." + PatientTableName + ";";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(getConnectionQuery()))
                {
                    connection.Open();
                    using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
                    {
                        MySqlDataReader myReader = cmdSel.ExecuteReader();

                        while (myReader.Read())
                        {
                            PatientsNames[(int)myReader[0]] = (string)myReader[1] + " " + (string)myReader[2];
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }

            return PatientsNames;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="DateStart"></param>
        /// <param name="DateEnd"></param>
        /// <param name="D_Filter"></param>
        /// <returns></returns>
        public static Patient GetPatients(int ID)
        {
            Patient SearchPatient = null;
            string sql = "SELECT * FROM " + Database + "." + PatientTableName + " Where id = " + ID + ";";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(getConnectionQuery()))
                {
                    connection.Open();
                    using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
                    {
                        //Execute command
                        //cmdSel.ExecuteNonQuery();

                        //Get the DataReader from the comment using ExecuteReader
                        MySqlDataReader myReader = cmdSel.ExecuteReader();

                        while (myReader.Read())
                        {
                            var test = (string)myReader[5];
                            SearchPatient = new Patient((string)myReader[1], (string)myReader[2], (string)myReader[3], (string)myReader[4], (string)myReader[5], (DateTime)myReader[6], (double)myReader[7], (double)myReader[8], (string)myReader[9]);
                            SearchPatient.ID = ID;
                        }
                    }
                    connection.Close();

                    GetPatients_ClinicalFollowUp(SearchPatient);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return SearchPatient;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="DateStart"></param>
        /// <param name="DateEnd"></param>
        /// <param name="D_Filter"></param>
        /// <returns></returns>
        public static void GetPatients_ClinicalFollowUp(Patient Value)
        {
            string sql = "SELECT * FROM " + Database + "." + PatientClinicalFollowUp + " Where id_patient = " + Value.ID + ";";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(getConnectionQuery()))
                {
                    connection.Open();
                    using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
                    {
                        //Get the DataReader from the comment using ExecuteReader
                        MySqlDataReader myReader = cmdSel.ExecuteReader();

                        while (myReader.Read())
                        {
                            Value.Diagnostic = (string)myReader[2];
                            Value.BalanceSheet = (string)myReader[3];
                            Value.Treatment = (string)myReader[4];
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex){}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Lastname"></param>
        /// <param name="Adress"></param>
        /// <param name="Phone"></param>
        public static bool UpdatePatient_ClinicalFollowUp(Patient Value)
        {
            try
            {
                // Connect to location
                MySqlConnection connection = new MySqlConnection(getConnectionQuery());
                connection.Open();

                // Update DB
                MySqlCommand InsertQuery = connection.CreateCommand();
                InsertQuery.CommandText = "Insert into `" + Database + "`.`" + PatientClinicalFollowUp + "` (`id_patient`, `Diagnostic`, `Bilan`, `Traitement`) Values (" + Value.ID + ", '" + Value.Diagnostic + "', '" + Value.BalanceSheet + "', '" + Value.Treatment + "') ON DUPLICATE KEY UPDATE Diagnostic = VALUES(Diagnostic), Bilan = VALUES(Bilan), Traitement = VALUES(Traitement);";
                int rowCount = InsertQuery.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="DateStart"></param>
        /// <param name="DateEnd"></param>
        /// <param name="D_Filter"></param>
        /// <returns></returns>
        public static List<Meeting> GetAppointements()
        {
            List<Meeting> MeeetingsData = new List<Meeting>();
            string sql = "SELECT * FROM " + Database + "." + Appointement + ";";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(getConnectionQuery()))
                {
                    connection.Open();
                    using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
                    {
                        //Get the DataReader from the comment using ExecuteReader
                        MySqlDataReader myReader = cmdSel.ExecuteReader();

                        //Id, PatientID, EventName, Notes, Begin, End, Color, Location, RecurrenceData, RecurrenceRule.
                        while (myReader.Read())
                        {
                            Meeting Data = new Meeting((int)myReader[0]);
                            //EventName, Begin, End, Location, Notes.
                            Data.UpdateMeeting(myReader[2].ToStringOrEmpty(), (DateTime)myReader[4], (DateTime)myReader[5], myReader[7].ToStringOrEmpty(), myReader[3].ToStringOrEmpty());

                            Data.PatientID = (int)myReader[1];
                            if (MainWindow.PatientsNames.ContainsKey(Data.PatientID)) Data.PatientName = MainWindow.PatientsNames[Data.PatientID];
                            var converter = new BrushConverter();
                            Data.Color = (Brush)converter.ConvertFromString((string)myReader[6]);
                            Data.SetRecurrenceRules(myReader[8].ToStringOrEmpty());

                            MeeetingsData.Add(Data);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return MeeetingsData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Lastname"></param>
        /// <param name="Adress"></param>
        /// <param name="Phone"></param>
        public static bool UpdateAppointement(Meeting Value)
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
                    InsertQuery.CommandText = "UPDATE `" + Database + "`.`" + Appointement + "` SET id_patient=@id_patient, Name=@Name, Notes =@Notes, BeginDate =@BeginDate, EndDate =@EndDate, Color =@Color, Location =@Location, RecurrenceData =@RecurrenceData WHERE id =@id";
                    InsertQuery.Parameters.AddWithValue("@id", Value.MeetingID);
                }
                else
                {
                    InsertQuery.CommandText = "INSERT INTO " + Database + "." + Appointement + " (id_patient, Name, Notes, BeginDate, EndDate, Color, Location, RecurrenceData) VALUES(@id_patient, @Name, @Notes, @BeginDate, @EndDate, @Color, @Location, @RecurrenceData)";
                }

                InsertQuery.Parameters.AddWithValue("@id_patient", Value.PatientID);
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
                System.Windows.MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        public static bool DeleteAppointement(int Value)
        {
            try
            {
                // Connect to location
                MySqlConnection connection = new MySqlConnection(getConnectionQuery());
                connection.Open();

                // Update DB
                MySqlCommand InsertQuery = connection.CreateCommand();
                InsertQuery.CommandText = "Delete From `" + Database + "`.`" + Appointement + "` Where `id` = " + Value;
                InsertQuery.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        
    }

    public static class Extension
    {
        public static string ToStringOrEmpty(this Object value)
        {
            return value == null ? "" : value.ToString();
        }
    }
}
