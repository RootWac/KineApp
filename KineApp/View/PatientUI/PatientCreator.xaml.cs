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

namespace KineApp.PatientUI
{
    /// <summary>
    /// Interaction logic for Patient.xaml
    /// </summary>
    public partial class PatientCreator : Page
    {
        public int Font { get; set; } = 9;

        public PatientCreator()
        {
            Font = (int)(Font * Data.ZOOM);

            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_Add_Click(object sender, RoutedEventArgs e)
        {
            DateTime birth = DP_DateOfBirth.SelectedDate.Value;
            Data.AddPatient(new Patient(TB_Name.Text, TB_LastName.Text, TB_Address.Text, TB_CIN.Text, TB_PhoneNumber.Text, birth, SB_Gender.IsChecked? 'F':'M', (int)S_CreateHeight.Value, (int)S_CreateWeight.Value));
        }
    }
}
