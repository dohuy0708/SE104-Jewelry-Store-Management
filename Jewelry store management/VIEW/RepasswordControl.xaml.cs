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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jewelry_store_management.VIEW
{
    /// <summary>
    /// Interaction logic for RepasswordControl.xaml
    /// </summary>
    public partial class RepasswordControl : UserControl
    {

        public string Repassword
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Repassword", typeof(string), typeof(RepasswordControl), new PropertyMetadata(string.Empty));



        public RepasswordControl()
        {
            InitializeComponent();
        }
    
    private void repasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Repassword= repasswordBox.Password;
        }
    }
   
}
