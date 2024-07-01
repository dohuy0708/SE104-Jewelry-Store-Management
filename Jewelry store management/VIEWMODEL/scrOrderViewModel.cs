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
        public ICommand ShowDetailCommand { get; set; }
        public ICommand DeleteRowCommand { get; set; }

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

        private SaleOrder selectedSaleOrder;
        public SaleOrder SelectedSaleOrder
        {
            get { return selectedSaleOrder; }
            set
            {
                selectedSaleOrder = value;
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

            DeleteRowCommand = new RelayCommand<SaleOrder>(async saleOrder => await DeleteRow(saleOrder));
            ShowDetailCommand = new RelayCommand<SaleOrder>(ShowDetail);


            LoadAllSaleOrders();


        }



        private async Task DeleteRow(SaleOrder saleOrder)
        {
            // Hiển thị thông báo xác nhận xóa
            MessageBox_Window.ShowDialog("Bạn chắc chắn xóa đơn dịch vụ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OkCancel);

            // Nếu người dùng chọn OK để xác nhận xóa
            if (MessageBox_Window.buttonResultClicked == MessageBox_Window.ButtonResult.OK)
            {
                if (saleOrder != null)
                {
                    try
                    {
                        // Xóa đơn dịch vụ từ Firebase
                        await _saleOrderHelper.DeleteSaleOrder(saleOrder.SaleId);

                        // Xóa đơn dịch vụ khỏi danh sách trong ViewModel
                        OrderEntries.Remove(saleOrder);

                        // Hiển thị thông báo thành công
                        MessageBox_Window.ShowDialog("Xóa đơn dịch vụ thành công!", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OK);
                    }
                    catch (Exception ex)
                    {
                        // Hiển thị thông báo lỗi nếu việc xóa gặp lỗi
                        MessageBox.Show($"Error deleting service order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // Hiển thị thông báo lỗi nếu `serviceOrder` là null
                    MessageBox.Show("Error: Service order is null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }



        private void ShowDetail(SaleOrder saleOrder)
        {
            OrderEntries.Clear();

            if (saleOrder != null)
            {
                var addSerView = new ReviewBill
                {
                    DataContext = new ReviewBillViewModel(saleOrder)
                };
                addSerView.ShowDialog();
                SelectedSaleOrder = null;
            }
            else
            {
                MessageBox_Window.ShowDialog("Service order is null!", "Error", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
            }

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
