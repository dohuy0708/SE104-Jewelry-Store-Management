using Jewelry_store_management.MODELS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace Jewelry_store_management.VIEWMODEL
{
    public class ReviewBillViewModel : BaseViewModel
    {

        private string _printContent;

        public ICommand ExportBillCommand { get; set; }
        private SaleOrder bill { get; set; }
        public SaleOrder Bill
        {
            get { return bill; }
            set
            {
                bill = value;
                OnPropertyChanged();
            }
        }
         
        // Properties
      
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

        private string dateOrder;
        public string DateOrder
        {
            get { return dateOrder; }
            set
            {
                dateOrder = value;
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




        // hàm chính 

        public ReviewBillViewModel()
        {
           
        }

        public ReviewBillViewModel(SaleOrder order)
        {
            
            
            GetSaleOrder();
           
            ExportBillCommand=new RelayCommand(_ => ExportBill());
            ListProduct = new ObservableCollection<Product>();

            if (order != null)
            {
                BillID = order.SaleId;
                CusName = order.CustomerName;
                SDT = order.CPhone;
                Email = order.CEmail;
                Address = order.CAddress;
                DateOrder = order.DateSale;
                TotalPrice = order.TotalPrice;
                foreach ( var orderitem in  order.ListSaleProduct)
                {
                    ListProduct.Add(orderitem);
                }    


            }
        }
        //Hàm xuất hóa đơn
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
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(240) }); // Tên sản phẩm
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(60) });  // Size
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(60) });  // Số lượng
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
                row.Cells.Add(new TableCell(new Paragraph(new Run(product.PurchasePrice.ToString("N0")))));
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
        //Hàm lấy bill
        public void GetSaleOrder()
        {

        }
        //Hàm lấy danh sách sản phẩm
       
    }
}
