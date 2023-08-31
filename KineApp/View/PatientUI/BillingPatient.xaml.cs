using LiveChartsCore.Defaults;
using LiveChartsCore;
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
using KineApp.Model;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Math;
using KineApp.Controller;

namespace KineApp.View.PatientUI
{
    /// <summary>
    /// Interaction logic for BillingPatient.xaml
    /// </summary>
    public partial class BillingPatient : Page
    {
        public int HeaderFont { get; set; } = 19;
        public int TitleFont { get; set; } = 16;
        public int Font { get; set; } = 11;

        Patient CurrentPatient = null;

        public BillingPatient()
        {
            HeaderFont = (int)(HeaderFont * Data.ZOOM);
            TitleFont = (int)(TitleFont * Data.ZOOM);
            Font = (int)(Font * Data.ZOOM);

            InitializeComponent();
            DataContext = this;
            CBB_DiscountType.Items.Add("%");
            CBB_DiscountType.Items.Add("DH");
        }

        #region Update UI
        public void UpdatePatient(Patient Value)
        {
            CurrentPatient = Value;
            L_Rate.Content = "Tarif : " + Value.CurrentRecord.Price.ToString();
            L_Price.Content = "Price : " + Value.CurrentRecord.Price.ToString();

            DG_NotPaied.ItemsSource = Value.CurrentRecord.ListOfSession.Where(var => !var.Bill.isPaied).Select(var => var.Bill);
            DG_Paied.ItemsSource = Value.CurrentRecord.ListOfSession.Where(var => var.Bill.isPaied).Select(var => var.Bill);

            UpdateForm();
        }


        /// <summary>
        /// 
        /// </summary>
        private void UpdateForm()
        {
            if (CurrentPatient != null)
            {
                int discount = 0;
                int price = 0;

                int.TryParse(TB_Discount.Text, out discount);

                var notpaiedsession = CurrentPatient.CurrentRecord.ListOfSession.Where(var => !var.Bill.isPaied).ToArray();
                int number = 0;

                if (RB_Total.IsChecked != true)
                    number = notpaiedsession.Where(var => var.Date >= DP_Begin.SelectedDate && var.Date <= DP_End.SelectedDate).ToArray().Length;
                else
                    number = notpaiedsession.Length;

                L_Rate.Content = "Tarif : " + CurrentPatient.CurrentRecord.Price + " x " + number + " seances";

                switch (CBB_DiscountType.SelectedValue.ToString())
                {
                    case "%":
                        price = (int)(CurrentPatient.CurrentRecord.Price - discount / 100.0 * CurrentPatient.CurrentRecord.Price);
                        break;
                    default:
                        price = CurrentPatient.CurrentRecord.Price - discount;
                        break;
                }

                price = Math.Max(0, price);
                price = Math.Min(CurrentPatient.CurrentRecord.Price, price);
                L_Price.Content = "Prix : " + (price * number).ToString();
            }
        }
        #endregion

        #region Events UI

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_Discount_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateForm();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBB_DiscountType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateForm();
        }

        private void DP_Begin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateForm();
        }

        private void DP_End_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateForm();
        }

        private void RB_Total_Checked(object sender, RoutedEventArgs e)
        {
            UpdateForm();
        }

        private void RB_Period_Checked(object sender, RoutedEventArgs e)
        {
            UpdateForm();
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var notpaiedsession = CurrentPatient.CurrentRecord.ListOfSession.Where(var => !var.Bill.isPaied).ToArray();

            if (RB_Total.IsChecked != true)
                notpaiedsession = notpaiedsession.Where(var => var.Date >= DP_Begin.SelectedDate && var.Date <= DP_End.SelectedDate).ToArray();

            foreach(var paie in notpaiedsession)
            {
                int discount = 0;

                int.TryParse(TB_Discount.Text, out discount);
                paie.Bill.Pay(CBB_DiscountType.SelectedValue == "%" ? DiscountEnum.Percentage: DiscountEnum.Argent, discount, comment: TB_Comment.Text);
            }

            UpdatePatient(CurrentPatient);         
        }
        #endregion


    }
}
