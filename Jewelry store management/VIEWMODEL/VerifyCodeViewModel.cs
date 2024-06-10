using Jewelry_store_management.HELPER;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Jewelry_store_management.VIEWMODEL
{
    public class VerifyCodeViewModel: BaseViewModel
    {
        // khai báo 

        public ICommand BackCommand { get; set; }
        public ICommand VerifyCommand { get; set; }

        //field
        public string _code;
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }


        // ham chinh
        public VerifyCodeViewModel() 
        {
            BackCommand = new RelayCommand(async _ => await BackClick());
            VerifyCommand = new RelayCommand(async _ => await VerifyClick());
            

        }
        // Constructor
      
      

        // ham chuc nang
        private async Task VerifyClick()
        {

            if (Code == GlobalVariables.VerificationCode)
            {

                var window = Application.Current.MainWindow;
                var viewModel = (window.DataContext as StartViewModel);
                viewModel.NView = 4;
            }
            else
            {
                MessageBox_Window.ShowDialog("Mã xác nhận không chính xác!", "Lỗi", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);

            }
        }


        
        private async Task BackClick()
        {
             // chuyển về màn hình Quên MK
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 2;
        }

       

    }
}
