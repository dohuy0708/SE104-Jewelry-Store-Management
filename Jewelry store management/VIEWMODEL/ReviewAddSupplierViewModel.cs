using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace Jewelry_store_management.VIEWMODEL
{
    public class ReviewAddSupplierViewModel:BaseViewModel
    {
        private string _supplierID;
        private string _supplierName;
        private string _supplierPhone;
        private string _supplierAddress;

        private readonly SupplierHelper _supplierHelper;

        // Các thuộc tính để liên kết với TextBox
        public string SupplierID
        {
            get => _supplierID;
            set
            {
                _supplierID = value;
                OnPropertyChanged();
            }
        }

        public string SupplierName
        {
            get => _supplierName;
            set
            {
                _supplierName = value;
                OnPropertyChanged();
            }
        }

        public string SupplierPhone
        {
            get => _supplierPhone;
            set
            {
                _supplierPhone = value;
                OnPropertyChanged();
            }
        }

        public string SupplierAddress
        {
            get => _supplierAddress;
            set
            {
                _supplierAddress = value;
                OnPropertyChanged();
            }
        }

        // Khai báo command
        public ICommand UpdateSupCommand { get; set; }

        public ReviewAddSupplierViewModel()
        {
             
        }
        public ReviewAddSupplierViewModel(Supplier supplier)
        {
            _supplierHelper = new SupplierHelper();
            UpdateSupCommand = new RelayCommand(async _ => await UpdateSupClick());


            // initial 
            if (supplier != null)
            {

                SupplierID= supplier.SID;
                SupplierName = supplier.Name;
                SupplierPhone = supplier.Phone;
                SupplierAddress = supplier.Address;
            }

        }

        // Hàm chức năng để thêm nhà cung cấp
        private async Task UpdateSupClick()
        {
            
            var newSupplier = new Supplier
            {
                SID = SupplierID,
                Name = SupplierName,
                Phone = SupplierPhone,
                Address = SupplierAddress
            };

            await _supplierHelper.UpdateSupplier(newSupplier);

            MessageBox_Window.ShowDialog("Cập nhật nhà cung cấp thành công!", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OK);
            


             
        }
    }

}
