using System;
using System.Collections.Generic;
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

namespace KineApp.View.PatientUI
{
    /// <summary>
    /// Interaction logic for SessionPatient.xaml
    /// </summary>
    public partial class RecordPatient : Page
    {
        Patient SelectedPatient { get; set; }

        public RecordPatient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdatePatient(Patient Current)
        {
            SelectedPatient = Current;

            if (Current.CurrentRecord == null)
            {
                G_Create.Visibility = Visibility.Hidden;
                G_Init.Visibility = Visibility.Visible;
                B_Add.Visibility = G_Create.Visibility;
                B_Finish.Visibility = Visibility.Hidden;
                B_Update.Visibility = Visibility.Hidden;
            }
            else
            {
                G_Create.Visibility = Visibility.Visible;
                G_Init.Visibility = Visibility.Hidden;
                B_Add.Visibility = G_Create.Visibility;
                B_Finish.Visibility = Visibility.Visible;
                B_Update.Visibility = Visibility.Visible;
                DisplayPatientInformation();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            G_Create.Visibility = Visibility.Visible;
            G_Init.Visibility = Visibility.Hidden;
            B_Finish.Visibility = Visibility.Hidden;
            B_Update.Visibility = Visibility.Hidden;
            B_Add.Visibility = G_Create.Visibility;

            DisplayCreationInformation();
        }


        /// <summary>
        /// 
        /// </summary>
        private void DisplayCreationInformation()
        {
            TB_Price.Text = Session.GeneralPrice.ToString();
            TB_SessionNumber.Text = "10";
            TB_Follow.Text = "";
            TB_Bilan.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayPatientInformation()
        {
            TB_Title.Text = SelectedPatient.CurrentRecord.Title.ToString();
            TB_Price.Text = SelectedPatient.CurrentRecord.Price.ToString();
            TB_SessionNumber.Text = SelectedPatient.CurrentRecord.NumberPrescribedSession.ToString();
            TB_Follow.Text = SelectedPatient.CurrentRecord.Follow;
            TB_Bilan.Text = SelectedPatient.CurrentRecord.Balancesheet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Data.AddRecord(SelectedPatient, TB_Title.Text, int.Parse(TB_SessionNumber.Text), int.Parse(TB_Price.Text), TB_Follow.Text, TB_Bilan.Text);
            UpdatePatient(SelectedPatient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_Update_Click(object sender, RoutedEventArgs e)
        {
            Data.UpdateRecord(SelectedPatient, TB_Title.Text, int.Parse(TB_SessionNumber.Text), int.Parse(TB_Price.Text), TB_Follow.Text, TB_Bilan.Text);
            UpdatePatient(SelectedPatient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_Finish_Click(object sender, RoutedEventArgs e)
        {
            Data.UpdateRecord(SelectedPatient, TB_Title.Text, int.Parse(TB_SessionNumber.Text), int.Parse(TB_Price.Text), TB_Follow.Text, TB_Bilan.Text);
            SelectedPatient.CloseRecord();
            UpdatePatient(SelectedPatient);
        }
    }
}
