using Jewelry_store_management.MODELS;
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
    public class scrSupplierViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<Supplier> supplierEntries;

        public ObservableCollection<Supplier> SupplierEntries
        {
            get { return supplierEntries; }
            set
            {
                supplierEntries = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; set; }

        public scrSupplierViewModel()
        {
            // Khởi tạo danh sách ncc
            SupplierEntries = new ObservableCollection<Supplier>
        {
            new Supplier { SID = "NCC1", Name = "Cty TNHH TV1", Address="TP. HCM" },
            new Supplier { SID = "NCC2", Name = "Cty TNHH TV2", Address="TP. Hà Nội" },
            new Supplier { SID = "NCC3", Name = "Cty H-Jewelry", Address="Bình Dương" },
            // Thêm các mục khác nếu cần thiết
        };

            // Khởi tạo lệnh tìm kiếm
            SearchCommand = new RelayCommand(Search);
        }

        private void Search(object parameter)
        {
            // Thực hiện logic tìm kiếm ở đây
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
