/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKinéApp
{
    class GoogleCalendar
    {
        static CalendarService service;
        public static Events events;

        public static void Initialize()
        {
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                ClientId = "797471231356-87erjq7gtb5khv2aajtt0c9hbm76nhp9.apps.googleusercontent.com",
                ClientSecret = "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",
            },
            new[] { CalendarService.Scope.Calendar },
            "madieaglevizion@gmail.com",
            CancellationToken.None).Result;

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
            var calendar = service.CalendarList.List().Execute().Items[0];
            var allevents = service.Events.List(calendar.Id).Execute();
            var allevents1 = service.Events.List(calendar.Id).Execute();

            foreach (var met in Meet)
            {
                Event event1 = SearchEvent(allevents, met);
                bool isExist = event1 != null;

                if (isExist)
                {
                    allevents1.Items.Remove(event1);
                    // Update values (Meet or event) depending on last update
                }
                else
                {
                    AddEvent(met);
                }

            }
        }

        public static Event SearchEvent(Events allevents, Meeting met)
        {
            try
            {
                foreach (var ev in allevents.Items)
                {
                    var SummarySplit = ev.Summary.Split(' ');
                    var PatientID = int.Parse(SummarySplit[0].Substring(1));
                    var PatientName = SummarySplit[2];
                    var Title = SummarySplit[2];
                    var Note = ev.Description;
                    var StartDate = ev.Start.DateTime;
                    var EndDate = ev.Start.DateTime;

                    if (met.PatientID == PatientID && met.Begin == StartDate && met.End == EndDate)
                    {
                        return ev;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
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
            ev.Summary = "#" + met.PatientID + " " + met.PatientName + " " + met.EventName;
            ev.ColorId = met.Color.ToString();

            service.Events.Insert(ev, calendar.Id).Execute();
            Update();
        }
    }

}
*/