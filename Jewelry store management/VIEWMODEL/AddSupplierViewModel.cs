using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Jewelry_store_management.VIEWMODEL
{
    public class AddSupplierViewModel : BaseViewModel
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
        public ICommand AddSupCommand { get; set; }

        public AddSupplierViewModel()
        {
            _supplierHelper = new SupplierHelper();
            AddSupCommand = new RelayCommand(async _ => await AddSupClick());
        }

        // Hàm chức năng để thêm nhà cung cấp
        private async Task AddSupClick()
        {
            var existingSupplier = await _supplierHelper.GetSupplier(SupplierID);

            if (existingSupplier != null)
            {
                MessageBox_Window.ShowDialog("Mã nhà cung cấp đã tồn tại!", "Lỗi", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
                return;
            }

            var newSupplier = new Supplier
            {
                SID = SupplierID,
                Name = SupplierName,
                Phone = SupplierPhone,
                Address = SupplierAddress
            };

            await _supplierHelper.AddSupplier(newSupplier);

            MessageBox_Window.ShowDialog("Thêm nhà cung cấp thành công!", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OK);
            SupplierID = string.Empty;
            SupplierName = string.Empty;
            SupplierPhone = string.Empty;
            SupplierAddress = string.Empty;


            /*if (MessageBox_Window.buttonResultClicked == MessageBox_Window.ButtonResult.OK)
            {
                // tắt cửa sổ windoww hiện tại 
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.IsActive)
                    {
                        window.Close();
                        break;
                    }
                }
            }*/
        }
    }
}
