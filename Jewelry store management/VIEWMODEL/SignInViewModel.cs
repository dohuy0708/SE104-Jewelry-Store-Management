using Jewelry_store_management.HELPER;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Jewelry_store_management.VIEWMODEL
{
    public class SignInViewModel : BaseViewModel
    {
        // Khai báo các lệnh (Command) cho các nút và chức năng
        public ICommand SignUpCommand { get; set; }
        public ICommand LogInCommand { get; set; }
        public ICommand ForgetPasswordCommand { get; set; }
        public ICommand IsRememberMeCheckedCommand { get; set; }

        private UserHelper _userHelper;
        private const string CredentialsFileName = "credentials.json"; // Tên file lưu trữ thông tin đăng nhập

        // Các trường dữ liệu (field) chứa thông tin đăng nhập
        private string _securePassword;
        private string _email;
        private bool _isChecked;

        // Thuộc tính Password (mật khẩu)
        public string Password
        {
            private get => _securePassword;
            set
            {
                _securePassword = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        // Thuộc tính Email
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        // Thuộc tính IsChecked để kiểm tra ô "Ghi nhớ đăng nhập" có được chọn hay không
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        // Hàm khởi tạo SignInViewModel
        public SignInViewModel()
        {
            LogInCommand = new RelayCommand(async _ => await LogInClick());
            SignUpCommand = new RelayCommand(async _ => await SignUpClick());
            ForgetPasswordCommand = new RelayCommand(async _ => await ForgetPasswordClick());
            _userHelper = new UserHelper();

            LoadCredentials(); // Tải thông tin đăng nhập đã lưu (nếu có)
        }

        // Hàm tải thông tin đăng nhập từ file credentials.json
        private void LoadCredentials()
        {
            if (File.Exists(CredentialsFileName))
            {
                var credentialsJson = File.ReadAllText(CredentialsFileName);
                var credentials = JsonSerializer.Deserialize<Credentials>(credentialsJson);
                if (credentials != null)
                {
                    Email = credentials.Email;
                    Password = Decrypt(credentials.EncryptedPassword);
                    IsChecked = true; // Đánh dấu ô "Ghi nhớ đăng nhập"
                }
            }
        }

        // Hàm lưu thông tin đăng nhập vào file credentials.json
        private void SaveCredentials()
        {
            if (IsChecked)
            {
                var credentials = new Credentials
                {
                    Email = Email,
                    EncryptedPassword = Encrypt(Password)
                };
                var credentialsJson = JsonSerializer.Serialize(credentials);
                File.WriteAllText(CredentialsFileName, credentialsJson);
            }
            else
            {
                if (File.Exists(CredentialsFileName))
                {
                    File.Delete(CredentialsFileName);
                }
            }
        }

        // Hàm mã hóa mật khẩu
        private string Encrypt(string plainText)
        {
            return Convert.ToBase64String(ProtectedData.Protect(Encoding.UTF8.GetBytes(plainText), null, DataProtectionScope.CurrentUser));
        }

        // Hàm giải mã mật khẩu
        private string Decrypt(string encryptedText)
        {
            return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(encryptedText), null, DataProtectionScope.CurrentUser));
        }

        // Hàm xử lý sự kiện khi nhấn nút Đăng nhập
        private async Task LogInClick()
        {
            if (ValidateInputs())
            {
                var user = await _userHelper.Login(Email, Password);

                if (user != null)
                {
                    SaveCredentials(); // Lưu thông tin đăng nhập nếu "Ghi nhớ đăng nhập" được chọn
                    
                    GlobalVariables.CurrentEmail = Email;

                    var currentWindow = Application.Current.MainWindow;

                    // Khởi động cửa sổ MainWindow
                    var mainWindow = new MainWindow();
                    Application.Current.MainWindow = mainWindow;
                    mainWindow.Show();

                    // Tắt cửa sổ hiện tại
                    currentWindow.Close();
                }
                else
                {
                    MessageBox_Window.ShowDialog("Email hoặc mật khẩu không đúng!", "Lỗi", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
                }
            }
        }

        // Hàm chuyển tới form Đăng ký
        private async Task SignUpClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 1;
        }

        // Hàm chuyển tới form Quên mật khẩu
        private async Task ForgetPasswordClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 2;
        }

        // Hàm kiểm tra đầu vào
        private bool ValidateInputs()
        {
            // Kiểm tra định dạng email
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email))
            {
                MessageBox_Window.ShowDialog("Vui lòng nhập Email và Mật khẩu để đăng nhập!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
                return false;
            }
            if (!IsValidEmail(Email))
            {
                MessageBox_Window.ShowDialog("Email không hợp lệ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
                return false;
            }
            // Kiểm tra mật khẩu phải có ít nhất 8 ký tự và chứa cả chữ cái và số
            if (Password.Length < 8 || !Password.Any(char.IsLetter) || !Password.Any(char.IsDigit))
            {
                MessageBox_Window.ShowDialog("Mật khẩu phải có ít nhất 8 ký tự, bao gồm cả chữ cái và số.", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
                return false;
            }
            return true;
        }

        // Phương thức kiểm tra định dạng email
        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Lớp chứa thông tin đăng nhập
        private class Credentials
        {
            public string Email { get; set; }
            public string EncryptedPassword { get; set; }
        }
    }
}
