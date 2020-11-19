using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Page
    {
        Brush TextBoxBrush;
        public static string Username = "";
        public static string Password = "";

        public Connexion()
        {
            InitializeComponent();
            var converter = new BrushConverter();
            TextBoxBrush = (Brush)converter.ConvertFromString("#FFB6B5B5");
            TB_UserName.Foreground = TextBoxBrush;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Username = TB_UserName.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_UserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Username == "")
            {
                TB_UserName.TextChanged -= new TextChangedEventHandler(TB_UserName_TextChanged);
                TB_UserName.Text = "Identifiant";
                TB_UserName.Foreground = TextBoxBrush;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_UserName_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_UserName.Foreground = Brushes.Black;
            if (Username == "")
            {
                TB_UserName.Text = "";
                TB_UserName.TextChanged += new TextChangedEventHandler(TB_UserName_TextChanged);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_Password.Visibility = Visibility.Hidden;
            PB_Password.Visibility = Visibility.Visible;
            PB_Password.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if(Password == "")
            {
                TB_Password.Visibility = Visibility.Visible;
                PB_Password.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PB_Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = PB_Password.Password;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_Connect_Click(object sender, RoutedEventArgs e)
        {
            Close(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_Close_Click(object sender, RoutedEventArgs e)
        {
            Close(false);
        }

        private void Close(bool WithConnetion)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            main.F_Connexion.Visibility = Visibility.Hidden;
            if(WithConnetion) main.TryConnect();
            TB_UserName.Text = "";
            PB_Password.Password = "";
            TB_UserName_LostFocus(null, null);
            TB_Password_LostFocus(null, null);
            Username = "";
            Password = "";
        }
    }
}
