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
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using KineApp.Model;
using KineApp.Controller;
using LiveChartsCore.Defaults;

namespace KineApp.PatientUI
{
    /// <summary>
    /// Interaction logic for DisplayPatient.xaml
    /// </summary>
    public partial class DisplayPatient : Page
    {
        public IEnumerable<ISeries> Series { get; set; }
        = new GaugeBuilder()
        .WithLabelsSize(50)
        .WithInnerRadius(75)
        .WithBackgroundInnerRadius(75)
        .WithBackground(new SolidColorPaint(new SKColor(100, 181, 246, 90)))
        .WithLabelsPosition(PolarLabelsPosition.ChartCenter)
        .AddValue(5, "Nombre de seances", SKColors.YellowGreen, SKColors.Red) // defines the value and the color 
        .BuildSeries();

        public IEnumerable<ISeries> SeriesCost { get; set; }

        public DisplayPatient()
        {
            InitializeComponent();
            DataContext = this;
            PC_Cost.Total = 500;

            // Init Cost serie
            Amount = new ObservableValue { Value = 0 };
            TotalPayement = new ObservableValue { Value = 0 };
            TotalAmount = new ObservableValue { Value = 0 };
            SeriesCost = new GaugeBuilder()
            .WithLabelsSize(20)
            .WithLabelsPosition(PolarLabelsPosition.Start)
            .WithLabelFormatter(point => $"{point.Context.Series.Name} {point.PrimaryValue}")
            .WithInnerRadius(20)
            .WithOffsetRadius(8)
            .WithBackgroundInnerRadius(20)

            .AddValue(Amount, "Solde du")
            .AddValue(TotalPayement, "Payement")
            .AddValue(TotalAmount, "Montant Total")
            .BuildSeries();


        }

        public ObservableValue Amount { get; set; }
        public ObservableValue TotalPayement { get; set; }
        public ObservableValue TotalAmount { get; set; }

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
            PatientInit.SelectedPatient = null;
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
        public void UpdatePatient(Patient Value)
        {
            TimeSpan sessionTime = new TimeSpan();
            foreach (var session in Value.CurrentRecord.ListOfSession)
                sessionTime += session.SessionTime;

            L_SessionCount.Content = Value.CurrentRecord.ListOfSession.Count;
            L_SessionTime.Content = (int) sessionTime.TotalMinutes;
            PC_Session.Total = Value.CurrentRecord.NumberPrescribedSession;
            var sessionCollection = (ICollection<ObservableValue>)Series.First().Values;
            sessionCollection.First().Value = Value.CurrentRecord.ListOfSession.Count;
            
            Amount.Value = Value.CurrentRecord.GetTotalAmount - Value.CurrentRecord.TotalPaiedAmount;
            TotalPayement.Value = Value.CurrentRecord.TotalPaiedAmount;
            TotalAmount.Value = Value.CurrentRecord.GetTotalAmount;
            TB_Bilan.Text = Value.CurrentRecord.Balancesheet;
            TB_Suivi.Text = Value.CurrentRecord.Follow;

            PC_Cost.Total = Value.CurrentRecord.GetTotalAmount;
        }
        #endregion

    }
}
