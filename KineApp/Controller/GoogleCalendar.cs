using AM.Widget.WPF;
using K4os.Compression.LZ4.Internal;
using System;
using System.Collections.Generic;
using System.Management.Instrumentation;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using KineApp.Model;
using System.Linq;
using LiveChartsCore.Geo;
using System.Windows;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json.Linq;

namespace KineApp.Controller
{
    class GoogleCalendar
    {
        static CalendarService service;
        public static Events events;
        public static Dictionary<string, string> colors = new Dictionary<string, string>();
        public static bool SyncInProgress = false;
        public static bool StopSync = false;

        /// <summary>
        /// 
        /// </summary>
        public static bool Init()
        {
            try
            {
                var data = FileIO.ReadGoogleCalendar();
                string id = data.Item1, secret = data.Item2, email = data.Item3;

                var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = id,
                    ClientSecret = secret,
                },
                new[] { CalendarService.Scope.Calendar },
                email,
                CancellationToken.None).Result;

                // Create Google Calendar API service.
                service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "KNApp",
                });


                var colorsevents = service.Colors.Get().Execute().Event__;

                // Print available event colors.
                foreach (KeyValuePair<string, ColorDefinition> color in colorsevents)
                {
                    colors[color.Value.Background.ToUpper()] = color.Key;
                }
            }
            catch (Exception ex)
            {
                var result = MessageBox.Show("Probleme survenue lors de la connexion au serveur Google. Voulez-vous continuez a essayer de vous connecter a Google calendar ?", "Erreur", MessageBoxButton.YesNo);

                if(result == MessageBoxResult.Yes)
                {
                    return Init();
                }
                else { return false; }

            }

            return true;
        }

        /// <summary>
        /// Synchronize all the events in the database with google calendar
        /// </summary>
        /// <param name="Meet"></param>
        internal static void Sync(List<Meeting> Meet)
        {
            while(SyncInProgress)
            {
                StopSync = true;
                Thread.Sleep(1000);
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(Sync), Meet);
        }

        internal static void Sync(object meetObject)
        {
            try
            {
                SyncInProgress=true;

                List<Meeting> Meet = (List<Meeting>)meetObject;
                var calendar = GetCalendar();
                var allevents = service.Events.List(calendar.Id).Execute();

                foreach (var met in Meet)
                {
                    if (StopSync) break;
                    UpdateOrInsert(allevents, calendar.Id, met);
                }

                SyncInProgress =false;
            }
            catch (Exception ex) { Log.Write(ex.ToString(), LogStatus.Critical); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        internal static void Delete(int value)
        {
            var calendar = GetCalendar();
            var allevents = service.Events.List(calendar.Id).Execute();

            Event event1 = SearchEvent(allevents, value);
            if(event1 != null) service.Events.Delete(calendar.Id, event1.Id).Execute();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        internal static void Update(Meeting value)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Update), value);
            /*
            var calendar = GetCalendar();
            var allevents = service.Events.List(calendar.Id).Execute();
            UpdateOrInsert(allevents, calendar.Id, value);*/
        }

        internal static void Update(object meetObject)
        {
            try
            {
                Meeting value = (Meeting)meetObject;
                var calendar = GetCalendar();
                var allevents = service.Events.List(calendar.Id).Execute();
                UpdateOrInsert(allevents, calendar.Id, value);
            }
            catch (Exception ex) { Log.Write(ex.ToString(), LogStatus.Critical); }
        }

        #region private
        /// <summary>
        /// Get the appropriate calendar
        /// </summary>
        /// <returns></returns>
        private static CalendarListEntry GetCalendar()
        {
            return service.CalendarList.List().Execute().Items.Where(var => var.Summary == "Center").First();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void Refresh()
        {
            var calendar = GetCalendar();
            events = service.Events.List(calendar.Id).Execute();
        }

        /// <summary>
        /// Update if the event already exist in google calendar, else call the add event method 
        /// </summary>
        /// <param name="allevents"></param>
        /// <param name="CalendarId"></param>
        /// <param name="met"></param>
        private static void UpdateOrInsert(Events allevents, string CalendarId, Meeting met)
        {
            try
            {
                Event event1 = SearchEvent(allevents, (int)met.MeetingID);
                bool isExist = event1 != null;

                if (isExist)
                {
                    event1.Start.DateTime = met.Begin;
                    event1.End.DateTime = met.End;
                    event1.Description = met.Notes;
                    event1.Summary = "#" + met.MeetingID + " - " + met.PatientName + " - " + met.EventName;
                    string color = met.Color.ToString();
                    color = color.Substring(0, 1) + color.Substring(3, 6);
                    if (colors.ContainsKey(color)) event1.ColorId = colors[color];
                    service.Events.Update(event1, CalendarId, event1.Id).Execute();
                    // Update values (Meet or event) depending on last update
                }
                else
                    AddEvent(met);
            }
            catch (Exception ex) { Log.Write(ex.ToString()); }
        }

        /// <summary>
        /// Add an events in google calendar "Center"
        /// </summary>
        /// <param name="met"></param>
        private static void AddEvent(Meeting met)
        {
            var calendar = GetCalendar();
            var ev = new Event();

            EventDateTime start = new EventDateTime();
            start.DateTime = met.Begin;

            EventDateTime end = new EventDateTime();
            end.DateTime = met.End;

            ev.Start = start;
            ev.End = end;
            ev.Description = met.Notes;
            ev.Summary = "#" + met.MeetingID + " - " + met.PatientName + " - " + met.EventName;
            //ev.ColorId = met.Color.ToString();

            service.Events.Insert(ev, calendar.Id).Execute();
            Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allevents"></param>
        /// <param name="met"></param>
        /// <returns></returns>
        private static Event SearchEvent(Events allevents, int meeting_id)
        {
            foreach (var ev in allevents.Items)
            {
                try
                {
                    var SummarySplit = ev.Summary.Split(new string[] { " - " }, StringSplitOptions.None);
                    var MeetingID = int.Parse(SummarySplit[0].Substring(1));
                    var PatientName = SummarySplit[2];
                    var Title = SummarySplit[2];
                    var Note = ev.Description;
                    var StartDate = ev.Start.DateTime;
                    var EndDate = ev.End.DateTime;

                    if (meeting_id == MeetingID)
                    {
                        return ev;
                    }
                }
                catch (Exception)
                { }

            }

            return null;
        }
        #endregion
    }
}
