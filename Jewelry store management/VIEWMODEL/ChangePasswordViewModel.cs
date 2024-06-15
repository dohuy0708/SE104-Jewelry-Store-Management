using Jewelry_store_management.HELPER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Security.Cryptography;
using Jewelry_store_management.VIEW;
 

namespace Jewelry_store_management.VIEWMODEL
{
    public class ChangePasswordViewModel:BaseViewModel
    {
        private UserHelper _userHelper;
        public ICommand ChangePasswordCommand { get; set; }


        private string _username;
        private string _email;
        private string _address;
        private string _phone;
        private string _text;
        private string _uid;
     
        private string _password;

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
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

        public string PassWord
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(PassWord));
            }
        }
      
        public string UID
        {
            get => _uid;
            set
            {
                _uid = value;
                OnPropertyChanged(nameof(UID));
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
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }



     


        public ChangePasswordViewModel() 
        {
            _userHelper = new UserHelper();
            ChangePasswordCommand = new RelayCommand(async _ => await ChangePassword());
        }



        private async Task ChangePassword()
        {

            string email = GlobalVariables.CurrentEmail;

            var user = await _userHelper.GetUserByEmail(email);
            if (HashPassword(PassWord)!=user.Password && user != null)
            {
                MessageBox_Window.ShowDialog("Nhập sai mật khẩu", "Lỗi", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);

            }
            else
            {
                if (ValidateInputs())
                {
                   

                    if (user != null)
                    {
                               
                            user.Password = HashPassword(NewPassword);
                            Text="Hệ thống đang xử lý!";
                            await _userHelper.UpdateUser(user);

                            MessageBox_Window.ShowDialog("Mật khẩu đã được thay đổi thành công!", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OK);
                            CloseWindow();

                    }
                    else
                    {
                        MessageBox_Window.ShowDialog("Hệ thống phát hiện một số vấn đề vui lòng thử lại sau!", "Lỗi", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
                    }
                }
            }
          


        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                {
                    if (window.DataContext == this)
                    {
                        window.Close();
                        break;
                    }
                }
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(NewPassword))
            {
                MessageBox_Window.ShowDialog("Vui lòng nhập Mật khẩu!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
                return false;
            }

            // Kiểm tra password phải có trên 8 ký tự và chứa cả chữ cái và số
            if (NewPassword.Length < 8 || !NewPassword.Any(char.IsLetter) || !NewPassword.Any(char.IsDigit))
            {
                MessageBox_Window.ShowDialog("Mật khẩu phải có ít nhất 8 ký tự, bao gồm cả chữ cái và số.", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);


                return false;
            }
            // Kiểm tra password nhập lại có trùng với password không
            if (string.IsNullOrEmpty(ConfirmPassword)||(NewPassword!=ConfirmPassword))///
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

