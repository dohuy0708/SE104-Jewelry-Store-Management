using Jewelry_store_management.HELPER;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Jewelry_store_management.VIEWMODEL
{
    public   class ForgetPasswordViewModel:BaseViewModel
    {
      
        // khai báo
        public ICommand BackCommand { get; set; }
        public ICommand GetCodeCommand { get; set; }
        private UserHelper _userHelper;

        // field
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string _text;
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
        public ForgetPasswordViewModel()
        {
            BackCommand = new RelayCommand(async _ => await BackClick());
            GetCodeCommand = new RelayCommand(async _ => await GetCodeClick());
            _userHelper = new UserHelper();
        }

      


        // hàm chức năng 
        private async Task BackClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 0;
        }
        private async Task GetCodeClick()
        {
            Text = "Vui lòng chờ trong giây lát!";

            if (ValidateInputs())
            {
                // gán email vào biến toàn cục 
                GlobalVariables.VerificationEmail = Email;
                // kiếm tra user có tài khoản trước đó chưa

                var user = await _userHelper.GetUserByEmail(Email);
                if (user != null)
                {
                    // hàm gửi code qua email
                    await SendCode();

                   

                    var window = Application.Current.MainWindow;
                    var viewModel = (window.DataContext as StartViewModel);
                    viewModel.NView = 3;


                }
                else
                {
                    MessageBox_Window.ShowDialog("Email không tồn tại! Vui lòng nhập đúng Email của tài khoản!", "Lỗi", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
                }
            }
        }
        private bool ValidateInputs()
        {
            // Kiểm tra định dạng email
            if ( string.IsNullOrEmpty(Email))
            {
                MessageBox_Window.ShowDialog("Vui lòng nhập Email !", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
                return false;
            }
            if (!IsValidEmail(Email))
            {
                MessageBox_Window.ShowDialog("Email không hợp lệ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
                return false;
            }
            

            return true;
        }
        private bool IsValidEmail(string email)
        {
            // Sử dụng Regex để kiểm tra định dạng email
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        
        
        public async Task SendCode()
        {
            // tạo mã code
            string code = GenerateCode();
            // lưu giá trị của code vào biến toàn cục thuộc lớp tĩnh, để có thể truy cập bên Verify
            GlobalVariables.VerificationCode = code;

            string fromMail = "huydonguyenh@gmail.com";
            string fromPass = "pmfceevwxoxsktxi";
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromMail);
            mailMessage.Subject = "XÁC NHẬN TÀI KHOẢN H- JEWELRY";
            mailMessage.To.Add(new MailAddress(Email));
            mailMessage.Body ="Mã xác nhận tài khoản của bạn là: "+ code +"\n Vui lòng không chia sẻ mã xác nhận cho người khác!";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPass),
                EnableSsl = true

            };
            smtpClient.Send(mailMessage);

         


        }
        private static Random random = new Random();
        public static string GenerateCode()
        {


            // tao 4 so ngan nhien
            int randomNumber = random.Next(1000000);

            // dinh dang thanh chuoi 4 ky tu
            string randomNumberString = randomNumber.ToString("D6");

            // ket hop thanh ID


            return randomNumberString;
        }


    }
}
