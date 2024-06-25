using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;


namespace Jewelry_store_management.VIEWMODEL
{
    public class scrOrderViewModel : BaseViewModel
    {
        private readonly SaleOrderHelper _saleOrderHelper;
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

        private string searchText;
        public string SearchText 
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }

        public scrOrderViewModel()
        {
            _saleOrderHelper = new SaleOrderHelper();

            // Khởi tạo danh sách đơn hàng
            OrderEntries = new ObservableCollection<SaleOrder>();
       

            // Khởi tạo lệnh tìm kiếm
            SearchCommand = new RelayCommand(Search);
           AddOrderCommand = new RelayCommand(async _ => await AddOrderClick());
            LoadAllSaleOrders();


        }
        private async void LoadAllSaleOrders()
        {
            var allSaleOrders = await _saleOrderHelper.GetAllSaleOrders();
            OrderEntries.Clear();
            foreach (var saleOrder in allSaleOrders)
            {
                OrderEntries.Add(saleOrder);
            }
        }
        private async Task AddOrderClick()
        {

            var BillView = new BillView
            {
                DataContext = new BillViewModel()
            };
            BillView.ShowDialog();
            LoadAllSaleOrders();

        }

        private void Search(object parameter)
        {

        }
       
    }
}
