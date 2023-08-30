using AM.Widget.WPF;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KineApp.Controller;
using KineApp.Model;
using Google.Protobuf.WellKnownTypes;

namespace KineApp.View.CalendarUI
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : Page
    {
        public Calendar()
        {
            InitializeComponent();

            CV_Calendar.UpdateData(Data.GetAppointements());
            CV_Calendar.NewOrEditAppointementEvent += new CalendarView.NewOrEditAppointementEventHandler(AddAppointement_Event);
            CV_Calendar.DeletingAppointementEvent += new CalendarView.DeletingAppointementEventHandler(DeletingAppointement_Event);
            Data.UpdateCalendar += new EventHandler(Update_Calendar);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Calendar(object sender, EventArgs e)
        {
            CV_Calendar.UpdateData(Data.GetAppointements());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        private void DeletingAppointement_Event(int Value)
        {
            Data.DeleteAppointement(Value);
        }

        /// <summary>
        /// Create new appoitement or update if already exist
        /// </summary>
        /// <param name="Value"></param>
        private void AddAppointement_Event(Meeting Value)
        {
            Data.AddAppointement_Event(Value);
        }
    }
}
