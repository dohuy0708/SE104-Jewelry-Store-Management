using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.HELPER;
using Jewelry_store_management.VIEW;

namespace Jewelry_store_management.VIEWMODEL
{
    public class scrWarehouseViewModel : BaseViewModel
    {
        private ProductHelper _productHelper;

        // Khai báo command
        public ICommand SearchCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ShowDetailCommand { get; set; }

        // Khai báo trường dữ liệu
        private ObservableCollection<Product> productEntries;
        public ObservableCollection<Product> ProductEntries
        {
            get { return productEntries; }
            set
            {
                productEntries = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<String> CategoryList;
        public ObservableCollection<String> TypeList;
        public ObservableCollection<String> MaterialList;
       // public ObservableCollection<Filter> CategoryFilterList;
        public ICollectionView CategoryFilteredItems { get; set; }


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

        // Các thuộc tính cho sản phẩm
        private string pid;
        public string PID
        {
            get { return pid; }
            set
            {
                pid = value;
                OnPropertyChanged();
            }
        }

        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }

       
        


        // Hàm chính    
        public scrWarehouseViewModel()
        {
            _productHelper = new ProductHelper();

            // Khởi tạo danh sách product
            ProductEntries = new ObservableCollection<Product>();

            AddCommand = new RelayCommand(async _ => await AddClick()); // True để bật lệnh mặc định
            SearchCommand = new RelayCommand(Search);
            ShowDetailCommand = new RelayCommand<Product>(ShowDetail);
            // Lấy tất cả sản phẩm khi khởi tạo ViewModel
            LoadAllProducts();
        }

        private void ShowDetail(Product pro)
        {
            ProductEntries.Clear();

            if (pro != null)
            {
                var addSerView = new ReviewAddProduct
                {
                    DataContext = new ReviewAddProductViewModel(pro)
                };
                addSerView.ShowDialog();
               // SelectedServiceOrder = null;
            }
            else
            {
                MessageBox_Window.ShowDialog("Service order is null!", "Error", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
            }

            LoadAllProducts();
       }





        private async void LoadAllProducts()
        {
            try
            {
                var products = await _productHelper.GetAllProducts();
                ProductEntries.Clear();
                foreach (var product in products)
                {
                    // Kiểm tra và xử lý trường hợp dữ liệu null ở đây
                    if (product.ImageURL == null)
                    {
                        // Nếu ImageURL null, có thể gán một giá trị mặc định hoặc báo lỗi
                        product.ImageURL = "/Drawable/Images/Logo.png"; // Đường dẫn tới hình ảnh mặc định
                    }
                    ProductEntries.Add(product);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây (ví dụ: logging, thông báo lỗi cho người dùng, ...)
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
       /* private bool FilterItems(object item)
        {
            if (item is Product currentItem)
            {
                return CategoryFilterList.Any(f => f.IsSelected && f.Name == currentItem.Category);
            }
            return false;
        }
*/
        private async Task AddClick()
        {
            try
            {
                var addProView = new AddProductView
                {
                    DataContext = new AddProductViewModel()
                };
                addProView.ShowDialog();
                // Sau khi thêm sản phẩm mới, tải lại danh sách sản phẩm
                  LoadAllProducts();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây (ví dụ: logging, thông báo lỗi cho người dùng, ...)
                MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Search(object parameter)
        {
            // Thực hiện logic tìm kiếm sản phẩm theo tên
           
        }
    }
}
