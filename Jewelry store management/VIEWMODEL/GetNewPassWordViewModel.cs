using Jewelry_store_management.HELPER;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Jewelry_store_management.VIEWMODEL
{
    public class GetNewPassWordViewModel : BaseViewModel
    {
        // khai báo 
        public ICommand BackCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        private UserHelper _userHelper;
        // field 
        private string _securePassword;
        private string _secureRepassword;
        private string _text;
        public string Password
        {
            private get => _securePassword;
            set
            {
                _securePassword = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Repassword
        {
            private get => _secureRepassword;
            set
            {
                _secureRepassword = value;
                OnPropertyChanged(nameof(Repassword));
            }
        }
   
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        // hàm chính 
        public GetNewPassWordViewModel()
        {
            BackCommand = new RelayCommand(async _ => await BackClick());
            ConfirmCommand = new RelayCommand(async _ => await ConfirmClick());
            _userHelper = new UserHelper();
        }

        // hàm chức năng 

        private async Task ConfirmClick()
        {
            if (ValidateInputs())
            {
                string email = GlobalVariables.VerificationEmail;
                var user = await _userHelper.GetUserByEmail(email);
                if (user != null)
                {
                    Text="Hệ thống đang xử lý!";

                    user.Password = HashPassword(Password);

                    await _userHelper.UpdateUser(user);

                    MessageBox_Window.ShowDialog("Mật khẩu đã được thay đổi thành công!", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OK);
                    var window = Application.Current.MainWindow;
                    var viewModel = (window.DataContext as StartViewModel);
                    viewModel.NView = 0;
                }
                else
                {
                    MessageBox_Window.ShowDialog("Hệ thống phát hiện một số vấn đề vui lòng thử lại sau!", "Lỗi", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
                }
            }
        }

        private async Task BackClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 2;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(Password))
            {
                MessageBox_Window.ShowDialog("Vui lòng nhập Mật khẩu!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
                return false;
            }

            // Kiểm tra password phải có trên 8 ký tự và chứa cả chữ cái và số
            if (Password.Length < 8 || !Password.Any(char.IsLetter) || !Password.Any(char.IsDigit))
            {
                MessageBox_Window.ShowDialog("Mật khẩu phải có ít nhất 8 ký tự, bao gồm cả chữ cái và số.", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);


                return false;
            }
            // Kiểm tra password nhập lại có trùng với password không
            if (string.IsNullOrEmpty(Repassword)||(Password!=Repassword))///
            {

                MessageBox_Window.ShowDialog("Mật khẩu nhập lại không khớp.", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
                return false;
            }


            return true;
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

