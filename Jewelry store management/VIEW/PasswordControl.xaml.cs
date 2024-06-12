using System.Windows;
using System.Windows.Controls;

namespace Jewelry_store_management.VIEW
{
    /// <summary>
    /// Interaction logic for PasswordControl.xaml
    /// </summary>
    public partial class PasswordControl : UserControl
    {
        public PasswordControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordControl),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordPropertyChanged));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordControl passwordControl)
            {
                var newPassword = (string)e.NewValue;
                if (passwordControl.passwordBox.Password != newPassword)
                {
                    passwordControl.passwordBox.Password = newPassword;
                }
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password != passwordBox.Password)
            {
                Password = passwordBox.Password;
            }
        }
    }
}
