using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Jewelry_store_management.VIEWMODEL
{
    public class ForgetPasswordViewModel
    {
        public ICommand BackCommand { get; set; }
        private void BackClick()
        {
            var window = Application.Current.MainWindow;
            var viewModel = (window.DataContext as StartViewModel);
            viewModel.NView = 0;
        }
        public ForgetPasswordViewModel()
        {
            BackCommand = new RelayCommand(_ => BackClick());
        }
    }
}
