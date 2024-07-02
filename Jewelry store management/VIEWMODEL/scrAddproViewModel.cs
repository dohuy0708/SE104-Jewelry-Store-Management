using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

 
 

namespace Jewelry_store_management.VIEWMODEL
{
    public class scrAddproViewModel:BaseViewModel
    {
        public ICommand SearchCommand { get; set; }
        public ICommand PurchaseProCommand { get; set; }


        public ICommand ShowDetailCommand { get; set; }
        public ICommand DeleteRowCommand { get; set; }


        private readonly PurchaseOrderHelper _purchaseOrderHelper;

        private ObservableCollection<PurchaseOrder> addproEntries;

        public ObservableCollection<PurchaseOrder> AddproEntries
        {
            get { return addproEntries; }
            set
            {
                addproEntries = value;
                OnPropertyChanged(nameof(AddproEntries));
            }
        }
        private PurchaseOrder selectedServiceOrder;
        public PurchaseOrder SelectedAddProOrder
        {
            get { return selectedServiceOrder; }
            set
            {
                selectedServiceOrder= value;
                OnPropertyChanged();

            }
        }


        public scrAddproViewModel()
        {
            _purchaseOrderHelper = new PurchaseOrderHelper();
            // Khởi tạo danh sách nhap
            AddproEntries = new ObservableCollection<PurchaseOrder>();
       

            // Khởi tạo lệnh tìm kiếm

            SearchCommand = new RelayCommand(Search);
            PurchaseProCommand = new RelayCommand(async _ => await AddProClick());

            DeleteRowCommand = new RelayCommand<PurchaseOrder>(async purchaseOrder => await DeleteRow(purchaseOrder));
            ShowDetailCommand = new RelayCommand<PurchaseOrder>(ShowDetail);
            LoadPurchase();
        }


        private void ShowDetail(PurchaseOrder purchaseOrder)
        {
            AddproEntries.Clear();

            if (purchaseOrder!= null)
            {
                var addView = new ReviewPurchaseOrder
                {
                    DataContext = new ReviewPurchaseOrderViewModel(purchaseOrder)
                };
                addView.ShowDialog();
                SelectedAddProOrder = null;
            }
            else
            {
                MessageBox_Window.ShowDialog("Service order is null!", "Error", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
            }

            LoadPurchase();
        }


        private async Task DeleteRow(PurchaseOrder purchaseOrder)
        {
            // Hiển thị thông báo xác nhận xóa
            MessageBox_Window.ShowDialog("Bạn chắc chắn xóa đơn dịch vụ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OkCancel);

            // Nếu người dùng chọn OK để xác nhận xóa
            if (MessageBox_Window.buttonResultClicked == MessageBox_Window.ButtonResult.OK)
            {
                if (purchaseOrder != null)
                {
                    try
                    {
                        // Xóa đơn dịch vụ từ Firebase
                        await _purchaseOrderHelper.DeletePurchaseOrder(purchaseOrder.PurchaseID);

                        // Xóa đơn dịch vụ khỏi danh sách trong ViewModel
                        AddproEntries.Remove(purchaseOrder);

                        // Hiển thị thông báo thành công
                        MessageBox_Window.ShowDialog("Xóa đơn mua hàng thành công!", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OK);
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

        private async Task LoadPurchase()
        {
            try
            {
                // Fetch all purchase orders from Firebase
                var purchaseOrders = await _purchaseOrderHelper.GetAllPurchaseOrders();

                // Clear existing entries
                AddproEntries.Clear();

                // Add fetched purchase orders to the collection
                foreach (var purchaseOrder in purchaseOrders)
                {
                   
                    AddproEntries.Add(purchaseOrder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase orders: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AddProClick()
        {
            

            var PurchaseProView = new PurchaseOrderView
            {
                DataContext = new PurchaseOderViewModel()
            };
           PurchaseProView.ShowDialog();

            LoadPurchase();

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

        private List<PurchaseOrder> allPurchaseOrders;


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
            allPurchaseOrders= await _purchaseOrderHelper.GetAllPurchaseOrders();
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                AddproEntries.Clear();
                foreach (var purchaseOrder in allPurchaseOrders)
                {
                    AddproEntries.Add(purchaseOrder);
                }
            }
            else
            {
                var lowerSearchText = RemoveVietnameseDiacritics(SearchText.ToLower());
                var filteredOrders = allPurchaseOrders.Where(o =>
                    (o.PurchaseID != null && RemoveVietnameseDiacritics(o.PurchaseID.ToLower()).Contains(lowerSearchText)) ||
                    (o.SupplierName != null && RemoveVietnameseDiacritics(o.SupplierName.ToLower()).Contains(lowerSearchText)) ||
                    (o.DatePurchase.ToString().ToLower().Contains(lowerSearchText)) ||


                    (o.PurchaseID != null && RemoveVietnameseDiacritics(o.PurchaseID.ToLower()) == lowerSearchText.ToLower()) ||
                    (o.SupplierName != null && RemoveVietnameseDiacritics(o.SupplierName.ToLower()) == lowerSearchText.ToLower()) ||
                    (o.DatePurchase.ToString().ToLower() == lowerSearchText.ToLower()) ||
                    (o.TotalPrice.ToString().ToLower() == lowerSearchText.ToLower())

                ).ToList();

                AddproEntries.Clear();
                foreach (var order in filteredOrders)
                {
                    AddproEntries.Add(order);
                }
            }
        }   

       
    }
}
