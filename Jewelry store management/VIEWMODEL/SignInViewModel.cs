using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Jewelry_store_management.VIEWMODEL
{
    public class SignInViewModel{ 
        public ICommand SignUpCommand{ get; set; }
        public ICommand ForgetPasswordCommand { get; set; }
        private void SignUpClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 1;
        }
        private void ForgetPasswordClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 2;
        }
        public SignInViewModel()
        {
            SignUpCommand = new RelayCommand(_ => SignUpClick());
            ForgetPasswordCommand = new RelayCommand(_ => ForgetPasswordClick());
        }
    }
}
