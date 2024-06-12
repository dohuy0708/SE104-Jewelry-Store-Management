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
    public class scrAddproViewModel:INotifyPropertyChanged
    {

        public Supplier s1=new Supplier("Cty TNHH 123", "NCC4", "03333", "TP. HCM");
        public Supplier s2=new Supplier("Cty TNHH 123", "NCC4", "03333", "TP. HCM");
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

        public ICommand SearchCommand { get; set; }

        public scrAddproViewModel()
        {
            // Khởi tạo danh sách nhap
            AddproEntries = new ObservableCollection<AddPro>
        {
            new AddPro(s1, "SP5", "Nhẫn cà rá", "07/01/2024", 300, "Thành công", "/Drawable/Icons/icon_success.png"),
            new AddPro(s1, "SP6", "Nhẫn kim cang", "09/01/2024", 100, "Thành công", "/Drawable/Icons/icon_success.png"),
            new AddPro(s2, "SP7", "Nhẫn giả", "14/03/2024", 500, "Thành công", "/Drawable/Icons/icon_success.png")
            //new AddPro { SID = "NCC2", Name = "Cty TNHH TV2", Address="TP. Hà Nội" },
            //new AddPro { SID = "NCC3", Name = "Cty H-Jewelry", Address="Bình Dương" },
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
