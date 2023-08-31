using System;
using System.Collections.Generic;
using System.Drawing;
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
using AM.Widget.WPF;
using KineApp.Controller;
using KineApp.Model;

namespace KineApp.View.PatientUI
{
    /// <summary>
    /// Interaction logic for SessionPatient.xaml
    /// </summary>
    public partial class SessionPatient : Page
    {
        public int TitleFont { get; set; } = 12;
        public int Font { get; set; } = 10;
        Patient SelectedPatient;
        public SessionPatient()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedPatient"></param>
        internal void UpdatePatient(Patient selectedPatient)
        {
            SelectedPatient = selectedPatient;
            if (selectedPatient.CurrentRecord.ListOfSession.Count > 0)
            {
                G_LasSession.Visibility = Visibility.Visible;

                Session last_session = selectedPatient.CurrentRecord.ListOfSession.Last();

                TB_DescriptionView.Text = last_session.Description;
                L_SessionNumber.Content = last_session.Title;
                L_SessionTime.Content = last_session.SessionTime.TotalMinutes.ToString("F0");
            }
            else
            {
                G_LasSession.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_AddSession_Click(object sender, RoutedEventArgs e)
        {
            if (CB_Appoitement.SelectedIndex >= 0)
            {
                var meet = (CB_Appoitement.SelectedValue as ComboBoxItem).Tag as Meeting;
                Data.AddSession(SelectedPatient, TB_Tilte.Text, TB_Price.Text, meet, S_SessionTime.Value, TB_Description.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CB_Appoitement.Items.Clear();
            int nextsession = SelectedPatient.CurrentRecord.ListOfSession.Count + 1;
            var validappoitement = SelectedPatient.CurrentRecord.Next_Appoitements.Where(var => var.Begin.Date == DateTime.Now.Date).ToList();

            TB_Tilte.Text = "Seance numero " + nextsession;
            TB_Price.Text = SelectedPatient.CurrentRecord.Price.ToString();
            if (validappoitement.Count > 0)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = validappoitement[0].Begin + validappoitement[0].EventName;
                item.Tag = validappoitement[0];

                CB_Appoitement.SelectedIndex = 0;
                CB_Appoitement.Items.Add(item);
                S_SessionTime.Value = (validappoitement[0].End - validappoitement[0].Begin).TotalMinutes;
            }
        }
    }
}
