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
    public class BillViewModel:BaseViewModel
    {
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

        public BillViewModel()
        {
            productlist = new ObservableCollection<Product>();
            AddProductCommand = new RelayCommand(_ => AddProduct());
        }
        public void AddProduct()
        {
            Productlist.Add(new Product("Nhẫn", "abx", 45000000, "M", 5));

        }
        public int GetTotalPrice()
        {
            return 800000;
        }
    }
}
