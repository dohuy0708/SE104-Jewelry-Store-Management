using Google.Api;
using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
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
            ExportBillCommand = new RelayCommand(_ => ExportBill());

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
            info.Inlines.Add(new Run($"MÃ DỊCH VỤ: {SerID}\n"));
            info.Inlines.Add(new Run($"TÊN DỊCH VỤ: {ServiceNameType}\n"));
            info.Inlines.Add(new Run($"TÊN KHÁCH HÀNG: {CusName}\n"));
            info.Inlines.Add(new Run($"SỐ ĐIỆN THOẠI: {SDT}\n"));
            info.Inlines.Add(new Run($"EMALI: {Email}\n"));
            info.Inlines.Add(new Run($"ĐỊA CHỈ: {Address}\n"));
            info.Inlines.Add(new Run($"NGÀY LẬP: {InitialDate}\n"));
            info.Inlines.Add(new Run($"NGÀY GIAO: {DeliveryDate}\n"));
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
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(200) }); // Tên sản phẩm
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(100) });  // Số lượng
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(60) });  // Giá
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(260) }); // Mô tả

            TableRowGroup headerGroup = new TableRowGroup();
            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Tên sản phẩm"))) { FontWeight = FontWeights.Bold, FontSize = 14 });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Số lượng"))) { FontWeight = FontWeights.Bold, FontSize = 14 });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Giá"))) { FontWeight = FontWeights.Bold, FontSize = 14 });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Mô tả"))) { FontWeight = FontWeights.Bold, FontSize = 14 });
            headerGroup.Rows.Add(headerRow);
            productTable.RowGroups.Add(headerGroup);

            TableRowGroup bodyGroup = new TableRowGroup();
            foreach (var product in Productlist)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(product.Name))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(product.Quantity.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(product.PurchasePrice.ToString("N0")))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(product.Description.ToString()))));
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
