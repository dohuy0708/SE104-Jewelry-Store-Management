using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Jewelry_store_management.VIEWMODEL
{
    public class SignUpViewModel
    {
        public ICommand BackCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        private void BackClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 0;
        }
        private void SignUpClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 0;
        }
        public SignUpViewModel()
        {
            BackCommand = new RelayCommand(_ => BackClick());
            SignUpCommand = new RelayCommand(_ => SignUpClick());
        }
    }
}
