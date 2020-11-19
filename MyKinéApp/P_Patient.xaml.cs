using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;

namespace MyKinéApp
{
    /// <summary>
    /// Logique d'interaction pour Patient.xaml
    /// </summary>
    public partial class P_Patient : Page
    {
        private Dictionary<string, string> Filter = new Dictionary<string, string>();
        private Patient ShownPatient;

        public P_Patient()
        {
            InitializeComponent();

            DG_Patients.ItemsSource = DataBase.GetPatients(ref Filter).DefaultView;
            I_Searsh_MouseDown(null, null);
        }

        ////////////////////////////////////////////////////////////////////////////// Menu ////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void I_Searsh_MouseDown(object sender, MouseButtonEventArgs e)
        {
            G_Create.Visibility = Visibility.Hidden;
            G_Search.Visibility = Visibility.Visible;
            G_SelectedPatient.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void I_AddUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            G_Create.Visibility = Visibility.Visible;
            G_Search.Visibility = Visibility.Hidden;
            G_SelectedPatient.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void I_SelectedUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            G_Create.Visibility = Visibility.Hidden;
            G_Search.Visibility = Visibility.Hidden;
            G_SelectedPatient.Visibility = Visibility.Visible;
        }

        ////////////////////////////////////////////////////////////////////////////// Create ////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Event to add patient to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string Address = new TextRange(TB_CreateAddress.Document.ContentStart, TB_CreateAddress.Document.ContentEnd).Text.Replace(Environment.NewLine, " ");
            Patient patient = new Patient(TB_CreateName.Text, TB_CreateLastName.Text, TB_CreateCIN.Text, TB_CreatePhone.Text, Address, EVDP_CreateBirthday.Date, S_CreateHeight.Value, S_CreateWeight.Value, SB_Gender.IsChecked ? "Femme" : "Homme");

            if (patient.Name == "" || patient.LastName == "" || patient.PhoneNumber == "" || patient.CIN == "") return;
            if (DataBase.AddPatient(patient))
            {
                TB_CreateName.Text = "";
                TB_CreateLastName.Text = "";
                TB_CreateAddress.Document.Blocks.Clear();
                TB_CreateAddress.Document.Blocks.Add(new Paragraph(new Run("")));
                TB_CreatePhone.Text = "";
                TB_CreateCIN.Text = "";

                DG_Patients.ItemsSource = DataBase.GetPatients(ref Filter).DefaultView;
                CollectionViewSource.GetDefaultView(DG_Patients.ItemsSource).Refresh();
            }
        }

        ////////////////////////////////////////////////////////////////////////////// Search ////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_LastName.Text == "") Filter.Remove("Nom");
            else Filter["Nom"] = "`Nom` LIKE '%" + TB_LastName.Text + "%' ";

            DG_Patients.ItemsSource = DataBase.GetPatients(ref Filter).DefaultView;
            CollectionViewSource.GetDefaultView(DG_Patients.ItemsSource).Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_Name.Text == "") Filter.Remove("Prenom");
            else Filter["Prenom"] = "`Prenom` LIKE '%" + TB_Name.Text + "%' ";

            DG_Patients.ItemsSource = DataBase.GetPatients(ref Filter).DefaultView;
            CollectionViewSource.GetDefaultView(DG_Patients.ItemsSource).Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_Phone.Text == "") Filter.Remove("Tel");
            else Filter["Tel"] = "`Tel` LIKE '%" + TB_Phone.Text + "%' ";

            DG_Patients.ItemsSource = DataBase.GetPatients(ref Filter).DefaultView;
            CollectionViewSource.GetDefaultView(DG_Patients.ItemsSource).Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_CIN_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_CIN.Text == "") Filter.Remove("CIN");
            else Filter["CIN"] = "`CIN` LIKE '%" + TB_CIN.Text + "%' ";

            DG_Patients.ItemsSource = DataBase.GetPatients(ref Filter).DefaultView;
            CollectionViewSource.GetDefaultView(DG_Patients.ItemsSource).Refresh();
        }

        /// <summary>
        /// Event to covert datetime format of column 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "yyyy-MM-dd";
        }

        ////////////////////////////////////////////////////////////////////// Selected patient //////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DG_Patients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView RowView = ((DataRowView)DG_Patients.SelectedValue);

            if (RowView != null)
            {
                int ID = (int)RowView.Row.ItemArray[0];
                ShownPatient = DataBase.GetPatients(ID);
                L_Patients.Content = ShownPatient.Name + ", " + ShownPatient.LastName;
            }
            else ShownPatient = null;

            L_Patients.Content = (ShownPatient != null) ? ShownPatient.Name + ", " + ShownPatient.LastName : "";

            TB_BalanceSheet.IsEnabled = ShownPatient != null;
            TB_Treatment.IsEnabled = ShownPatient != null;
            TB_Diagnostic.IsEnabled = ShownPatient != null;

            TB_BalanceSheet.Text = (ShownPatient != null && ShownPatient.BalanceSheet != null) ? ShownPatient.BalanceSheet : "";
            TB_Treatment.Text = (ShownPatient != null && ShownPatient.Treatment != null) ? ShownPatient.Treatment : "";
            TB_Diagnostic.Text = (ShownPatient != null && ShownPatient.Diagnostic != null) ? ShownPatient.Diagnostic : "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_Diagnostic_TextChanged(object sender, TextChangedEventArgs e)
        {
            B_PatientsUpdate.IsEnabled = ShownPatient != null && (ShownPatient.Diagnostic != TB_Diagnostic.Text || ShownPatient.BalanceSheet != TB_BalanceSheet.Text || ShownPatient.Treatment != TB_Treatment.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_Bilan_TextChanged(object sender, TextChangedEventArgs e)
        {
            B_PatientsUpdate.IsEnabled = ShownPatient != null && (ShownPatient.Diagnostic != TB_Diagnostic.Text || ShownPatient.BalanceSheet != TB_BalanceSheet.Text || ShownPatient.Treatment != TB_Treatment.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_Traitement_TextChanged(object sender, TextChangedEventArgs e)
        {
            B_PatientsUpdate.IsEnabled = ShownPatient != null && (ShownPatient.Diagnostic != TB_Diagnostic.Text || ShownPatient.BalanceSheet != TB_BalanceSheet.Text || ShownPatient.Treatment != TB_Treatment.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_PatientsUpdate_Click(object sender, RoutedEventArgs e)
        {
            ShownPatient.Diagnostic = TB_Diagnostic.Text;
            ShownPatient.Treatment = TB_Treatment.Text;
            ShownPatient.BalanceSheet = TB_BalanceSheet.Text;
            ShownPatient.Update_ClinicalFollowUp();

            ShownPatient = DataBase.GetPatients(ShownPatient.ID);
            B_PatientsUpdate.IsEnabled = ShownPatient != null && (ShownPatient.Diagnostic != TB_Diagnostic.Text || ShownPatient.BalanceSheet != TB_BalanceSheet.Text || ShownPatient.Treatment != TB_Treatment.Text);
        }

        ////////////////////////////////////////////////////////////////////////////// Mixt ////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Close Form Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void I_Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //this.Close();
            MainWindow main = Application.Current.MainWindow as MainWindow;
            main.F_Patient.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetFont()
        {
            ////////////////////////////  Create  ////////////////////////////
            L_CreateAddress.FontSize = ActualHeight / 45;
            L_CreateBirthday.FontSize = ActualHeight / 45;
            L_CreateCIN.FontSize = ActualHeight / 45;
            L_CreateGender.FontSize = ActualHeight / 45;
            L_CreateHeight.FontSize = ActualHeight / 45;
            L_CreateLastName.FontSize = ActualHeight / 45;
            L_CreateName.FontSize = ActualHeight / 45;
            L_CreatePhone.FontSize = ActualHeight / 45;
            L_CreateWeight.FontSize = ActualHeight / 45;

            TB_CreateAddress.FontSize = ActualHeight / 45;
            TB_CreateCIN.FontSize = ActualHeight / 45;
            TB_CreateHeight.FontSize = ActualHeight / 45;
            TB_CreateLastName.FontSize = ActualHeight / 45;
            TB_CreateName.FontSize = ActualHeight / 45;
            TB_CreatePhone.FontSize = ActualHeight / 45;
            TB_CreateWeight.FontSize = ActualHeight / 45;
            SB_Gender.FontSize = ActualHeight / 45;
            EVDP_CreateBirthday.FontSize = ActualHeight / 45;
            S_CreateHeight.FontSize = ActualHeight / 45;
            S_CreateWeight.FontSize = ActualHeight / 45;

            ////////////////////////////  Create  ////////////////////////////
            L_Name.FontSize = ActualHeight / 45;
            L_LastName.FontSize = ActualHeight / 45;
            L_CIN.FontSize = ActualHeight / 45;
            L_Phone.FontSize = ActualHeight / 45;

            TB_Name.FontSize = ActualHeight / 45;
            TB_LastName.FontSize = ActualHeight / 45;
            TB_CIN.FontSize = ActualHeight / 45;
            TB_Phone.FontSize = ActualHeight / 45;

            ////////////////////////////  Create  ////////////////////////////
            L_Traitement.FontSize = ActualHeight / 45;
            L_Bilan.FontSize = ActualHeight / 45;
            L_Diagnostic.FontSize = ActualHeight / 45;

            TB_Treatment.FontSize = ActualHeight / 45;
            TB_BalanceSheet.FontSize = ActualHeight / 45;
            TB_Diagnostic.FontSize = ActualHeight / 45;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetFont();
        }

        ////////////////////////////////////////////////////////////////////////////// Fin ////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}