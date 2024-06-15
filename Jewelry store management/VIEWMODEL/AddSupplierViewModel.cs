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
    public class AddSupplierViewModel: BaseViewModel
    {
        public ICommand AddProductCommand { get; set; }
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
        public AddSupplierViewModel()
        {
            productlist = new ObservableCollection<Product>();
            AddProductCommand=new RelayCommand(_ => Addproduct());
        }
        private void Addproduct()
        {
            Productlist.Add(new Product("Nhẫn bạc", "22456", 999999, "s", 56));
        }

    }
}
