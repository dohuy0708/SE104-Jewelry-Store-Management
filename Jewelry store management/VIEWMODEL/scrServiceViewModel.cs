using Jewelry_store_management.HELPER;
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
using System.Windows;
using System.Windows.Input;

namespace Jewelry_store_management.VIEWMODEL
{
    public class scrServiceViewModel:BaseViewModel
    {
        // khai báo command
        public ICommand SearchCommand { get; set; }
        public ICommand AddServiceCommand { get; set; }

        private readonly ServiceOrderHelper _serviceOrderHelper;

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

      


        // hàm main 


        public scrServiceViewModel()
        {
            // Khởi tạo danh sách đơn hàng
            ServiceEntries = new ObservableCollection<ServiceOrder>();
            _serviceOrderHelper = new ServiceOrderHelper();

            // Khởi tạo lệnh tìm kiếm
            SearchCommand = new RelayCommand(Search);
            AddServiceCommand = new RelayCommand(async _ => await AddClick());

            LoadServiceEntries();
        }

        private async void LoadServiceEntries()
        {
            try
            {
                var serviceOrders = await _serviceOrderHelper.GetAllServiceOrders();
                ServiceEntries.Clear();
                foreach (var serviceOrder in serviceOrders)
                {
                    ServiceEntries.Add(serviceOrder);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây (ví dụ: logging, thông báo lỗi cho người dùng, ...)
                MessageBox.Show($"Error loading service orders: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AddClick()
        {

            var addSerView = new ServiceView           
            {
                DataContext = new ServiceViewModel()
            };
            addSerView.ShowDialog();

            LoadServiceEntries();
        }

        private void Search(object parameter)
        {
            // Thực hiện logic tìm kiếm ở đây
        }
       
    }
}
