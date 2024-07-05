using Google.Protobuf.WellKnownTypes;
using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Jewelry_store_management.VIEWMODEL
{
    public class ServiceViewModel : BaseViewModel
    {
        public ICommand AddProCommand { get; set; }
        public ICommand AddServiceOrderCommand { get; set; }
        public ICommand DeleteRowCommand { get; set; }

        private readonly ServiceOrderHelper _serviceHelper;
     
        private readonly ProductHelper _productHelper;

        // Constructor
        public ServiceViewModel()
        {
            _serviceHelper = new ServiceOrderHelper();
           
            _productHelper = new ProductHelper();

            Productlist = new ObservableCollection<Product>();
         


            ServiceNameType = new ObservableCollection<string>
            {
                "Đánh bóng, làm sạch trang sức",
                "Gia công trang sức",
                "Mạ trang sức"


            };

            StatusType = new ObservableCollection<string>
            {
                "Hoàn Thành",
                "Chờ xử lý"
            };
            SelectedStatus = "Chờ xử lý";

            InitialDate = DateTime.Now;

            AddProCommand = new RelayCommand(async _ => await AddProduct());
            AddServiceOrderCommand = new RelayCommand(async _ => await AddServiceOrder());

            DeleteRowCommand = new RelayCommand<Product>(async product => await DeleteRow(product));

        }

        // Properties for data binding

        private ObservableCollection<string> serviceNameType;
        public ObservableCollection<string> ServiceNameType
        {
            get { return serviceNameType; }
            set
            {
                serviceNameType = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> statusType;
        public ObservableCollection<string> StatusType
        {
            get { return statusType; }
            set
            {
                statusType = value;
                OnPropertyChanged();
            }
        }

        private string selectedServiceName;
        public string SelectedServiceName
        {
            get { return selectedServiceName; }
            set
            {
                selectedServiceName = value;
                OnPropertyChanged();
            }
        }

        private string selectedStatus;
        public string SelectedStatus 
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> productList;
        public ObservableCollection<Product> Productlist
        {
            get { return productList; }
            set
            {
                productList = value;
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
      

        private DateTime? initialDate;
        public DateTime? InitialDate
        {
            get { return initialDate; }
            set
            {
                initialDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime? deliveryDate;
        public DateTime? DeliveryDate
        {
            get { return deliveryDate; }
            set
            {
                deliveryDate = value;
                OnPropertyChanged();
            }
        }
        private string serID;
        public string SerID
        {
            get { return serID; }
            set
            {
                serID = value;
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

        private string productName;
        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
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

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        private string describe;
        public string Decribe
        {
            get { return describe; }
            set
            {
                describe = value;
                OnPropertyChanged();
            }
        }

        private decimal totalPrice;
        public decimal TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                OnPropertyChanged();
            }
        }

        // Commands
        private async Task DeleteRow(Product product)
        { 

            if (product != null)
            {
                try
                {

                    // Xóa đơn dịch vụ khỏi danh sách trong ViewModel
                    Productlist.Remove(product);
                    TotalPrice -= product.SalePrice * product.Quantity;

                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu việc xóa gặp lỗi
                    MessageBox.Show($"Error deleting service order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
        private async Task AddProduct()
        {
            if (!string.IsNullOrEmpty(ProductName) && Quantity > 0 && Price > 0)
            {
                var newProduct = new Product
                {
                    Name = ProductName,
                    Quantity = Quantity,
                    SalePrice = Price,
                    Description = Decribe
                };

                Productlist.Add(newProduct);

                // Calculate total price
                TotalPrice += Price * Quantity;



                // Clear input fields after adding product
                ProductName = string.Empty;
                Quantity = 0;
                Price = 0;
                Decribe = string.Empty;
            }
            else
            {
                MessageBox_Window.ShowDialog("Vui lòng nhập đầy đủ thông tin sản phẩm hợp lệ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
            }
        }

        private async Task AddServiceOrder()
        {
            if (!string.IsNullOrEmpty(CusName) && !string.IsNullOrEmpty(SDT) && Productlist.Any())
            {
                if (InitialDate <= DeliveryDate)
                {
                    var newServiceOrder = new ServiceOrder
                    {
                        ServiceID = SerID,
                        CustomerName = CusName,
                        CPhone = SDT,
                        CEmail = Email,
                        CAddress = Address,
                        DateOrder = InitialDate.HasValue ? InitialDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd"),
                        DateDelivery = DeliveryDate.HasValue ? DeliveryDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"),
                        ServiceName = SelectedServiceName,
                        Status = SelectedStatus,
                        TotalPrice = (double)TotalPrice,
                        ListServiceProduct = Productlist.ToList()
                    };

                    // Add the service order to the database
                    await _serviceHelper.AddServiceOrder(newServiceOrder);

                    // Reset fields after successful addition
                    CusName = string.Empty;
                    SDT = string.Empty;
                    Email = string.Empty;
                    Address = string.Empty;
                    InitialDate = DateTime.Now;
                    DeliveryDate = DateTime.Now.AddDays(7);
                    SelectedServiceName = null;
                    SelectedStatus = null;
                    Productlist.Clear();
                    TotalPrice = 0;

                    MessageBox_Window.ShowDialog("Thêm dịch vụ thành công!", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OK);
                }
                else
                {
                    MessageBox_Window.ShowDialog("Ngày giao hàng phải sau ngày tạo đơn!", "Chú ý", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox_Window.ShowDialog("Vui lòng nhập đầy đủ thông tin dịch vụ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
            }
        }

        // Helper methods to get service types and status types


    }
}
