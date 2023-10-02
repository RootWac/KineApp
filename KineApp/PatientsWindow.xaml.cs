using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AM.Widget.WPF;
using KineApp.Controller;
using KineApp.Model;

namespace KineApp
{

    /// <summary>
    /// Interaction logic for PatientsWindow.xaml
    /// </summary>
    public partial class PatientsWindow : Window
    {
        public PatientsWindow()
        {

            //FileIO.WriteGoogleCalendar();
            //FileIO.WriteSQL();
            FileIO.ReadSQLFile();
            Data.Initialize(true);

            InitializeComponent();

            AppointmentEditor.PatientsNames = Data.GetPatientsNames();
            //Data.Simulate();

            /* //Scan
            {
                DeviceInfo firstScannerAvailable = null;
                var deviceManager = new DeviceManager();
                // Loop through the list of devices
                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
                {
                    // Skip the device if it's not a scanner
                    if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                    {
                        continue;
                    }

                    var val = deviceManager.DeviceInfos[i].Properties["Name"].get_Value();

                    // Print something like e.g "WIA Canoscan 4400F"
                    Console.WriteLine(val);
                    firstScannerAvailable = deviceManager.DeviceInfos[i];
                }
                var device = firstScannerAvailable.Connect();
                var scannerItem = device.Items[1];
                var imageFile = (ImageFile)scannerItem.Transfer(FormatID.wiaFormatJPEG);
                var path = @"C:\Users\amadi\Desktop\scan.jpeg";
                // Save image !
                imageFile.SaveFile(path);
            }*/

            //new ConnectionWindow().ShowDialog();
            Setter effectSetter = new Setter();
            effectSetter.Property = ScrollViewer.EffectProperty;
            effectSetter.Value = new DropShadowEffect
            {
                ShadowDepth = 2,
                Direction = 330,
                Color = Colors.Black,
                Opacity = 0.5,
                BlurRadius = 4
            };
            Style dropShadowScrollViewerStyle = new Style(typeof(Image));
            dropShadowScrollViewerStyle.Setters.Add(effectSetter);

            I_Note.Resources.Add(typeof(Image), dropShadowScrollViewerStyle);
            I_Patient.Resources.Add(typeof(Image), dropShadowScrollViewerStyle);
            I_Calendar.Resources.Add(typeof(Image), dropShadowScrollViewerStyle);
            I_Unk.Resources.Add(typeof(Image), dropShadowScrollViewerStyle);
            I_Stat.Resources.Add(typeof(Image), dropShadowScrollViewerStyle);

            UpdatePage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void I_Search_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void I_Patient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdatePage("Patient");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void I_Calendar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdatePage("Calendar");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Grid).Background = new SolidColorBrush(Color.FromArgb(255, 255, 128, 0));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Grid).Background = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_MouseEnter(object sender, MouseEventArgs e)
        {
            //B_MenuSecondary.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_MouseLeave(object sender, MouseEventArgs e)
        {
            //B_MenuSecondary.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void I_Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Page"></param>
        private void UpdatePage(string Page = "Default")
        {
            Patient.Visibility = Visibility.Hidden;
            Calendar.Visibility = Visibility.Hidden;

            switch (Page)
            {
                case "Calendar":
                    Calendar.Visibility = Visibility.Visible;
                    break;

                default:
                    Patient.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
