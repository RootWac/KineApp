using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AM.Widget.WPF;
using KineApp.Controller;

namespace KineApp.Model
{
    public class Record
    {
        public int Id;
        public int Price = 120;
        public string Title = "";
        public string Follow = "";
        public string Balancesheet = "";

        public HashSet<Meeting> Next_Appoitements { get; private set; }
        public List<Session> ListOfSession { get; private set; }
        public int NumberPrescribedSession { get; private set; }
        public DateTime Begin { get;  private set; }
        public DateTime End { get; private set; } = DateTime.MinValue;

        /// <summary>
        /// 
        /// </summary>
        public void Initialize(int Id, int NumberPrescribedSession = 10, int Price = 120)
        {
            this.Id = Id;
            this.Price = Price;
            Next_Appoitements = new HashSet<Meeting>();
            this.NumberPrescribedSession = NumberPrescribedSession;
            ListOfSession = new List<Session>();
            Begin = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeDB(int Id, string Title, DateTime Begin, int NumberPrescribedSession, int Price, string Follow, string Balancesheet)
        {
            this.Id = Id;
            this.Title = Title;
            this.Price = Price;
            Next_Appoitements = new HashSet<Meeting>();
            this.NumberPrescribedSession = NumberPrescribedSession;
            ListOfSession = new List<Session>();
            this.Begin = Begin;
            this.Follow = Follow;
            this.Balancesheet = Balancesheet;
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeDB(int Id, string Title, DateTime Begin, int NumberPrescribedSession, int Price, string Follow, string Balancesheet, DateTime End)
        {
            InitializeDB(Id, Title, Begin, NumberPrescribedSession, Price, Follow, Balancesheet);
            this.End = End;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Report"></param>
        /// <param name="Follow"></param>
        public bool Finish(DateTime CloseDate)
        {
            if (SQL.Connection.CloseRecord(this))
            {
                End = CloseDate;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        internal void AddAppointement(Meeting value)
        {
            if(Id == value.PatientID && ListOfSession.Where(var => (int)var.appoint.MeetingID == (int)value.MeetingID).Count()==0)
            {
                Next_Appoitements.RemoveWhere(var => var.MeetingID == value.MeetingID);
                Next_Appoitements.Add(value);
            }
        }

        public double GetTotalAmount { get { return ListOfSession.Sum(var => var.Bill.Amount); } }
        public double TotalPaiedAmount { get { return ListOfSession.Where(var => var.Bill.isPaied).Sum(var => var.Bill.Amount); } }
    }
}
