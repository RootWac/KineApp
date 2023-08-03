using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Logique d'interaction pour Billing.xaml
    /// </summary>
    public partial class Billing : Page
    {
        Dictionary<DateTime, double> Data;

        public Billing()
        {
            InitializeComponent();

            ShowHistory();
        }

        /// <summary>
        /// Close Form Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void I_Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //this.Close();
            MainWindow main = Application.Current.MainWindow as MainWindow;
            main.F_Billing.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowHistory()
        {
            B_Delete.Visibility = MainWindow.IsConnected ? Visibility.Visible : Visibility.Hidden;
            LB_History.Items.Clear();
            if (P_Patient.ShownPatient != null)
            {
                double Total = 0;
                Data = DataBase.GetBillingHistory(P_Patient.ShownPatient.ID);
                foreach (var data in Data)
                {
                    LB_History.Items.Add(data.Key + ":      " + data.Value);
                    Total += data.Value;
                }

                L_Total.Content = "Total : " + Total;
                L_Patient.Content = P_Patient.ShownPatient.Name + " " + P_Patient.ShownPatient.LastName;
            }
            else L_Patient.Content = "";
        }

        private void B_Add_Click(object sender, RoutedEventArgs e)
        {
            if (P_Patient.ShownPatient != null)
            {
                double Amount = 0;
                try
                {
                    Amount = double.Parse(TB_Add.Text.Replace(",", "."), CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    MessageBox.Show("Le montant rentré est invalide. Veuillez rentrer le montant dans le bon format");
                    return;
                }
                DataBase.AddBilling(P_Patient.ShownPatient, Amount);
                ShowHistory();
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ShowHistory();
        }

        private void B_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (P_Patient.ShownPatient != null && LB_History.SelectedIndex >= 0)
                DataBase.DeleteBilling(P_Patient.ShownPatient, Data.Keys.ToArray()[LB_History.SelectedIndex]);
            ShowHistory();
        }
    }
}
