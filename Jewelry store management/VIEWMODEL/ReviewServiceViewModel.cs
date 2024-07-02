using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Jewelry_store_management.VIEWMODEL
{
    public class ReviewServiceViewModel:BaseViewModel
    {
        public ICommand AddProCommand { get; set; }
        public ICommand AddServiceOrderCommand { get; set; }
        public ICommand ExportBillCommand { get; set; }


        private readonly ServiceOrderHelper _serviceHelper;

        private readonly ProductHelper _productHelper;

        // Constructor

        // thêm constructor không có tham số chứ không sẽ bị lỗi
        public ReviewServiceViewModel()
        {

        }
        public ReviewServiceViewModel(ServiceOrder serviceOrder)
        {
            _serviceHelper = new ServiceOrderHelper();

            _productHelper = new ProductHelper();

            Productlist = new ObservableCollection<Product>();



            
            StatusType = new ObservableCollection<string>
            {
                "Hoàn Thành",
                "Chờ xử lý"
            };
            SelectedStatus = "Chờ xử lý";

             

            AddProCommand = new RelayCommand(async _ => await AddProduct());
            AddServiceOrderCommand = new RelayCommand(async _ => await AddServiceOrder());
            ExportBillCommand = new RelayCommand<object>(ExportBill);

            // initial field
            if (serviceOrder != null)
            {
                SerID = serviceOrder.ServiceID;
                CusName = serviceOrder.CustomerName;
                SDT = serviceOrder.CPhone;
                Email = serviceOrder.CEmail;
                Address = serviceOrder.CAddress;
                ServiceNameType = serviceOrder.ServiceName;
                SelectedStatus = serviceOrder.Status;
                InitialDate =  serviceOrder.DateOrder;
                DeliveryDate = serviceOrder.DateDelivery;
                TotalPrice = (decimal)serviceOrder.TotalPrice;

                // Assuming serviceOrder.Products is a collection of Product
                foreach (var product in serviceOrder.ListServiceProduct)
                {
                    Productlist.Add(product);
                }
            }
        }

        private async Task AddServiceOrder()
        {
            if (!string.IsNullOrEmpty(CusName) && !string.IsNullOrEmpty(SDT) && Productlist.Any())
            {
                var newServiceOrder = new ServiceOrder
                {
                    ServiceID =  SerID,
                    CustomerName = CusName,
                    CPhone = SDT,
                    CEmail = Email,
                    CAddress = Address,
                    DateOrder = InitialDate,
                    DateDelivery = DeliveryDate ,
                    ServiceName= ServiceNameType,
                    Status = SelectedStatus,
                    TotalPrice = (double)TotalPrice,
                    ListServiceProduct = Productlist.ToList()
                };

                // Add the service order to the database
                await _serviceHelper.UpdateServiceOrder(newServiceOrder);

               

                MessageBox_Window.ShowDialog("Cập nhật dịch vụ thành công!", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OK);
            }
            else
            {
                MessageBox_Window.ShowDialog("Vui lòng nhập đầy đủ thông tin dịch vụ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
            }
        }

        // Properties for data binding

        private string serviceNameType;
        public string ServiceNameType
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


        private string initialDate;
        public string InitialDate
        {
            get { return initialDate; }
            set
            {
                initialDate = value;
                OnPropertyChanged();
            }
        }

        private string deliveryDate;
        public string DeliveryDate
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

        public void ExportBill(object obj)
        {
            System.Windows.Controls.PrintDialog printdialog = new System.Windows.Controls.PrintDialog();
            if (printdialog.ShowDialog() == true)
            {
                if (obj is Visual visual)
                {
                    printdialog.PrintVisual(visual, "Invoice");
                }
            }
        }

        // Helper methods to get service types and status types


    }

}
