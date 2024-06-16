using Jewelry_store_management.MODELS;
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
    public class AddBillViewModel:BaseViewModel
    {
        public ICommand DeleteProductCommand { get; set; }
        public ICommand AddproductCommand { get; set; }
        private ObservableCollection<Product> getProductList;
        public ObservableCollection<Product> GetProductList
        {
            get { return getProductList; }
            set
            {
                getProductList = value;
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
        private ObservableCollection<Product> productList;
        public ObservableCollection<Product> ProductList
        {
            get { return productList; }
            set
            {
                productList = value;
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
        private double voucher;
        public double Voucher
        {
            get { return voucher; }
            set
            {
                voucher = value;
                OnPropertyChanged();
            }
        }
        private double cost;
        public double Cost
        {
            get { return cost; }
            set
            {
                cost = value;
                OnPropertyChanged();
            }
        }
        public AddBillViewModel()
        {
            GetProductList = new ObservableCollection<Product>();    
            ProductList = new ObservableCollection<Product>();
            TotalPrice = 0;
            Voucher = 0;
            Cost = 0;
            AddproductCommand=new RelayCommand(_=> AddProduct());
            DeleteProductCommand=new RelayCommand<Product>(DeleteProduct);
        }
        //Lấy List product từ DB
        public void GetProduct()
        {
            //GetProductList.Add
        }
        //Button thêm sản phẩm
        public void AddProduct()
        {
            ProductList.Add(new Product("nhẫn", "abc", 2599, "s", 2));
        }
        public void DeleteProduct(Product product)
        {
            if (product != null && ProductList.Contains(product))
                    ProductList.Remove(product);
        }
    }
}
