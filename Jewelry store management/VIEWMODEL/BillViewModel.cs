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
using System.Windows.Documents;

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
            if (SelectedProduct != null && SelectedProduct.Quantity - Quantity>=0 && Quantity > 0)
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


                  MessageBox_Window.ShowDialog("Thêm đơn hàng thành công!\n Bạn có muốn xuất hóa đơn?", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OkCancel);
                if (MessageBox_Window.buttonResultClicked == MessageBox_Window.ButtonResult.OK)
                {
                    ExportBill();
                }

                // giảm số lượng sản phẩm đã mua xuống 
                foreach (var product in ListProduct)
                {
                    // Decrease Quantity of each product in Firebase or perform necessary actions
                    // Example:
                    await _productHelper.DecreaseProductQuantity(product.PID, product.Quantity);
                }

                // Clear input fields after adding order
                BillID = string.Empty;
                CusName = string.Empty;
                SDT = string.Empty;
                Email = string.Empty;
                Address = string.Empty;
                DateOrder = DateTime.Now;
                ListProduct.Clear();
                TotalPrice = 0;
            }
            else
            {
                MessageBox_Window.ShowDialog("Vui lòng nhập đầy đủ thông tin dịch vụ!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OK);
            }
        }
        public void ExportBill()
        {
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument doc = CreateFlowDocument();
                IDocumentPaginatorSource idpSource = doc;
                printDialog.PrintDocument(idpSource.DocumentPaginator, "Purchase Order");
            }
        }

        private FlowDocument CreateFlowDocument()
        {
            FlowDocument doc = new FlowDocument();
            doc.PagePadding = new Thickness(50);
            doc.ColumnWidth = double.PositiveInfinity;

            Paragraph header = new Paragraph(new Run("CHI TIẾT HÓA ĐƠN"));
            header.FontSize = 36;
            header.FontWeight = FontWeights.Bold;
            header.TextAlignment = TextAlignment.Center;
            doc.Blocks.Add(header);

            Paragraph info = new Paragraph();
            info.Inlines.Add(new Run($"MÃ HÓA ĐƠN: {BillID}\n"));
            info.Inlines.Add(new Run($"NGÀY LẬP: {DateOrder}\n"));
            info.Inlines.Add(new Run($"TÊN KHÁCH HÀNG: {CusName}\n"));
            info.Inlines.Add(new Run($"SỐ ĐIỆN THOẠI: {SDT}\n"));
            info.Inlines.Add(new Run($"EMALI: {Email}\n"));
            info.Inlines.Add(new Run($"ĐỊA CHỈ: {Address}\n"));
            info.FontSize = 14;
            doc.Blocks.Add(info);

            Paragraph productHeader = new Paragraph(new Run("THÔNG TIN SẢN PHẨM:"));
            productHeader.FontSize = 16;
            productHeader.FontWeight = FontWeights.DemiBold;
            doc.Blocks.Add(productHeader);

            Table productTable = new Table();
            productTable.CellSpacing = 20;
            productTable.BorderBrush = Brushes.Black;
            productTable.BorderThickness = new Thickness(1);
            doc.Blocks.Add(productTable);
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(120) }); // Mã sản phẩm
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(200) }); // Tên sản phẩm
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(60) });  // Size
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(100) });  // Số lượng
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(120) }); // Giá

            TableRowGroup headerGroup = new TableRowGroup();
            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Mã sản phẩm"))) { FontWeight = FontWeights.Bold, FontSize = 14 });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Tên sản phẩm"))) { FontWeight = FontWeights.Bold, FontSize = 14 });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Size"))) { FontWeight = FontWeights.Bold, FontSize = 14 });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Số lượng"))) { FontWeight = FontWeights.Bold, FontSize = 14 });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Giá"))) { FontWeight = FontWeights.Bold, FontSize = 14 });
            headerGroup.Rows.Add(headerRow);
            productTable.RowGroups.Add(headerGroup);

            TableRowGroup bodyGroup = new TableRowGroup();
            foreach (var product in ListProduct)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(product.PID))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(product.Name))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(product.Size))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(product.Quantity.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(product.SalePrice.ToString("N0")))));
                bodyGroup.Rows.Add(row);
            }
            productTable.RowGroups.Add(bodyGroup);

            Paragraph totalPrice = new Paragraph(new Run($"Tổng giá trị (VND): {TotalPrice.ToString("N0")}"));
            totalPrice.FontSize = 16;
            totalPrice.FontWeight = FontWeights.DemiBold;
            totalPrice.Margin = new Thickness(30, 0, 10, 0);
            doc.Blocks.Add(totalPrice);

            return doc;
        }
    }
}
