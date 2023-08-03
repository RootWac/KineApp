using AM.Widget.WPF;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyKinéApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static bool IsConnected = false;
        internal static Dictionary<int, string> PatientsNames;
        public MainWindow()
        {
            InitializeComponent();
            DataBase.ReadFile();
            /*B_Patient.Visibility = Visibility.Hidden;
            B_Calendar.Visibility = Visibility.Hidden;
            B_Facture.Visibility = Visibility.Hidden;*/
            PatientsNames = DataBase.GetPatientsNames();
            AppointmentEditor.PatientsNames = PatientsNames;
            GoogleCalendar.Initialize();
        }



        /// <summary>
        /// Takes a bitmap and converts it to an image that can be handled by WPF ImageBrush
        /// </summary>
        /// <param name="src">A bitmap image</param>
        /// <returns>The image as a BitmapImage for WPF</returns>
        public ImageSource BitmapConvert(System.Drawing.Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            src.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void TryConnect()
        {
            if (DataBase.CheckPassword(Connexion.Username, Connexion.Password))
            {
                IsConnected = true;
                /*B_Patient.Visibility = Visibility.Visible;
                B_Calendar.Visibility = Visibility.Visible;
                B_Facture.Visibility = Visibility.Visible;*/
                IB_User.ImageSource = BitmapConvert(Properties.Resources.UserDisconnect);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_User_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnected)
            {
                F_Connexion.Visibility = Visibility.Visible;
            }
            else
            {
                IsConnected = false;
                /*B_Patient.Visibility = Visibility.Hidden;
                B_Calendar.Visibility = Visibility.Hidden;
                B_Facture.Visibility = Visibility.Hidden;*/
                IB_User.ImageSource = BitmapConvert(Properties.Resources.UserConnect);
            }
        }

        private void B_Patient_Click(object sender, RoutedEventArgs e)
        {
            F_Patient.Visibility = Visibility.Visible;
        }

        private void B_Calendar_Click(object sender, RoutedEventArgs e)
        {
            F_Calendar.Visibility = Visibility.Visible;
            PatientsNames = DataBase.GetPatientsNames();
            AppointmentEditor.PatientsNames = PatientsNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void B_Facture_Click(object sender, RoutedEventArgs e)
        {
            F_Billing.Visibility = Visibility.Visible;
        }
    }
}
