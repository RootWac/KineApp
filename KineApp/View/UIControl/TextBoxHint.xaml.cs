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

namespace WpfApp1.UIControl
{
    /// <summary>
    /// Interaction logic for TextBoxHint.xaml
    /// </summary>
    public partial class TextBoxHint : UserControl
    {
        public TextBox Element
        {
            get
            {
                return TB_Element;
            }
        }

        public TextBoxHint()
        {
            InitializeComponent();
        }
    }
}
