using FireSharp.EventStreaming;
using Google.Api.Gax.ResourceNames;
using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
 
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Jewelry_store_management.VIEWMODEL
{
    public class AddProductViewModel:BaseViewModel
    {

        // khai báo helper
        private readonly SupplierHelper _supplierHelper;
        private readonly ProductHelper _productHelper;

        // khai báo command
        public ICommand AddImageCommand { get; set; }
 
        public ICommand AddProCommand { get; set; }


        // danh mục sản phẩm
        private ObservableCollection<string> productTypes;
        public ObservableCollection<string> ProductTypes
        {
            get { return productTypes; }
            set
            {
                productTypes = value;
                OnPropertyChanged();
            }
        }
        private String selectedType;
        public String SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged();
            }
        }

        // danh mục chất liệu
        private ObservableCollection<string> materialTypes;
        public ObservableCollection<string> MaterialTypes
        {
            get { return materialTypes; }
            set
            {
                materialTypes = value;
                OnPropertyChanged();
            }
        }
        private String selectedMaterial;
        public String SelectedMaterial
        {
            get { return selectedMaterial; }
            set
            {
                selectedMaterial = value;
                OnPropertyChanged();
            }
        }

        // List hình ảnh
        private BitmapImage image{ get; set; }
        public BitmapImage Image
        {
            get { return image; }
            set 
            { 
                image = value;
                OnPropertyChanged();
            }
        }

      
        

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
        private bool hasImage;
        public bool HasImage
        {
            get { return hasImage; }
            set
            {
                hasImage = value;
                OnPropertyChanged(nameof(HasImage));
            }
        }


        // các thuộc tính của sản phẩm 
        // các thuộc tính sản phẩm để binding
        private string productID;
        public string ProductID
        {
            get { return productID; }
            set
            {
                productID = value;
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

        private string productSize;
        public string ProductSize
        {
            get { return productSize; }
            set
            {
                productSize = value;
                OnPropertyChanged();
            }
        }

        private int productQuantity;
        public int ProductQuantity
        {
            get { return productQuantity; }
            set
            {
                productQuantity = value;
                OnPropertyChanged();
            }
        }

        private decimal purchasePrice;
        public decimal PurchasePrice
        {
            get { return purchasePrice; }
            set
            {
                purchasePrice = value;
                OnPropertyChanged();
            }
        }

        private decimal salePrice;
        public decimal SalePrice
        {
            get { return salePrice; }
            set
            {
                salePrice = value;
                OnPropertyChanged();
            }
        }

        private string materialType;
        public string MaterialType
        {
            get { return materialType; }
            set
            {
                materialType = value;
                OnPropertyChanged();
            }
        }

        private double  productWeight;
        public double ProductWeight
        {
            get { return productWeight; }
            set
            {
                productWeight = value;
                OnPropertyChanged();
            }
        }

        private string productDescription;
        public string ProductDescription
        {
            get { return productDescription; }
            set
            {
                productDescription = value;
                OnPropertyChanged();
            }
        }


        // hàm chinh
        public AddProductViewModel()
        {
            _supplierHelper = new SupplierHelper();
            _productHelper = new ProductHelper();
            //

            Supplierlist = new ObservableCollection<string>();
            Task task = GetSupplierlist();
            //

            AddImageCommand = new RelayCommand(_ => AddImage());
        
            AddProCommand = new RelayCommand (async _ => await AddProClick());

            //
            ProductTypes = new ObservableCollection<string>
            {
                "Nhẫn",
                "Bông tai",
                "Dây chuyền",
                "Vòng tay",
                "Thần tài"
            };

            MaterialTypes = new ObservableCollection<string>
            {
                "Vàng 9999",
                "Vàng 24k",
                "Vàng Trắng",
                "Vàng Tây",
                "Vàng Ý",
                "Bạc"
            };
        }

        private async Task AddProClick()
        {
            try
            {
                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrEmpty(ProductID) ||
                        string.IsNullOrEmpty(ProductName) ||
                       string.IsNullOrEmpty(SelectedSupplier) ||
                       string.IsNullOrEmpty(ProductSize) ||
                      string.IsNullOrEmpty(ProductQuantity.ToString()) ||
                      string.IsNullOrEmpty(PurchasePrice.ToString()) ||
                      string.IsNullOrEmpty(SalePrice.ToString()) ||
                      string.IsNullOrEmpty(ProductWeight.ToString()) ||
                      string.IsNullOrEmpty(ProductDescription) ||
                      string.IsNullOrEmpty(SelectedType) ||
                      string.IsNullOrEmpty(SelectedMaterial))
                {
                    
                    MessageBox_Window.ShowDialog("Vui lòng nhập đầy đủ thông tin! ", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);

                    return;
                }

                // Kiểm tra xem ProductID đã tồn tại chưa
                var existingProduct = await _productHelper.GetProduct(ProductID);
                if (existingProduct != null)
                {
                    MessageBox_Window.ShowDialog("Mã sản phẩm đã tồn tại trong hệ thống!", "Lỗi", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
                    return;
                }

                // Kiểm tra giá bán phải cao hơn giá nhập 
                if ( SalePrice!=null && PurchasePrice!= null && SalePrice <= PurchasePrice)
                {
                    MessageBox_Window.ShowDialog("Giá bán phải cao hơn giá nhập!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
                    return;
                }

                // Upload image if exists
                string imagestring = null;

                if (Image!=null)
                {
                    imagestring = ConvertBitmapImageToBase64(Image);
                    //  MessageBox.Show(Image+": = ");
                    
                }

                // Tạo đối tượng Product từ các thông tin nhập vào
                Product newProduct = new Product
                {
                    PID = ProductID,
                    Name = ProductName,
                    Size = ProductSize,
                    Quantity = ProductQuantity,
                    PurchasePrice = PurchasePrice,
                    SalePrice = SalePrice,
                   
                    Weight = ProductWeight,
                    Description = ProductDescription,
                    SupplierName = SelectedSupplier,
                    Type = SelectedType,
                    Material = SelectedMaterial,
                    ImageURL = imagestring // Assign the image URL here
                };

                // Gọi phương thức AddProduct của ProductHelper để thêm sản phẩm vào Firebase
               await _productHelper.AddProduct(newProduct);

                MessageBox_Window.ShowDialog("Thêm sản phẩm thành công!", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OK);

                // Xóa dữ liệu sau khi thêm thành công (nếu cần)
               // ClearFields();
            }
            catch (Exception ex)
            {
               MessageBox.Show($"Lỗi khi thêm sản phẩm: {ex.Message}");
            }
        }

        private void ClearFields()
        {
            ProductID = "";
            ProductName = "";
            ProductSize = "";
            ProductQuantity = 0;
            PurchasePrice = 0;
            SalePrice = 0;
            MaterialType = "";
            ProductWeight = 0;
            ProductDescription = "";
            Image = null;
        }


        // hàm chức năng
        private void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName, UriKind.Absolute);
                bitmap.EndInit();

                Image = bitmap;
                HasImage = true;
            }

             


        }
        
        // Hàm lấy list nhà cung cấp
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

        // chuyển từ bitmap sang String để lưu lên DB
        private string ConvertBitmapImageToBase64(BitmapImage bitmapImage)
        {

           
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chuyển đổi hình ảnh: {ex.Message}");
                return null;
            }
        }
       

    }

}
