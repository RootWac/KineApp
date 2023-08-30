using AM.Widget.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace KineApp.Model
{
    public class Session
    {
        /// <summary>
        /// It's the price fixed by the administrator and that will be applyed for all the new Patients
        /// </summary>
        public static int GeneralPrice = 120;

        public int Id;
        public DateTime Date;
        public TimeSpan SessionTime;
        public Meeting appoint;
        public FollowUp Follow;
        public Transaction Bill;
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
