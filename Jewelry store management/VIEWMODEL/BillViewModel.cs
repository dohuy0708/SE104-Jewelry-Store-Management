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
    public class BillViewModel:BaseViewModel
    {
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
        private string _printContent;

        public ICommand ExportBillCommand { get; set; }

        public BillViewModel()
        {
            Productlist = new ObservableCollection<Product>();
            List<Product> products = new List<Product>();
            //dòng này để text
            products.Add(new Product("abc", "aa", 256, "s", 25));
            Bill = new SaleOrder(products);
            GetSaleOrder();
            GetProductList();
            ExportBillCommand=new RelayCommand<object>(ExportBill);
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
        public void GetProductList()
        {
            foreach(var product in Bill.ListSaleProduct)
            {
                productlist.Add(product);
            }
        }
    }
}
