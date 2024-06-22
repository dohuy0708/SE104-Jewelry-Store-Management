using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jewelry_store_management.VIEWMODEL
{
    public class scrServiceViewModel:BaseViewModel
    {
        // khai báo command
        public ICommand SearchCommand { get; set; }
        public ICommand AddServiceCommand { get; set; }


        private ObservableCollection<ServiceOrder> serviceEntries;

        public ObservableCollection<ServiceOrder> ServiceEntries
        {
            get { return serviceEntries; }
            set
            {
                serviceEntries = value;
                OnPropertyChanged();
            }
        }

      

        public scrServiceViewModel()
        {
            // Khởi tạo danh sách đơn hàng
            ServiceEntries = new ObservableCollection<ServiceOrder>();
        

            // Khởi tạo lệnh tìm kiếm
            SearchCommand = new RelayCommand(Search);
            AddServiceCommand = new RelayCommand(async _ => await AddClick());
        }

        private async Task AddClick()
        {

            var addSerView = new ServiceView           
            {
                DataContext = new ServiceViewModel()
            };
            addSerView.ShowDialog();
        }

        private void Search(object parameter)
        {
            // Thực hiện logic tìm kiếm ở đây
        }
       
    }
}
