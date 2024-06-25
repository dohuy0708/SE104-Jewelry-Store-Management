using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Jewelry_store_management.VIEWMODEL
{
    public class PurchaseOderViewModel:BaseViewModel
    {
        public ICommand AddProductCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand DeleteRowCommand { get; set; }

        private readonly SupplierHelper _supplierHelper;
        private readonly ProductHelper _productHelper;
        private readonly PurchaseOrderHelper _purchaseOrderHelper;

        // List nhà cung cấp
        private ObservableCollection<String> supplierlist { get; set; }
        public ObservableCollection<String> Supplierlist
        {
            get { return supplierlist; }
            set
            {
                supplierlist = value;
                OnPropertyChanged();
            }
        }

        private String selectedSupplier;
        public String SelectedSupplier
        {
            get { return selectedSupplier; }
            set
            {
                selectedSupplier = value;
                OnPropertyChanged();
            }
        }


     
        private ObservableCollection<String> sizelist { get; set; }
        public ObservableCollection<String> Sizelist
        {
            get { return sizelist; }
            set
            {
                sizelist = value;
                OnPropertyChanged();
            }
        }

        // List sản phẩm
        private ObservableCollection<Product> productlist { get; set; }
        public ObservableCollection<Product> Productlist
        {
            get { return productlist; }
            set
            {
                productlist = value;
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


        // date
        private DateTime? entryDate;
        public DateTime? EntryDate
        {
            get { return entryDate; }
            set
            {
                entryDate = value;
                OnPropertyChanged();
            }
        }



        //  Danh sách các sản phẩm nhập hàng 
        private ObservableCollection<Product> _listpurchase;
        public ObservableCollection<Product> ListPurChase
        {
            get { return _listpurchase; }
            set
            {
                _listpurchase = value;
                OnPropertyChanged(nameof(ListPurChase));
            }
        }


        // tính tổng Giá trị đơn nhập 
        public decimal totalprice;
        public decimal TotalPrice
        {
            get
            {
               return  totalprice;
            }
            set
            {
                totalprice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        // mã đơn mua 
        private String purchaseID;
        public String PurchaseID
        {
            get { return purchaseID; }
            set
            {
                purchaseID = value;
                OnPropertyChanged();
            }
        }

        // nhà cung cấp
        private String supplierID;
        public String SupplierID
        {
            get { return supplierID; }
            set
            {
                supplierID = value;
                OnPropertyChanged();
            }
        }

        // Ngày mua 
        public string date;
        public String Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        public string productName;
        public String ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                productName = value;
                OnPropertyChanged();
            }
        }

        public string productID;
        public String ProductID
        {
            get
            {
                return productID;
            }
            set
            {
                productID = value;
                OnPropertyChanged();
            }
        }
        public string size;
        public String Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                OnPropertyChanged();
            }
        }
        public int quantity;
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }
        public decimal purchaseprice;
        public decimal PurchasePrice
        {
            get
            {
                return purchaseprice;
            }
            set
            {
                purchaseprice = value;
                OnPropertyChanged();
            }
        }



        // hàm chính
        public PurchaseOderViewModel()
        {
            _supplierHelper = new SupplierHelper();
            _productHelper = new ProductHelper();
            _purchaseOrderHelper = new PurchaseOrderHelper();

            ListPurChase = new ObservableCollection<Product>();
            //
            Productlist = new ObservableCollection<Product>();
           Task tas= GetProductlist();
            Supplierlist = new ObservableCollection<string>();
            Task task = GetSupplierlist();
            EntryDate = DateTime.Now;
            //


            DeleteRowCommand = new RelayCommand<Product>(async product => await DeleteRow(product));
            AddProductCommand = new RelayCommand(async _ => await AddClick());
            ImportCommand = new RelayCommand(async _ => await ImportClick());
        }


        // hàm chức năng

        private async Task DeleteRow(Product product)
        {
            // Hiển thị thông báo xác nhận xóa


            // Nếu người dùng chọn OK để xác nhận xóa

            if (product != null)
            {
                try
                {
                    // Xóa đơn dịch vụ từ Firebase


                    // Xóa đơn dịch vụ khỏi danh sách trong ViewModel
                    ListPurChase.Remove(product);

                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu việc xóa gặp lỗi
                    MessageBox.Show($"Error deleting service order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private async Task ImportClick()
        {
            try
            {
                // Check if required fields are not null or empty
                if (!string.IsNullOrEmpty(PurchaseID) && !string.IsNullOrEmpty(SelectedSupplier) && ListPurChase.Any())
                {
                    // Create a new PurchaseOrder object
                    var newPurchaseOrder = new PurchaseOrder
                    {
                        PurchaseID = PurchaseID,
                        SupplierName = SelectedSupplier,
                        SID = SupplierID,
                        DatePurchase = EntryDate.HasValue ? EntryDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd"),
                        TotalPrice = (double)TotalPrice,
                        ListPurchaseProduct = ListPurChase.ToList()
                    };

                    // Add the purchase order to Firebase
                    await _purchaseOrderHelper.AddPurchaseOrder(newPurchaseOrder);

                    // Reset the fields after successful import
                    PurchaseID = string.Empty;
                    SupplierID = string.Empty;
                    SelectedSupplier = null;
                    ListPurChase.Clear();
                    TotalPrice = 0;

                    MessageBox_Window.ShowDialog("Nhập hàng thành công!", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OK);
                }
                else
                {
                    MessageBox_Window.ShowDialog("Vui lòng nhập đầy đủ thông tin đơn hàng!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox_Window.ShowDialog($"Đã có lỗi xảy ra: {ex.Message}", "Lỗi", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
            }
        }

        private async Task AddClick()
        {
            if (SelectedProduct != null &&   quantity > 0 && PurchasePrice > 0)
            {
                var newProduct = new Product
                {
                    PID = SelectedProduct.PID,
                    Name = SelectedProduct.Name,
                    Size = SelectedProduct.Size,
                    Quantity = quantity,
                    PurchasePrice = PurchasePrice
                };

                ListPurChase.Add(newProduct);
                OnPropertyChanged(nameof(ListPurChase));

                // Reset các trường nhập liệu sau khi thêm sản phẩm
                SelectedProduct = null;
                Quantity = 0;
                PurchasePrice = 0;

                decimal total = 0;
                foreach (var product in ListPurChase)
                {
                    total += product.PurchasePrice * product.Quantity;
                }
                TotalPrice = total;

            }
            else
            {
                MessageBox_Window.ShowDialog("Vui lòng nhập đầy đủ thông tin giá và số lượng hợp lệ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);

            }
        }
        private async Task GetSupplierlist()
        {

            try
            {
                var suppliers = await _supplierHelper.GetAllSuppliers();
                Supplierlist.Clear();
                foreach (var supplier in suppliers)
                { 
                    Supplierlist.Add(supplier.Name);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions

            }
        }
        private async Task GetProductlist()
        {   

            try
            {
                var Products = await _productHelper.GetAllProducts();
                Productlist.Clear();
                foreach (var product in Products)
                {

                    Productlist.Add(product);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions

            }
        }

    }
}
