using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jewelry_store_management.VIEWMODEL
{
    public class scrSupplierViewModel : BaseViewModel
    {
        // khai báo fields
        public ICommand SearchCommand { get; set; }
        public ICommand AddSupCommand { get; set; }

        private ObservableCollection<Supplier> supplierEntries;
        private readonly SupplierHelper _supplierHelper;

        public ObservableCollection<Supplier> SupplierEntries
        {
            get { return supplierEntries; }
            set
            {
                supplierEntries = value;
                OnPropertyChanged();
            }
        }

        // hàm chính
        public scrSupplierViewModel()
        {
            // Khởi tạo SupplierHelper
            _supplierHelper = new SupplierHelper();

            // Khởi tạo danh sách ncc
            SupplierEntries = new ObservableCollection<Supplier>();

            // Thêm các mục khác nếu cần thiết

            // Khởi tạo lệnh tìm kiếm
            SearchCommand = new RelayCommand(Search);
            AddSupCommand = new RelayCommand(async _ => await AddSupClick());

            // Load suppliers
            LoadSuppliers();
        }

        private async Task AddSupClick()
        {
            var addSupView = new AddSuppierView()
            {
                DataContext = new AddSupplierViewModel()
            };
            addSupView.ShowDialog();

            // Reload suppliers after adding a new one
            await LoadSuppliers();
        }

        private async Task LoadSuppliers()
        {
            try
            {
                var suppliers = await _supplierHelper.GetAllSuppliers();
                SupplierEntries.Clear();
                foreach (var supplier in suppliers)
                {
                    SupplierEntries.Add(supplier);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
               
            }
        }

        // hàm chức năng
        private void Search(object parameter)
        {
            // Thực hiện logic tìm kiếm ở đây
        }
    }
}
