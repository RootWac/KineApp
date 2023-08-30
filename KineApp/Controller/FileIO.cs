using Google.Apis.Auth.OAuth2;
using KineApp.Model;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KineApp.Controller
{
    class FileIO
    {
        static string AppDataFileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\KineApp\\";

        /// <summary>
        /// 
        /// </summary>
        public static void ReadSQLFile()
        {
            try
            {         
                JObject obj = JObject.Parse(File.ReadAllText(AppDataFileName + "SQL.txt"));
                SQL.Connection.Init(obj["IP"].ToString(), obj["ID"].ToString(), obj["Password"].ToString(), (int)obj["Port"]);
            }
            catch (Exception ex)
            {
                Log.Write("Error while reading the SQL file :" + ex.ToString(), LogStatus.Critical);
            }
        }

        public static (string, string, string) ReadGoogleCalendar()
        {
            try
            {
                JObject obj = JObject.Parse(Encryption.Read(AppDataFileName + "GoogleCalendar.txt"));
                return (obj["ClientId"].ToString(), obj["ClientSecret"].ToString(), obj["Email"].ToString());
            }
            catch (Exception ex)
            {
                Log.Write("Error while reading the GoogleCalendar file :" + ex.ToString(), LogStatus.Error, true);
            }

            return ("", "", "");
        }

        public static void WriteGoogleCalendar()
        {
            try
            {
                JObject obj = new JObject();
                obj.Add("ClientId", "751906553217-v8o5hdbi7q299ag6f691faupc212g1hc.apps.googleusercontent.com");
                obj.Add("ClientSecret", "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");
                obj.Add("Email", "centremadi.assistance@gmail.com");

                Encryption.Write("GoogleCalendar.txt", obj.ToString());
            }
            catch (Exception ex)
            {
                Log.Write("Error while writing googlecalendar information :" + ex.ToString(), LogStatus.Critical);
            }
        }
    }
}
