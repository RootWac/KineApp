using AM.Widget.WPF;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyKinéApp
{
    class GoogleCalendar
    {
        static CalendarService service;
        public static Events events;

        public static void Initialize()
        {
            /*
            try
            {
                var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "751906553217-v8o5hdbi7q299ag6f691faupc212g1hc.apps.googleusercontent.com",
                    ClientSecret = "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",
                },
                new[] { CalendarService.Scope.Calendar },
                "centremadi.assistance@gmail.com",
                CancellationToken.None).Result;

                // Create Google Calendar API service.
                service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Kine",
                });
            }
            catch (Exception ex) { DataBase.Log(ex.ToString()); }*/

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                ClientId = "797471231356-87erjq7gtb5khv2aajtt0c9hbm76nhp9.apps.googleusercontent.com",
                ClientSecret = "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",
            },
            new[] { CalendarService.Scope.Calendar },
            "madieaglevizion@gmail.com",
            CancellationToken.None,
            new FileDataStore("GoogleAnalyticsApiConsole")).Result;

            // Create Google Calendar API service.
            service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "KNApp",
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Update()
        {
            var calendar = service.CalendarList.List().Execute().Items[0];
            events = service.Events.List(calendar.Id).Execute();
        }

        public static void Sync(List<Meeting> Meet)
        {
            try
            {
                var calendar = service.CalendarList.List().Execute().Items[0];
                var allevents = service.Events.List(calendar.Id).Execute();
                var allevents1 = service.Events.List(calendar.Id).Execute();

                foreach (var met in Meet)
                    try
                    {
                        Update(allevents, calendar.Id, met);
                    }catch(Exception ex) { DataBase.Log(ex.ToString()); }

            }
            catch (Exception ex) { DataBase.Log(ex.ToString()); }
        
        }

        public static Event SearchEvent(Events allevents, Meeting met)
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

                    if ((int)met.MeetingID == MeetingID)
                    {
                        return ev;
                    }
                }
                catch (Exception)
                { }

            }

            return null;
        }
        public static void Update(Meeting met)
        {
            var calendar = service.CalendarList.List().Execute().Items[0];
            var allevents = service.Events.List(calendar.Id).Execute();
            Update(allevents, calendar.Id, met);

        }


        public static void Update(Events allevents, string CalendarId, Meeting met)
        {
            Event event1 = SearchEvent(allevents, met);
            bool isExist = event1 != null;

            if (isExist)
            {
                event1.Start.DateTime = met.Begin;
                event1.End.DateTime = met.End;
                event1.Description = met.Notes;
                event1.Summary = "#" + met.MeetingID + " - " + met.PatientName + " - " + met.EventName;
                service.Events.Update(event1, CalendarId, event1.Id).Execute();
                // Update values (Meet or event) depending on last update
            }
            else
            {
                AddEvent(met);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void AddEvent(Meeting met)
        {
            var calendar = service.CalendarList.List().Execute().Items[0];

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
            Update();
        }
    }

}
