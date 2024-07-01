using Jewelry_store_management.MODELS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
           
            ExportBillCommand=new RelayCommand<object>(ExportBill);
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
        //Hàm lấy bill
        public void GetSaleOrder()
        {

        }
        //Hàm lấy danh sách sản phẩm
       
    }
}
