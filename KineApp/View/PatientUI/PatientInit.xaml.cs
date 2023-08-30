using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
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
using KineApp.View.PatientUI;
using AM.Widget.WPF;
using Google.Protobuf.WellKnownTypes;

namespace KineApp.PatientUI
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class PatientInit : Page
    {
        private static PatientInit _CurrentPage;
        private static Patient _SelectedPatient = null;

        public static Patient SelectedPatient
        {
            get
            {
                return _SelectedPatient;
            }
            set
            {
                _SelectedPatient = value;
                _CurrentPage.UpdateForm("Create");
            }
        }

        public PatientInit()
        {
            InitializeComponent();
            DataContext = this;

            UpdatePatientList(Data.L_Patients);
            UpdateForm("Create");
            _CurrentPage = this;


            Data.NewPatient += new EventHandler(OnNewPatient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNewPatient(object sender, EventArgs e)
        {
            TB_Search.Text = "";
            UpdatePatientList(Data.L_Patients);

            Patient pat = (Patient)sender;

            foreach (var itm in LB_Patients.Items)
            {
                if((int)((ListBoxItem)itm).Tag == pat.Id)
                {
                    LB_Patients.SelectedItem = itm;
                }
            }
        }

        private void SP_Menu_MouseDown(object sender, MouseButtonEventArgs e)
        {


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SP_Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedPatient = null;
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

        #region Update UI
        private void UpdatePatient(Patient Value)
        {
            L_Name.Content = Value.CompleteName;
            TB_Notes.Text = Value.Note;
            TB_Address.Text = Value.Address;

            L_Gender.Content = (Value.Gender == 'F' ? "Feminin" : "Masculin") + ", " + Value.Age + "ans";
            if(Value.Gender == 'F')
            {
                I_Gender_F.Visibility = Visibility.Visible;
                I_Gender_M.Visibility = Visibility.Hidden;
                L_Gender.Content = "Feminin, " + Value.Age + "ans";
            }
            else
            {
                I_Gender_F.Visibility = Visibility.Hidden;
                I_Gender_M.Visibility = Visibility.Visible;
                L_Gender.Content = "Masculin, " + Value.Age + "ans";
            }

            L_Appoitement.Items.Clear();

            if(Value.CurrentRecord != null)
            {
                foreach (var appoit in Value.CurrentRecord.Next_Appoitements)
                {
                    L_Appoitement.Items.Add(appoit.Begin + " - " + appoit.EventName);
                }
            }
        }
        #endregion

        #region Patients 
        private void L_Patients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(LB_Patients.SelectedIndex >= 0)
            {
                int id = (int)(LB_Patients.SelectedItem as ListBoxItem).Tag;

                DP_ToolbarPatient.IsEnabled = true;
                SelectedPatient = Data.L_Patients.Where(val => val.Id == id).ToArray()[0];
                UpdatePatient(SelectedPatient);
                UpdateForm("Home");
            }
            else
            {
                DP_ToolbarPatient.IsEnabled = false;
                SelectedPatient = null;
                UpdateForm("Create");
            }
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        private void UpdatePatientList(List<Patient> patients_list)
        {
            LB_Patients.Items.Clear();
            foreach (var val in patients_list)
            {
                ListBoxItem item = new ListBoxItem();
                item.Tag = val.Id;
                item.Content = val.LastName + " " + val.FirstName;
                LB_Patients.Items.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateForm(string Page)
        {
            BillingPatient.Visibility = Visibility.Hidden;
            DisplayPatient.Visibility = Visibility.Hidden;
            CreatePatient.Visibility = Visibility.Hidden;
            RecordPatient.Visibility = Visibility.Hidden;
            SessionPatient.Visibility = Visibility.Hidden;

            if (Page == "Create")
            {
                CreatePatient.Visibility = Visibility.Visible;
            }
            else if (SelectedPatient.CurrentRecord != null)
            {
                switch (Page)
                {
                    case "Billing":
                        RB_Billing.IsChecked = true;
                        ((BillingPatient)BillingPatient.Content).UpdatePatient(SelectedPatient);
                        BillingPatient.Visibility = Visibility.Visible;
                        break;

                    case "Record":
                        RB_Record.IsChecked = true;
                        ((RecordPatient)RecordPatient.Content).UpdatePatient(SelectedPatient);
                        RecordPatient.Visibility = Visibility.Visible;
                        break;

                    case "Session":
                        RB_Sessions.IsChecked = true;
                        ((SessionPatient)SessionPatient.Content).UpdatePatient(SelectedPatient);
                        SessionPatient.Visibility = Visibility.Visible;
                        break;

                    default:
                        RB_Home.IsChecked = true;
                        ((DisplayPatient)DisplayPatient.Content).UpdatePatient(SelectedPatient);
                        DisplayPatient.Visibility = Visibility.Visible;
                        break;
                }
            }
            else
            {
                RB_Record.IsChecked = true;
                ((RecordPatient)RecordPatient.Content).UpdatePatient(SelectedPatient);
                RecordPatient.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_Create_Click(object sender, RoutedEventArgs e)
        {
            LB_Patients.SelectedIndex = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Patient> patientResearch = Data.L_Patients;
            string searchtext = TB_Search.Text.ToLower();

            if(searchtext != "")
                patientResearch = Data.L_Patients.Where(var => var.Phone.Contains(searchtext) || var.LastName.ToLower().Contains(searchtext)
                    || var.FirstName.ToLower().Contains(searchtext) || var.CIN.ToLower().Contains(searchtext)
                    || (var.LastName + " " + var.FirstName).ToLower().Contains(searchtext) 
                    || (var.FirstName + " " + var.LastName).ToLower().Contains(searchtext)).ToList();

            UpdatePatientList(patientResearch);
            /*
            LB_Patients.Items.Clear();
            foreach (var val in patientResearch)
                LB_Patients.Items.Add(val.LastName + " " + val.FirstName);*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleBilling_Checked(object sender, RoutedEventArgs e)
        {
            UpdateForm("Billing");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleHome_Checked(object sender, RoutedEventArgs e)
        {
            UpdateForm("Home");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleSession_Checked(object sender, RoutedEventArgs e)
        {
            UpdateForm("Record");
        }

        private void RB_Sessions_Checked(object sender, RoutedEventArgs e)
        {
            UpdateForm("Session");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_AddEvent_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedPatient.CurrentRecord != null)
            {
                AppointmentEditor Edit = new AppointmentEditor(SelectedPatient.CompleteName);
                Edit.ShowDialog();

                if (Edit.AppointmentResult == AppointmentEditor.Result.Save)
                {
                    Data.AddAppointement_Event(Edit.AppointmentData);
                }
                else if (Edit.AppointmentResult == AppointmentEditor.Result.Delete)
                {
                    Data.DeleteAppointement((int)Edit.AppointmentData.MeetingID);
                }
            }
            else
            {
                MessageBox.Show("Veuillez ouvrir un nouveau dossier pour ajouter ");
            }
        }
    }
}
