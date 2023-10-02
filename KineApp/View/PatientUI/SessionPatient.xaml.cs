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
using Google.Protobuf.WellKnownTypes;
using KineApp.Controller;
using KineApp.Model;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;

namespace KineApp.View.PatientUI
{
    /// <summary>
    /// Interaction logic for SessionPatient.xaml
    /// </summary>
    public partial class SessionPatient : Page
    {
        public int TitleFont { get; set; } = 16;
        public int Font { get; set; } = 14;

        public IEnumerable<ISeries> Series { get; set; }
        = new GaugeBuilder()
        .WithLabelsSize(30)
        .WithInnerRadius(75)
        .WithBackgroundInnerRadius(75)
        .WithBackground(new SolidColorPaint(new SKColor(100, 181, 246, 90)))
        .WithLabelsPosition(PolarLabelsPosition.ChartCenter)
        .AddValue(5, "Nombre de seances", SKColors.YellowGreen, SKColors.Red) // defines the value and the color 
        .BuildSeries();

        Patient SelectedPatient;

        public SessionPatient()
        {
            TitleFont = (int)(TitleFont * Data.ZOOM);
            Font = (int)(Font * Data.ZOOM);

            InitializeComponent();
            DataContext = this;

            Data.UpdatePatient += new EventHandler(OnPatientUpdate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPatientUpdate(object sender, EventArgs e)
        {
            if(SelectedPatient != null) UpdateAppoitementList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedPatient"></param>
        internal void UpdatePatient(Patient selectedPatient)
        {
            SelectedPatient = selectedPatient;
            L_Total.Content = "Nombre total de seance prescrit : " + selectedPatient.CurrentRecord.NumberPrescribedSession;
            PC_Session.Total = selectedPatient.CurrentRecord.NumberPrescribedSession;

            LB_HistorySession.Items.Clear();
            if (selectedPatient.CurrentRecord.ListOfSession.Count > 0)
            {
                G_LasSession.Visibility = Visibility.Visible;

                Session last_session = selectedPatient.CurrentRecord.ListOfSession.Last();

                TB_DescriptionView.Text = last_session.Description;
                L_SessionNumber.Content = last_session.Title;
                L_SessionTime.Content = last_session.SessionTime.TotalMinutes.ToString("F0");

                foreach(var value in selectedPatient.CurrentRecord.ListOfSession)
                {
                    LB_HistorySession.Items.Add(value.Date.ToString() + " " + value.Title);
                }
                LB_HistorySession.SelectedIndex = 0;
            }
            else
            {
                G_LasSession.Visibility = Visibility.Hidden;
            }


            var sessionCollection = (ICollection<ObservableValue>)Series.First().Values;
            sessionCollection.First().Value = selectedPatient.CurrentRecord.ListOfSession.Count;
            UpdateAppoitementList();
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
                UpdatePatient(SelectedPatient);
            }
            else
            {
                MessageBox.Show("Il n'y pas de rendez selectionne pour la seance, veuillez ajouter un rendez-vous avant!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TB_Tilte.Text = "Seance numero " + (SelectedPatient.CurrentRecord.ListOfSession.Count + 1);
            TB_Price.Text = SelectedPatient.CurrentRecord.Price.ToString();
            UpdateAppoitementList();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateAppoitementList()
        {
            CB_Appoitement.Items.Clear();
            var validappoitement = SelectedPatient.CurrentRecord.Next_Appoitements.Where(var => var.Begin.Date == DateTime.Now.Date).ToList();
            if (validappoitement.Count > 0)
            {
                foreach(var element in validappoitement)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = element.Begin + " " + element.EventName;
                    item.Tag = element;

                    CB_Appoitement.SelectedIndex = 0;
                    CB_Appoitement.Items.Add(item);
                    S_SessionTime.Value = (element.End - element.Begin).TotalMinutes;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LB_HistorySession_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SelectedPatient.CurrentRecord.ListOfSession.Count > 0 && LB_HistorySession.SelectedIndex != -1)
            {
                var value = SelectedPatient.CurrentRecord.ListOfSession[LB_HistorySession.SelectedIndex];
                TB_HistoryDescriptionView.Text = value.Description;
                L_HistorySessionNumber.Content = value.Title;
                L_HistorySessionTime.Content = value.SessionTime.TotalMinutes.ToString("F0");
            }
            else
            {
                TB_HistoryDescriptionView.Text = "";
                L_HistorySessionNumber.Content = "";
                L_HistorySessionTime.Content = 30;
            }
        }

        private void B_Next_Click(object sender, RoutedEventArgs e)
        {
            if (LB_HistorySession.SelectedIndex < SelectedPatient.CurrentRecord.ListOfSession.Count - 1)
            {
                LB_HistorySession.SelectedIndex = LB_HistorySession.SelectedIndex + 1;
            }
        }

        private void B_Previous_Click(object sender, RoutedEventArgs e)
        {
            if (LB_HistorySession.SelectedIndex > 0)
            {
                LB_HistorySession.SelectedIndex = LB_HistorySession.SelectedIndex - 1;
            }
        }
    }
}
