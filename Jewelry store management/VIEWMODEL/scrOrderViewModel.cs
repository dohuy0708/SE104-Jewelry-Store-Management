using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;

namespace Jewelry_store_management.VIEWMODEL
{
    public class scrOrderViewModel : BaseViewModel
    {

        public ICommand SearchCommand { get; set; }
        public ICommand AddOrderCommand { get; set; }

        private ObservableCollection<SaleOrder> orderEntries;

        public ObservableCollection<SaleOrder> OrderEntries
        {
            get { return orderEntries; }
            set
            {
                orderEntries = value;
                OnPropertyChanged();
            }
        }

     

        public scrOrderViewModel()
        {
            // Khởi tạo danh sách đơn hàng
            OrderEntries = new ObservableCollection<SaleOrder>();
       

            // Khởi tạo lệnh tìm kiếm
            SearchCommand = new RelayCommand(Search);
           AddOrderCommand = new RelayCommand(async _ => await AddOrderClick());
        }

        private async Task AddOrderClick()
        {

            var BillView = new BillView
            {
                DataContext = new BillViewModel()
            };
            BillView.ShowDialog();
        }

        private void Search(object parameter)
        {
            // Thực hiện logic tìm kiếm ở đây
        }
       
    }
}
