using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
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

            BackCommand = new RelayCommand(_ => BackClick());
            SignUpCommand = new RelayCommand(_ => SignUpClick());
            _userHelper = new UserHelper();
        }


        // các hàm chức năng
        private void BackClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 0;
        }
        private async void SignUpClick()
        {
            // nếu kiểm tra input hợp lệ thì gọi hàm đăng ký
            if (ValidateInputs())
            {
                
                
                var newUser = new User
                {
                    Name = UserName,
                    Password = Password,
                    Email = Email

                };


                bool isRegistered = await _userHelper.RegisterUser(newUser);

                if (isRegistered)
                {
                    MessageBox.Show("Đăng ký thành công!");
                    // nếu đăng ký thành công thì quay về đăng nhập
                    var window = Application.Current.MainWindow;
                    var viewModel = (window.DataContext as StartViewModel);
                    viewModel.NView = 0;
                }
                else
                {
                    MessageBox.Show("Email đã tồn tại, vui lòng sử dụng email khác.");
                }

            }

             
           
        }



        // phương thức kiểm tra đầu vào
        private bool ValidateInputs()
        {
            // kiểm tra họ và tên 
            if (string.IsNullOrEmpty(UserName))
            {
                MessageBox.Show("Tên người dùng không hợp lệ!");
                return false;
            }
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

            // Kiểm tra password nhập lại có trùng với password không
            if (string.IsNullOrEmpty(Repassword)||(Password!=Repassword))///
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp.");
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
