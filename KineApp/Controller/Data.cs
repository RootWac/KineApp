using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using KineApp.Model;
using AM.Widget.WPF;
using Google.Protobuf.WellKnownTypes;
using System.Linq;

namespace KineApp.Controller
{
    class Data
    {
        public static double ZOOM = 1;

        public static bool IsGoogleCalendarActivated;

        /// <summary>
        /// List of patient data from local database
        /// </summary>
        public static List<Patient> L_Patients = new List<Patient>();

        /// <summary>
        /// List of meeting data inside local calendar
        /// </summary>
        public static List<Meeting> L_Events = new List<Meeting>();


        /// <summary>
        /// 
        /// </summary>
        public static void Initialize(bool UseGoogle = true)
        {
            ZOOM = System.Windows.SystemParameters.PrimaryScreenWidth / 1920;

            UpdatePatients();

            IsGoogleCalendarActivated = UseGoogle;

            if (UseGoogle)
                IsGoogleCalendarActivated = GoogleCalendar.Init();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Type"></param>
        /// <param name="Discount"></param>
        /// <returns></returns>
        internal static bool PayrollUpdate(int Id, DiscountEnum Type, double Discount, string Comment, double Amount)
        {
            return SQL.Connection.PayrollUpdate(Id, Type, Discount, Comment, Amount);
        }


        #region Patient (Add, Get)
        /// <summary>
        /// 
        /// </summary>
        public static bool AddPatient(Patient NewPatient)
        {
            if (NewPatient.FirstName == "" || NewPatient.LastName == "") return false;

            if (SQL.Connection.AddPatient(NewPatient))
            {
                L_Patients.Add(NewPatient);
                OnNewPatient(NewPatient);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetPatientsNames()
        {
            Dictionary<int, string> PatientsNames = new Dictionary<int, string>();
            foreach(var pat in L_Patients)
            {
                if(pat.CurrentRecord != null)
                    PatientsNames[pat.CurrentRecord.Id] = pat.CompleteName;
            }
            return PatientsNames;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void UpdatePatients()
        {
            L_Patients = SQL.Connection.GetPatients();
            GetAppointements();
            SQL.Connection.GetRecord(L_Patients);

        }
        #endregion

        #region Appointement

        public static void AddAppointementToPatient(Meeting value)
        {
            //L_Patients.Where(var => var.CurrentRecord != null && var.CurrentRecord.Id == value.PatientID).ToList().ForEach(var => var.CurrentRecord.AddAppointement(value));
            L_Patients.Where(var => var.CurrentRecord != null).ToList().ForEach(var => var.CurrentRecord.AddAppointement(value));
        }

        /// <summary>
        /// When the event is added or updated from the calendar
        /// </summary>
        /// <param name="value"></param>
        internal static void AddAppointement_Event(Meeting value)
        {
            if (SQL.Connection.UpdateAppointement(value))
            {
                AddAppointementToPatient(value);

                OnUpdatePatient();

                //update google calendar
                if (IsGoogleCalendarActivated) GoogleCalendar.Update(value);

                OnUpdateCalendar();
            }
             
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        internal static void DeleteAppointement(int value)
        {
            //Remove from google (the order is important)
            if (SQL.Connection.DeleteAppointement(value))
            {
                // To do : add method to avoid delete if it's not saved in database. second is to remove the appoitment from the currrent record
                OnUpdatePatient();

                if (IsGoogleCalendarActivated) GoogleCalendar.Delete(value);

                OnUpdateCalendar();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static List<Meeting> GetAppointements()
        {
            var list = SQL.Connection.GetAppointements();

            foreach (var item in list) { AddAppointementToPatient(item); }

            if (IsGoogleCalendarActivated)
                GoogleCalendar.Sync(list);

            L_Events = list;

            return list;
        }
        #endregion

        #region Record
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SelectedPatient"></param>
        /// <param name="sessionnumber"></param>
        /// <param name="price"></param>
        internal static void AddRecord(Patient SelectedPatient, int sessionnumber, int price, string follow, string balancesheet)
        {
            if (SQL.Connection.CreateRecord(SelectedPatient, price, sessionnumber, follow, balancesheet))
            {
                SQL.Connection.GetRecord(SelectedPatient);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedPatient"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        internal static void UpdateRecord(Patient selectedPatient, int sessionnumber, int price, string follow, string balancesheet)
        {
            if (SQL.Connection.UpdateRecord(selectedPatient, price, sessionnumber, follow, balancesheet))
            {
                SQL.Connection.GetRecord(selectedPatient);
            }        
        }
        #endregion

        internal static void AddSession(Patient SelectedPatient, string Title, string Price, Meeting meet, double SessionTime, string Description)
        {
            if (SQL.Connection.AddSession(SelectedPatient, Title, Price, meet, SessionTime, Description))
            {
                SQL.Connection.GetSessions(SelectedPatient);
            }
        }


        #region Events handler
        public static event EventHandler NewPatient;

        private static void OnNewPatient(Patient Patient)
        {
            NewPatient?.Invoke(Patient, null);
        }

        public static event EventHandler UpdatePatient;

        private static void OnUpdatePatient()
        {
            UpdatePatient?.Invoke(null, null);
        }

        public static event EventHandler UpdateCalendar;

        private static void OnUpdateCalendar()
        {
            UpdateCalendar?.Invoke(null, null);
        }
        #endregion

    }

    public static class Extension
    {
        public static string ToStringOrEmpty(this Object value)
        {
            return value == null ? "" : value.ToString();
        }
    }
}
