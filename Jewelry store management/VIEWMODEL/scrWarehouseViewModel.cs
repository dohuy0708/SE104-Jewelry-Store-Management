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
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Windows.Data;

namespace Jewelry_store_management.VIEWMODEL
{
    public class scrWarehouseViewModel : BaseViewModel
    {
        private ProductHelper _productHelper;

        // Khai báo command
        public ICommand SearchCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand FilterCommand { get; set; }

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
        private Visibility isFilter;
        public Visibility IsFilter
        {
            get { return isFilter; }
            set
            {
                isFilter = value;
                OnPropertyChanged();
            }
        }
        //Danh sách các loại sản phẩm
        public ObservableCollection<String> TypeList;
        //Danh sách các loại đã chọn filter
        public ObservableCollection<Filter> TypeFilterList { get; set; }
        //Danh sách các nguyên liệu
        public ObservableCollection<String> MaterialList;
        //Danh sách các nguyên liệu đã chọn filter
        public ObservableCollection<Filter> MaterialFilterList { get; set; }

        //Danh sách các Sản phẩm sau khi Filter
        public ICollectionView FilteredProductList { get; set; }

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

        private double _priceRange;
        public double PriceRange
        {
            get { return _priceRange; }
            set
            {
                _priceRange = value;
                OnPropertyChanged();
                UpdateMinPrice();
                FilteredProductList.Refresh();
            }
        }

        private double? _minPrice;
        public double? MinPrice
        {
            get { return _minPrice; }
            set
            {
                if (_minPrice != value)
                {
                    _minPrice = value;
                    OnPropertyChanged();
                    if(FilteredProductList!=null)
                    FilteredProductList.Refresh();
                }
            }
        }
        private double? _maxPrice;
        public double? MaxPrice
        {
            get { return _maxPrice; }
            set
            {
                if (_maxPrice != value)
                {
                    _maxPrice = value;
                    OnPropertyChanged();
                }
            }
        }



        // Hàm chính    
        public scrWarehouseViewModel()
        {
            IsFilter = Visibility.Collapsed;
            _productHelper = new ProductHelper();
            //

            // Khởi tạo danh sách product
            ProductEntries = new ObservableCollection<Product>();
            //Khởi tạo danh sách loại
            TypeList = new ObservableCollection<String> { "Nhẫn", "Bông tai", "Dây chuyền", "Vòng tay", "Thần tài" };
            //Khởi tạo danh sách nguyên liệu
            MaterialList = new ObservableCollection<string> { "Vàng 9999", "Vàng 24k", "Vàng Trắng", "Vàng Tây", "Vàng Ý", "Bạc" };
            //Lấy danh sách Type Filter đã được chọn
            TypeFilterList = new ObservableCollection<Filter>(TypeList.Select(t => new Filter { FilterName = t }));
            //Lấy danh sách Material Filter đã được chọn
            MaterialFilterList = new ObservableCollection<Filter>(MaterialList.Select(m => new Filter { FilterName = m }));

            foreach (var filter in TypeFilterList)
            {
                filter.PropertyChanged += FilterOption_PropertyChanged;
            }
            foreach (var filter in MaterialFilterList)
            {
                filter.PropertyChanged += FilterOption_PropertyChanged;
            }
            MinPrice = 0;
            MaxPrice = 0;

            LoadAllProducts();
            //Lấy danh sách sản phẩm phù hợp với các filter
            FilteredProductList = CollectionViewSource.GetDefaultView(ProductEntries);
            FilteredProductList.Filter = FilterItems;
            FilteredProductList.Refresh();
            FilterCommand = new RelayCommand(_ => filter());
            AddCommand = new RelayCommand(async _ => await AddClick()); // True để bật lệnh mặc định
            SearchCommand = new RelayCommand(Search);
            ShowDetailCommand = new RelayCommand<Product>(ShowDetail);
            // Lấy tất cả sản phẩm khi khởi tạo ViewModel
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

        private void FilterOption_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Filter.IsSelected))
            {
                FilteredProductList.Refresh();
            }
        }

        private bool FilterItems(object product)
        {
            if (product is Product currentProduct)
            {
                bool typeSelected = TypeFilterList.Any(f => f.IsSelected);
                bool materialSelected = MaterialFilterList.Any(f => f.IsSelected);

                bool typeMatch = !typeSelected || TypeFilterList.Any(f => f.IsSelected && f.FilterName == currentProduct.Type);
                bool materialMatch = !materialSelected || MaterialFilterList.Any(f => f.IsSelected && f.FilterName == currentProduct.Material);
                bool priceMatch = true;

                if (MinPrice.HasValue)
                {
                    priceMatch = (double)currentProduct.SalePrice >= MinPrice.Value;
                }

                return typeMatch && materialMatch && priceMatch;
            }
            return false;
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

        private List<Product> allProducts;

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

        private async void Search(object parameter) //ma sp, ten, gia, size
        {
            allProducts= await _productHelper.GetAllProducts();
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                ProductEntries.Clear();
                foreach (var product in allProducts)
                {
                    ProductEntries.Add(product);
                }
            }
            else
            {
                var lowerSearchText = RemoveVietnameseDiacritics(SearchText.ToLower());
                var filteredProduct = allProducts.Where(o =>
                    (o.PID != null && RemoveVietnameseDiacritics(o.PID.ToLower()).Contains(lowerSearchText)) ||
                    (o.Name != null && RemoveVietnameseDiacritics(o.Name.ToLower()).Contains(lowerSearchText)) ||
                    (o.Size != null && o.Size.ToLower().Contains(lowerSearchText)) ||


                    (o.PID != null && RemoveVietnameseDiacritics(o.PID.ToLower()) == lowerSearchText.ToLower()) ||
                    (o.Name != null && RemoveVietnameseDiacritics(o.Name.ToLower()) == lowerSearchText.ToLower()) ||
                    (o.Size != null && o.Size.ToLower() == lowerSearchText.ToLower()) ||
                    (o.SalePrice.ToString().ToLower() == lowerSearchText.ToLower())

                ).ToList();

                ProductEntries.Clear();
                foreach (var product in filteredProduct)
                {
                    ProductEntries.Add(product);
                }
            }

        }
        private void filter()
        {
            if (IsFilter == Visibility.Collapsed)
            {
                IsFilter = Visibility.Visible;
            }

            else
            {
                IsFilter = Visibility.Collapsed;
            }
        }
        private void UpdateMinPrice()
        {
            double range = PriceRange / 1000; // Chia cho 1000 vì Slider có Maximum là 1000
            MaxPrice = (double)ProductEntries.Max(product => product.SalePrice);
            MinPrice = (double)ProductEntries.Max(product => product.SalePrice) * range;
        }
    }
    //Định nghĩa class Filter
    public class Filter : BaseViewModel
    {
        private bool isSelected;
        public string FilterName { get; set; }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }
    }

}
