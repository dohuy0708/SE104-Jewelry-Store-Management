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

namespace Jewelry_store_management.VIEW
{
    /// <summary>
    /// Interaction logic for MessageBox_Window.xaml
    /// </summary>
    public partial class MessageBox_Window : Window
    {
        public enum MessageBoxButton
        {
            YesNoCancel,
            OK,
            YesNo,
            OkCancel
        }

        public enum ButtonResult
        {
            NULL,
            YES,
            NO,
            CANCEL,
            OK
        }

        public static ButtonResult buttonResultClicked;

        public MessageBox_Window(string message, string title, string imagePath, MessageBoxButton button)
        {
            InitializeComponent();
            TBLOCK_Title.Text = title;
            TBLOCK_Message.Text = message;
            SetImageSource(imagePath);

            SetTitleColor(title);

            buttonResultClicked = ButtonResult.NULL;

            if (button == MessageBoxButton.YesNoCancel)
            {
                SP_ContainsButton.Children.Remove(BTN_OK);
            }
            else if (button == MessageBoxButton.OK)
            {
                SP_ContainsButton.Children.Remove(BTN_CANCEL);
                SP_ContainsButton.Children.Remove(BTN_NO);
                SP_ContainsButton.Children.Remove(BTN_YES);
            }
            else if (button == MessageBoxButton.YesNo)
            {
                SP_ContainsButton.Children.Remove(BTN_CANCEL);
                SP_ContainsButton.Children.Remove(BTN_OK);
            }
            else if (button == MessageBoxButton.OkCancel)
            {
                SP_ContainsButton.Children.Remove(BTN_NO);
                SP_ContainsButton.Children.Remove(BTN_YES);
            }
        }

        private void SetImageSource(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                var uri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                TBLOCK_Icon.Source = new BitmapImage(uri);
            }
        }

        private void SetTitleColor(string title)
        {
            if (title.Equals("Thành công", StringComparison.OrdinalIgnoreCase))
            {
                TBLOCK_Title.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (title.Equals("Lỗi", StringComparison.OrdinalIgnoreCase))
            {
                TBLOCK_Title.Foreground = new SolidColorBrush(Colors.Red);
            }
            else if (title.Equals("Chú ý", StringComparison.OrdinalIgnoreCase))
            {
                TBLOCK_Title.Foreground = new SolidColorBrush(Colors.Orange);
            }
            else
            {
                TBLOCK_Title.Foreground = new SolidColorBrush(Colors.Black); // Default color
            }
        }

        public static void Show(string text, string title = "", string imagePath = "", MessageBox_Window.MessageBoxButton button = MessageBox_Window.MessageBoxButton.OK)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                new MessageBox_Window(text, title, imagePath, button).Show();
            });
        }

        public static void ShowDialog(string text, string title = "", string imagePath = "", MessageBox_Window.MessageBoxButton button = MessageBox_Window.MessageBoxButton.OK)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                new MessageBox_Window(text, title, imagePath, button).ShowDialog();
            });
        }

        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            buttonResultClicked = ButtonResult.OK;
            this.Close();
        }

        private void Button_Click_YES(object sender, RoutedEventArgs e)
        {
            buttonResultClicked = ButtonResult.YES;
            this.Close();
        }

        private void Button_Click_NO(object sender, RoutedEventArgs e)
        {
            buttonResultClicked = ButtonResult.NO;
            this.Close();
        }

        private void Button_Click_CANCEL(object sender, RoutedEventArgs e)
        {
            buttonResultClicked = ButtonResult.CANCEL;
            this.Close();
        }
    }
}
