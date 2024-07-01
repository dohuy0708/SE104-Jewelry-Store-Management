using Jewelry_store_management.MODELS;
using Jewelry_store_management.HELPER;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Jewelry_store_management.VIEW;
using System.Windows.Media;
using System.Windows;

namespace Jewelry_store_management.VIEWMODEL
{
    public class BillViewModel : BaseViewModel
    {
        private readonly SaleOrderHelper _saleOrderHelper;
        private readonly ProductHelper _productHelper;
        public ICommand DeleteRowCommand { get; set; }

        public BillViewModel()
        {
            _saleOrderHelper = new SaleOrderHelper();
            _productHelper = new ProductHelper();

            Productlist = new ObservableCollection<Product>();
            ListProduct = new ObservableCollection<Product>();

            DateOrder= DateTime.Now;
            LoadProducts();

            AddProductCommand = new RelayCommand(async _ => await AddProduct());
            AddOrderCommand = new RelayCommand(async _ => await AddOrder());


            DeleteRowCommand = new RelayCommand<Product>(async product => await DeleteRow(product));
        }


        // load lên combobox

        private async Task DeleteRow(Product product)
        {

            if (product != null)
            {
                try
                {

                    // Xóa đơn dịch vụ khỏi danh sách trong ViewModel
                    ListProduct.Remove(product);
                    TotalPrice -= (double)product.SalePrice * product.Quantity;

                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu việc xóa gặp lỗi
                    MessageBox.Show($"Error deleting service order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private async void LoadProducts()
        {
            var products = await _productHelper.GetAllProducts();
            foreach (var product in products)
            {
                Productlist.Add(product);
            }
        }

        // Properties
        private ObservableCollection<Product> productlist;
        public ObservableCollection<Product> Productlist
        {
            get { return productlist; }
            set
            {
                productlist = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> listProduct;
        public ObservableCollection<Product> ListProduct
        {
            get { return listProduct; }
            set
            {
                listProduct = value;
                OnPropertyChanged();
            }
        }

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged();
            }
        }

        private string billID;
        public string BillID
        {
            get { return billID; }
            set
            {
                billID = value;
                OnPropertyChanged();
            }
        }

        private string cusName;
        public string CusName
        {
            get { return cusName; }
            set
            {
                cusName = value;
                OnPropertyChanged();
            }
        }

        private string sdt;
        public string SDT
        {
            get { return sdt; }
            set
            {
                sdt = value;
                OnPropertyChanged();
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }

        private DateTime? dateOrder;
        public DateTime? DateOrder
        {
            get { return dateOrder; }
            set
            {
                dateOrder = value;
                OnPropertyChanged();
            }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }

        private double totalPrice;
        public double TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand AddProductCommand { get; set; }
        public ICommand AddOrderCommand { get; set; }

        // Methods
        private async Task AddProduct()
        {
            if (SelectedProduct != null && Quantity > 0)
            {
                var product = new Product
                {
                    PID = SelectedProduct.PID,
                    Name = SelectedProduct.Name,
                    Size = SelectedProduct.Size,
                    Quantity = Quantity,
                    SalePrice = SelectedProduct.SalePrice
                };

                ListProduct.Add(product);
                TotalPrice = (double)ListProduct.Sum(p => p.Quantity * p.SalePrice);

                // Reset quantity
                Quantity = 0;
                SelectedProduct= null;
            }
            else
            {
                MessageBox_Window.ShowDialog("Vui lòng nhập đầy đủ thông tin sản phẩm hợp lệ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
            }
        }

        private async Task AddOrder()
        {
            if (!string.IsNullOrEmpty(CusName) && !string.IsNullOrEmpty(SDT) && ListProduct.Any())
            {
                // Check if BillID already exists in Firebase
                var existingOrder = await _saleOrderHelper.GetSaleOrder(BillID);
                if (existingOrder != null)
                {
                    MessageBox_Window.ShowDialog("Mã hóa đơn đã tồn tại. Vui lòng nhập mã hóa đơn khác!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
                    return;
                }

                var saleOrder = new SaleOrder
                {
                    SaleId = BillID,
                    CustomerName = CusName,
                    CPhone = SDT,
                    CEmail = Email,
                    CAddress = Address,
                    DateSale = DateOrder.HasValue ? DateOrder.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd"),
                    TotalPrice = TotalPrice,
                    ListSaleProduct = ListProduct.ToList()
                };

                await _saleOrderHelper.AddSaleOrder(saleOrder);

                // Clear input fields after adding order
                BillID = string.Empty;
                CusName = string.Empty;
                SDT = string.Empty;
                Email = string.Empty;
                Address = string.Empty;
                DateOrder = DateTime.Now;
                ListProduct.Clear();
                TotalPrice = 0;

                  MessageBox_Window.ShowDialog("Thêm đơn hàng thành công!\n Bạn có muốn xuất hóa đơn?", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OkCancel);
                if (MessageBox_Window.buttonResultClicked == MessageBox_Window.ButtonResult.OK)
                {
                    // Nếu OK thì xuất hóa đơn
                }
            }
            else
            {
                MessageBox_Window.ShowDialog("Vui lòng nhập đầy đủ thông tin dịch vụ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
            }
        }
    }
}
