using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Jewelry_store_management.VIEWMODEL
{
    public class ReviewPurchaseOrderViewModel : BaseViewModel
    {
        public ICommand ExportBillCommand { get; set; }

        private readonly PurchaseOrderHelper _purchaseOrderHelper;

        private string entryDate;
        public string EntryDate
        {
            get { return entryDate; }
            set
            {
                entryDate = value;
                OnPropertyChanged();
            }
        }

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

        public decimal totalprice;
        public decimal TotalPrice
        {
            get { return totalprice; }
            set
            {
                totalprice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private string purchaseID;
        public string PurchaseID
        {
            get { return purchaseID; }
            set
            {
                purchaseID = value;
                OnPropertyChanged();
            }
        }

        private string supplierName;
        public string SupplierName
        {
            get { return supplierName; }
            set
            {
                supplierName = value;
                OnPropertyChanged();
            }
        }

        public ReviewPurchaseOrderViewModel() { }

        public ReviewPurchaseOrderViewModel(PurchaseOrder purchaseOrder)
        {
            _purchaseOrderHelper = new PurchaseOrderHelper();
            ListPurChase = new ObservableCollection<Product>();
            ExportBillCommand = new RelayCommand(_ =>ExportBill());

            if (purchaseOrder != null)
            {
                PurchaseID = purchaseOrder.PurchaseID;
                SupplierName = purchaseOrder.SupplierName;
                EntryDate = purchaseOrder.DatePurchase;
                foreach (var product in purchaseOrder.ListPurchaseProduct)
                {
                    ListPurChase.Add(product);
                }
                TotalPrice = (decimal)purchaseOrder.TotalPrice;
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

            Paragraph header = new Paragraph(new Run("CHI TIẾT ĐƠN NHẬP HÀNG"));
            header.FontSize = 36;
            header.FontWeight = FontWeights.Bold;
            header.TextAlignment = TextAlignment.Center;
            doc.Blocks.Add(header);

            Paragraph info = new Paragraph();
            info.Inlines.Add(new Run($"MÃ ĐƠN HÀNG: {PurchaseID}\n"));
            info.Inlines.Add(new Run($"NHÀ CUNG CẤP: {SupplierName}\n"));
            info.Inlines.Add(new Run($"NGÀY NHẬP: {EntryDate}\n"));
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
            productTable.Columns.Add(new TableColumn() { Width = new GridLength(100) }); // Mã sản phẩm
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
            foreach (var product in ListPurChase)
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
    }
}
