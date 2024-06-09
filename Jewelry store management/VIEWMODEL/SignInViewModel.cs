using Jewelry_store_management.HELPER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Jewelry_store_management.VIEWMODEL
{
    public class SignInViewModel:BaseViewModel
    { 

        // khai báo
        public ICommand SignUpCommand{ get; set; }
        public ICommand LogInCommand { get; set; }
        public ICommand ForgetPasswordCommand { get; set; }

        private UserHelper _userHelper;

        // field

      
        private string _securePassword;
        private string _email;



        public string Password
        {
            private get => _securePassword;
            set
            {
                _securePassword = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }


        // các hàm click 
        public SignInViewModel()
        {
            LogInCommand = new RelayCommand(async _ => await LogInClick());
            SignUpCommand =new RelayCommand(async _ => await SignUpClick());
            ForgetPasswordCommand = new RelayCommand(async _ => await ForgetPasswordClick());
            _userHelper = new UserHelper();
        }


        // nút đăng ký
        private async Task LogInClick()
        {
            // kiểm tra đầu vào
            if (ValidateInputs())
            {
                var user = await _userHelper.Login(Email, Password);

                if (user != null)
                {
                    MessageBox.Show("Đăng nhập thành công!");
                    // Chuyển đến trang chính hoặc thực hiện các thao tác cần thiết sau khi đăng nhập

                }
                else
                {
                    MessageBox.Show("Email hoặc mật khẩu không đúng.");
                }
            }

        }

        // chuyển tới form đăng ký
        private async Task SignUpClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 1;

        }
        // chuyển tới form quên mật khẩu 
        private async Task ForgetPasswordClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 2;
        }

        // các hàm chức năng


        // kiểm tra đầu vào
        private bool ValidateInputs()
        {
            // Kiểm tra định dạng email
            if (string.IsNullOrEmpty(Email) || !IsValidEmail(Email))
            {
                MessageBox.Show("Email không hợp lệ.");
                return false;
            }

            // Kiểm tra password phải có trên 8 ký tự và chứa cả chữ cái và số
            if (string.IsNullOrEmpty(Password) || Password.Length < 8 || !Password.Any(char.IsLetter) || !Password.Any(char.IsDigit))
            {
                MessageBox.Show("Password phải có ít nhất 8 ký tự, bao gồm cả chữ cái và số.");
                return false;
            }

          
          

            return true;
        }

        // phương thức kiểm tra định dạng email
        private bool IsValidEmail(string email)
        {
            // Sử dụng Regex để kiểm tra định dạng email
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
