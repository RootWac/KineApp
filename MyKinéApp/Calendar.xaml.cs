using AM.Widget.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyKinéApp
{
    /// <summary>
    /// Logique d'interaction pour Calendar.xaml
    /// </summary>
    public partial class Calendar : Page
    {
        public Calendar()
        {
            InitializeComponent();

            CV_Calendar.UpdateData(DataBase.GetAppointements());
            CV_Calendar.NewOrEditAppointementEvent += new CalendarView.NewOrEditAppointementEventHandler(AddAppointement_Event);
            CV_Calendar.DeletingAppointementEvent += new CalendarView.DeletingAppointementEventHandler(DeletingAppointement_Event);
        }

        private void DeletingAppointement_Event(int Value)
        {
            DataBase.DeleteAppointement(Value);
            CV_Calendar.UpdateData(DataBase.GetAppointements());
        }

        private void AddAppointement_Event(Meeting Value)
        {
            DataBase.UpdateAppointement(Value);
            CV_Calendar.UpdateData(DataBase.GetAppointements());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void I_Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            main.F_Calendar.Visibility = Visibility.Hidden;
        }
    }
}
