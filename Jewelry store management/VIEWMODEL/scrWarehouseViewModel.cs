using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;

namespace Jewelry_store_management.VIEWMODEL
{
    public class scrWarehouseViewModel :BaseViewModel
    {

        // khai báo command
        public ICommand SearchCommand { get; set; }

        public ICommand AddCommand { get; set; }


        // khai báo trường dữ liệu
        private ObservableCollection<Product> productEntries;

        public ObservableCollection<Product> ProductEntries
        {
            get { return productEntries; }
            set
            {
                productEntries = value;
                OnPropertyChanged();
            }
        }

     

        public scrWarehouseViewModel()
        {
            // Khởi tạo danh sách pro
            ProductEntries = new ObservableCollection<Product>();


            AddCommand = new RelayCommand(async _ => await AddClick());
            SearchCommand = new RelayCommand(Search);
        }

        private async Task AddClick()
        {
            var addProView = new AddProductView
            {
                DataContext = new AddProductViewModel()
            };
            addProView.ShowDialog();
        }

        private void Search(object parameter)
        {
            // Thực hiện logic tìm kiếm ở đây
        }
      
    }
}
