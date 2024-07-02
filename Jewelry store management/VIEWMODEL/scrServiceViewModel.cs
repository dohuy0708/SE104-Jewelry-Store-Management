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
        public ICommand ShowDetailCommand { get; set; }
        public ICommand DeleteRowCommand { get; set; }
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

        private ServiceOrder selectedServiceOrder;
        public ServiceOrder SelectedServiceOrder
        {
            get { return selectedServiceOrder; }
            set
            {
                selectedServiceOrder = value;
                OnPropertyChanged();
                 
            }
        }

        // hàm main 


        public scrServiceViewModel()
        {
            // Khởi tạo danh sách đơn hàng
            ServiceEntries = new ObservableCollection<ServiceOrder>();
            _serviceOrderHelper = new ServiceOrderHelper();
           

            // Sửa lỗi khởi tạo RelayCommand cho phương thức không đồng bộ
            DeleteRowCommand = new RelayCommand<ServiceOrder>(async serviceOrder => await DeleteRow(serviceOrder));

            // Khởi tạo lệnh tìm kiếm
            SearchCommand = new RelayCommand(Search);
            AddServiceCommand = new RelayCommand(async _ => await AddClick());
            ShowDetailCommand = new RelayCommand<ServiceOrder>(ShowDetail);
            LoadServiceEntries();
        }

        private async Task DeleteRow(ServiceOrder serviceOrder)
        {
            // Hiển thị thông báo xác nhận xóa
            MessageBox_Window.ShowDialog("Bạn chắc chắn xóa đơn dịch vụ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OkCancel);

            // Nếu người dùng chọn OK để xác nhận xóa
            if (MessageBox_Window.buttonResultClicked == MessageBox_Window.ButtonResult.OK)
            {
                if (serviceOrder != null)
                {
                    try
                    {
                        // Xóa đơn dịch vụ từ Firebase
                        await _serviceOrderHelper.DeleteServiceOrder(serviceOrder.ServiceID);

                        // Xóa đơn dịch vụ khỏi danh sách trong ViewModel
                        ServiceEntries.Remove(serviceOrder);

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

        // sự kiện sau khi click vào 1 hàng thì show detail lên 
        private void ShowDetail(ServiceOrder serviceOrder)
        {
            ServiceEntries.Clear();

            if (serviceOrder != null)
            {
                var addSerView = new ReviewService
                {
                    DataContext = new ReviewServiceViewModel(serviceOrder)
                };
                addSerView.ShowDialog();
                SelectedServiceOrder = null;
            }
            else
            {
                MessageBox_Window.ShowDialog("Service order is null!", "Error", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
            }

            LoadServiceEntries();
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

        private List<ServiceOrder> allServiceOrders;

        public string RemoveVietnameseDiacritics(string text)
        {
            string[] vietnameseChars = new string[]
            {
            "áàảãạâấầẩẫậăắằẳẵặ",
            "éèẻẽẹêếềểễệ",
            "íìỉĩị",
            "óòỏõọôốồổỗộơớờởỡợ",
            "úùủũụưứừửữự",
            "ýỳỷỹỵ",
            "đ",
            "ÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶ",
            "ÉÈẺẼẸÊẾỀỂỄỆ",
            "ÍÌỈĨỊ",
            "ÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ",
            "ÚÙỦŨỤƯỨỪỬỮỰ",
            "ÝỲỶỸỴ",
            "Đ"
            };

            char[] replaceChars = new char[]
            {
            'a', 'e', 'i', 'o', 'u', 'y', 'd',
            'A', 'E', 'I', 'O', 'U', 'Y', 'D'
            };

            for (int i = 0; i < vietnameseChars.Length; i++)
            {
                foreach (char c in vietnameseChars[i])
                {
                    text = text.Replace(c, replaceChars[i]);
                }
            }

            return text;
        }
        private async void Search(object parameter)
        {
            allServiceOrders= await _serviceOrderHelper.GetAllServiceOrders();
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                ServiceEntries.Clear();
                foreach (var serviceOrder in allServiceOrders)
                {
                    ServiceEntries.Add(serviceOrder);
                }
            }
            else
            {
                var lowerSearchText = RemoveVietnameseDiacritics(SearchText.ToLower());
                var filteredOrders = allServiceOrders.Where(o =>
                    (o.ServiceID != null && RemoveVietnameseDiacritics(o.ServiceID.ToLower()).Contains(lowerSearchText)) ||
                    (o.CustomerName != null && RemoveVietnameseDiacritics(o.CustomerName.ToLower()).Contains(lowerSearchText)) ||
                    (o.ServiceName != null && RemoveVietnameseDiacritics(o.ServiceName.ToLower()).Contains(lowerSearchText)) ||
                    (o.DateOrder.ToString().ToLower().Contains(lowerSearchText)) ||
                    (o.Status !=  null && RemoveVietnameseDiacritics(o.Status.ToString().ToLower()).Contains(lowerSearchText)) ||
                    
                    (o.ServiceID != null && RemoveVietnameseDiacritics(o.ServiceID.ToLower()) == lowerSearchText.ToLower()) ||
                    (o.ServiceName != null && RemoveVietnameseDiacritics(o.ServiceName.ToLower()) == lowerSearchText.ToLower()) ||
                    (o.CustomerName != null && RemoveVietnameseDiacritics(o.CustomerName.ToLower()) == lowerSearchText.ToLower()) ||
                    (o.Status != null && RemoveVietnameseDiacritics(o.Status.ToLower()) == lowerSearchText.ToLower()) ||
                    (o.DateOrder.ToString().ToLower() == lowerSearchText.ToLower())||
                    (o.TotalPrice.ToString().ToLower() == lowerSearchText.ToLower())
                ).ToList();

                ServiceEntries.Clear();
                foreach (var order in filteredOrders)
                {
                    ServiceEntries.Add(order);
                }
            }
        }
       
    }
}
