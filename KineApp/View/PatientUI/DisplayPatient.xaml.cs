﻿using System;
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
using LiveChartsCore.Kernel;
using Microsoft.Win32;
using System.IO;
using System.Management;
using System.Diagnostics;

namespace KineApp.PatientUI
{
    /// <summary>
    /// Interaction logic for DisplayPatient.xaml
    /// </summary>
    public partial class DisplayPatient : Page
    {
        Patient SelectedPatient = null;
        public int Font { get; set; } = 19;
        public int TitleFont { get; set; } = 22;

        public Func<ChartPoint, string> PointLabel { get; set; }

        public IEnumerable<ISeries> Series { get; set; }
        = new GaugeBuilder()
        .WithLabelsSize(30)
        .WithInnerRadius(75)
        .WithBackgroundInnerRadius(75)
        .WithBackground(new SolidColorPaint(new SKColor(100, 181, 246, 90)))
        .WithLabelsPosition(PolarLabelsPosition.ChartCenter)
        .AddValue(5, "Nombre de seances", SKColors.YellowGreen, SKColors.Black)
        .BuildSeries();

        public IEnumerable<ISeries> SeriesCost { get; set; }

        public DisplayPatient()
        {
            TitleFont = (int)(TitleFont * Data.ZOOM);
            Font = (int)(Font * Data.ZOOM);

            InitializeComponent();
            DataContext = this;
            PC_Cost.Total = 500;

            // Init Cost serie
            Amount = new ObservableValue { Value = 0 };
            TotalPayement = new ObservableValue { Value = 0 };
            TotalAmount = new ObservableValue { Value = 0 };
            SeriesCost = new GaugeBuilder()
            .WithLabelsSize(Font * 0.8)
            .WithLabelsPosition(PolarLabelsPosition.Start)
            .WithLabelFormatter(point => $"{point.Context.Series.Name} {point.PrimaryValue}")
            .WithInnerRadius(TitleFont * 0.5)
            .WithOffsetRadius(TitleFont * 0.25)
            .WithBackgroundInnerRadius(TitleFont * 0.5)
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
            SelectedPatient = Value;
            TimeSpan sessionTime = new TimeSpan();
            foreach (var session in Value.CurrentRecord.ListOfSession)
                sessionTime += session.SessionTime;

            L_SessionCount.Content = Value.CurrentRecord.NumberPrescribedSession;
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

            LB_OldReport.Items.Clear();
            foreach(var val in Value.AllRecords)
            {
                if (val.Value.Id != Value.CurrentRecord.Id)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = val.Value.Title + ": " + val.Value.Begin;
                    item.Background = val.Value.End != DateTime.MinValue ? Brushes.Red : Brushes.Green;
                    LB_OldReport.Items.Add(item);
                }
            }

            LB_Files.Items.Clear();
            foreach (var val in Value.AdditionalFiles)
            {
                if (val.record == Value.CurrentRecord.Id)
                {
                    LB_Files.Items.Add(System.IO.Path.GetFileName(val.filename));
                }
            }
        }
        #endregion

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var filename = openFileDialog.FileName;

                var searcher = new ManagementObjectSearcher( "root\\CIMV2", "SELECT * FROM Win32_MappedLogicalDisk").Get();

                string providerName = "Local";
                foreach (ManagementObject queryObj in searcher)
                {
                    if (queryObj["Name"].ToString() == (filename[0] + ":"))
                    {
                        providerName = queryObj["ProviderName"].ToString();
                    }
                }

                if (!Data.AddFileNameToPatient(SelectedPatient, "AdditionalImage", filename, providerName))
                {
                    MessageBox.Show("Probleme survenue lors de l'enregistrement du fichier dans la base de donnees!");
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LB_Files_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(LB_Files.SelectedIndex >= 0)
            {
                Process process = new Process();
                process.StartInfo.FileName = SelectedPatient.AdditionalFiles[LB_Files.SelectedIndex].filename;
                process.Start();
            }
        }
    }
}
