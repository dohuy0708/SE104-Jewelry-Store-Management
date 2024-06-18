using Jewelry_store_management.MODELS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jewelry_store_management.VIEWMODEL
{
    public class PurchaseOderViewModel:BaseViewModel
    {
        private ObservableCollection<Supplier>suppliertlist { get; set; }
        public ObservableCollection<Supplier> Suppliertlist
        {
            get { return suppliertlist; }
            set
            {
                suppliertlist = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<String> getproductlist { get; set; }
        public ObservableCollection<String> GetProductlist
        {
            get { return getproductlist; }
            set
            {
                getproductlist = value;
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
        private ObservableCollection<Product> productlist { get; set; }
        public ObservableCollection<Product> Productlist
        {
            get { return productlist; }
            set
            {
                productlist = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        public int TotalPrice
        {
            get { return GetTotalPrice(); }
        }
        public ICommand AddProductCommand { get; set; }
        public PurchaseOderViewModel()
        {
            Suppliertlist = new ObservableCollection<Supplier>();
            Getsupplier();
            GetProductlist = new ObservableCollection<string>();
            GetProduct();          
            Sizelist = new ObservableCollection<string>();
            GetSize();
            Productlist = new ObservableCollection<Product>();
            AddProductCommand = new RelayCommand(_ => AddProduct());
        }
        public void AddProduct()
        {
            Productlist.Add(new Product("Nhẫn", "abx", 45000000, "M", 5));
            
        }
        public void Getsupplier()
        {
            Suppliertlist.Add(new Supplier("nxx", "byz", "0793384989", "45 Hoàng Diệu"));
        }
        public void GetSize()
        {
            Sizelist.Add("M");
        }
        public void GetProduct()
        {
            GetProductlist.Add("Nhẫn");

        }
        public int GetTotalPrice()
        {
            return 800000;
        }
    }
}
