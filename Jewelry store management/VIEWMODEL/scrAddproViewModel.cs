using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

 
 

namespace Jewelry_store_management.VIEWMODEL
{
    public class scrAddproViewModel:BaseViewModel
    {
        public ICommand SearchCommand { get; set; }
        public ICommand AddProCommand { get; set; }

       
        private ObservableCollection<AddPro> addproEntries;

        public ObservableCollection<AddPro> AddproEntries
        {
            get { return addproEntries; }
            set
            {
                addproEntries = value;
                OnPropertyChanged();
            }
        }

      

        public scrAddproViewModel()
        {
            // Khởi tạo danh sách nhap
            AddproEntries = new ObservableCollection<AddPro>();
       

            // Khởi tạo lệnh tìm kiếm

            SearchCommand = new RelayCommand(Search);
            AddProCommand = new RelayCommand(async _ => await AddProClick());
        }




        private async Task AddProClick()
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
