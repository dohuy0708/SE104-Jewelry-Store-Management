
using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
 
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;
using System.Xml.Linq;
 


namespace Jewelry_store_management.VIEWMODEL
{
    public class scrAccountViewModel : BaseViewModel
    {
        private UserHelper _userHelper;
 



        // Commands
        public ICommand SignOutCommand { get; set; }
        public ICommand ChangePassCommand { get; set; }
        public ICommand ChangeInfoCommand { get; set; }
        public ICommand ChangeImageCommand { get; set; }

        // Fields
        private string _username;
        private string _email;
        private string _address;
        private string _phone;
        private string _buttonname;
        private string _uid;
        private bool _isEditingMode;
        private string _password;

        // Property to control editing mode
        public bool IsEditingMode
        {
            get => _isEditingMode;
            set
            {
                _isEditingMode = value;
                OnPropertyChanged(nameof(IsEditingMode));
            }
        }

        // Properties

        public string PassWord
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(PassWord));
            }
        }
        public string ButtonName
        {
            get => _buttonname;
            set
            {
                _buttonname = value;
                OnPropertyChanged(nameof(ButtonName));
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
        // List hình ảnh
        private BitmapImage image { get; set; }
        public BitmapImage Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }


        // Constructor
        public scrAccountViewModel()
        {
           
            _userHelper = new UserHelper();
           
            // Initialize commands
            SignOutCommand = new RelayCommand(async _ => await SignOut());
            ChangePassCommand = new RelayCommand(async _ => await ChangePassword());
            ChangeInfoCommand = new RelayCommand(async _ => await ChangeInformation());
            ChangeImageCommand = new RelayCommand(async _ => await ChangeImage());

            ButtonName="Chỉnh sửa thông tin";
            IsEditingMode = true;
            // Load user data
            LoadUserData();
        }

        // Methods to load user data
        private async void LoadUserData()
        {
            var email = GlobalVariables.CurrentEmail;
             
            if (!string.IsNullOrEmpty(email))
            {
                var user = await _userHelper.GetUserByEmail(email);


                

                if (user != null)
                {
                    UID = user.Uid;
                    Username = user.Name;
                    Email = user.Email;
              

                    if (user.Phone != null)
                    {
                        Phone =user.Phone;
                    }
                    else
                    {
                        Phone = "(Chưa cập nhật)";
                    }
                    if (user.Address != null)
                    {
                        Address =user.Address;
                    }
                    else
                    {
                        Address= "(Chưa cập nhật)";
                    }

                    PassWord = user.Password;



                    if (user.ImageURL != null )
                    {
                        Image = ConvertBase64ToBitmapImage(user.ImageURL);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }




        // Command methods
        private async Task SignOut()
        {

            // Thêm cái messagebox vào 
            MessageBox_Window.ShowDialog("Bạn chắc chắn muốn đăng xuất!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OkCancel);

            if (MessageBox_Window.buttonResultClicked == MessageBox_Window.ButtonResult.OK)
            {
                // Clear the global variables
                GlobalVariables.CurrentEmail = null;

                // Close the current window
                var currentWindow = Application.Current.MainWindow;

                // Open the StartView window
                var startView = new StartView();
                Application.Current.MainWindow = startView;
                startView.Show();

                // Close the current window
                currentWindow.Close();
            }    

                
        }   

        private async Task ChangePassword()
        {
            
            
            var changePasswordView = new ChangePassWordView
            {
                DataContext = new ChangePasswordViewModel()
            };
            changePasswordView.ShowDialog();
        }

        private async Task ChangeInformation()
        {
            string imagestring = null;
            string preview = null;
            if (Image!=null)
            {
                imagestring = ConvertBitmapImageToBase64(Image);
                 preview = imagestring.Substring(0, 10);
              

            }
            if (!IsEditingMode)
            { 

                
               User updatedUser = new User
                {
                    Name = Username,
                    Email = Email,
                    Uid = UID,
                    Phone = Phone,
                    
                    Address = Address,
                    Password = PassWord,
                    ImageURL = imagestring

                };
                 


                await _userHelper.UpdateUser(updatedUser);

                    MessageBox_Window.ShowDialog("Cập nhật thông tin thành công!", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OK);

                    // Disable editing mode
                    IsEditingMode = true;
                    ButtonName="Chỉnh sửa thông tin";
                 

            }
            else
            {
                // Enable editing mode
                    IsEditingMode = false;
                    ButtonName="Cập nhật thông tin";
            }
        }

        private async Task ChangeImage()
        {
            IsEditingMode = false;
            ButtonName="Cập nhật thông tin";


            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName, UriKind.Absolute);
                bitmap.EndInit();

                Image = bitmap;
                
            }

        }
        private string ConvertBitmapImageToBase64(BitmapImage bitmapImage)
        {


            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chuyển đổi hình ảnh: {ex.Message}");
                return null;
            }
        }

        private BitmapImage ConvertBase64ToBitmapImage(string base64String)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze(); // Optional: Freeze to make it cross-thread accessible
                    return bitmapImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chuyển đổi chuỗi Base64 thành hình ảnh: {ex.Message}");
                return null;
            }
        }
    }
}
