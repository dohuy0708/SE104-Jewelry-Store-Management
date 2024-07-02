using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;

namespace Jewelry_store_management.VIEWMODEL
{
    // hàm main
   
    public class ReviewPurchaseOrderViewModel :BaseViewModel
    {

        public ICommand ExportBillCommand { get; set; }

        private readonly PurchaseOrderHelper _purchaseOrderHelper;

        
        


        // date
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
                return totalprice;
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
        private String supplierName;
        public String SupplierName
        {
            get { return supplierName; }
            set
            {
                supplierName = value;
                OnPropertyChanged();
            }
        }

      
  

        // hàm chính
        public ReviewPurchaseOrderViewModel() { }
        public ReviewPurchaseOrderViewModel(PurchaseOrder purchaseOrder)
        {

            
            _purchaseOrderHelper = new PurchaseOrderHelper();

            ListPurChase = new ObservableCollection<Product>();

            ExportBillCommand = new RelayCommand<object>(ExportBill);

            // innitial
            if (purchaseOrder != null)
            {
                PurchaseID = purchaseOrder.PurchaseID;
                SupplierName = purchaseOrder.SupplierName;
                EntryDate = purchaseOrder.DatePurchase;
                foreach (var product in purchaseOrder.ListPurchaseProduct)
                {
                    ListPurChase.Add(product);
                }
                TotalPrice=(decimal)purchaseOrder.TotalPrice;
            }
        }


        // In hóa đơn 
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



    }



}
