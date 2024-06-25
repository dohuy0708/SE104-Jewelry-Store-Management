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
        public ICommand PurchaseProCommand { get; set; }
        private readonly PurchaseOrderHelper _purchaseOrderHelper;

        private ObservableCollection<PurchaseOrder> addproEntries;

        public ObservableCollection<PurchaseOrder> AddproEntries
        {
            get { return addproEntries; }
            set
            {
                addproEntries = value;
                OnPropertyChanged(nameof(AddproEntries));
            }
        }

      

        public scrAddproViewModel()
        {
            _purchaseOrderHelper = new PurchaseOrderHelper();
            // Khởi tạo danh sách nhap
            AddproEntries = new ObservableCollection<PurchaseOrder>();
       

            // Khởi tạo lệnh tìm kiếm

            SearchCommand = new RelayCommand(Search);
            PurchaseProCommand = new RelayCommand(async _ => await AddProClick());

             LoadPurchase();
        }


        private async Task LoadPurchase()
        {
            try
            {
                // Fetch all purchase orders from Firebase
                var purchaseOrders = await _purchaseOrderHelper.GetAllPurchaseOrders();

                // Clear existing entries
                AddproEntries.Clear();

                // Add fetched purchase orders to the collection
                foreach (var purchaseOrder in purchaseOrders)
                {
                    var addProEntry = new PurchaseOrder()
                    {
                        // Map properties from PurchaseOrder to AddPro as needed
                        // Assuming AddPro has similar properties to PurchaseOrder
                        PurchaseID = purchaseOrder.PurchaseID,
                        SupplierName = purchaseOrder.SupplierName,
                        DatePurchase = purchaseOrder.DatePurchase,
                        TotalPrice = purchaseOrder.TotalPrice
                    };
                    AddproEntries.Add(addProEntry);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase orders: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AddProClick()
        {
            

            var PurchaseProView = new PurchaseOrderView
            {
                DataContext = new PurchaseOderViewModel()
            };
           PurchaseProView.ShowDialog();

            LoadPurchase();

        }

        private void Search(object parameter)
        {
            // Thực hiện logic tìm kiếm ở đây
        }   

       
    }
}
