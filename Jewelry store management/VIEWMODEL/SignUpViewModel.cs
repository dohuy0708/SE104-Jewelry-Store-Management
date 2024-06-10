using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace Jewelry_store_management.VIEWMODEL
{
    public class SignUpViewModel:BaseViewModel
    {
        // khai báo
        public ICommand BackCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
         


        private UserHelper _userHelper;


        // Fields
        private string _userName;
        private string _securePassword;   
        private string _email;
        private string _secureRepassword;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

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

        public string Repassword
        {
            private get => _secureRepassword;
            set
            {
                _secureRepassword = value;
                OnPropertyChanged(nameof(Repassword));
            }
        }


        // hàm khởi tạo 
        public SignUpViewModel()
        {

            BackCommand = new RelayCommand(async _ => await BackClick());
            SignUpCommand = new RelayCommand(async _ => await SignUpClick());
            _userHelper = new UserHelper();
        }


        // các hàm chức năng
        private async Task BackClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 0;
        }
        private async Task SignUpClick()
        {
            // nếu kiểm tra input hợp lệ thì gọi hàm đăng ký
            if (ValidateInputs())
            {
                
                
                var newUser = new User
                {
                    Uid = GenerateId(),
                    Name = UserName,
                    Password = Password,
                    Email = Email

                };


                bool isRegistered = await _userHelper.RegisterUser(newUser);

                if (isRegistered)
                {
                    MessageBox_Window.ShowDialog("Đăng ký thành công!", "Thành công", "\\Drawable\\Icons\\icon_success", MessageBox_Window.MessageBoxButton.OK);

                     
                    // nếu đăng ký thành công thì quay về đăng nhập
                    var window = Application.Current.MainWindow;
                    var viewModel = (window.DataContext as StartViewModel);
                    viewModel.NView = 0;
                }
                else
                {
                    MessageBox_Window.ShowDialog("Email đã tồn tại, vui lòng sử dụng email khác.", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);

                   
                }


            }

             
           
        }



        // phương thức kiểm tra đầu vào
        private bool ValidateInputs()
        {
            // kiểm tra họ và tên 
            if (string.IsNullOrEmpty(UserName))
            {
                MessageBox_Window.ShowDialog("Tên người dùng không hợp lệ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);

                return false;
            }
            // Kiểm tra định dạng email
            if (string.IsNullOrEmpty(Email) || !IsValidEmail(Email))
            {
                MessageBox_Window.ShowDialog("Email không hợp lệ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);

                return false;
            }

            // Kiểm tra password phải có trên 8 ký tự và chứa cả chữ cái và số
            if (string.IsNullOrEmpty(Password) || Password.Length < 8 || !Password.Any(char.IsLetter) || !Password.Any(char.IsDigit))
            {
                MessageBox_Window.ShowDialog("Password phải có ít nhất 8 ký tự, bao gồm cả chữ cái và số.", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
 
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

        // phương thức kiểm tra định dạng email
        private bool IsValidEmail(string email)
        {
            // Sử dụng Regex để kiểm tra định dạng email
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private static Random random = new Random();
        public static string GenerateId()
        {
            const string prefix = "AD"; // dinh nghia tien to co dinh

            // tao 4 so ngan nhien
            int randomNumber = random.Next(10000);

            // dinh dang thanh chuoi 4 ky tu
            string randomNumberString = randomNumber.ToString("D4");

            // ket hop thanh ID
            string id = prefix + randomNumberString;

            return id;
        }


    }
}
