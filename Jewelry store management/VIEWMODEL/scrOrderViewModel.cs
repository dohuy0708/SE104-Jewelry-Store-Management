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

namespace Jewelry_store_management.VIEWMODEL
{
    public class scrOrderViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Order> orderEntries;

        public ObservableCollection<Order> OrderEntries
        {
            get { return orderEntries; }
            set
            {
                orderEntries = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; set; }

        public scrOrderViewModel()
        {
            // Khởi tạo danh sách đơn hàng
            OrderEntries = new ObservableCollection<Order>
        {
            new Order { OrderID = "D1", CustomerName = "Nguyễn Văn A1", OrderDate = "01/01/2024", StatusImage = "/Drawable/Icons/icon_success.png" },
            new Order { OrderID = "D2", CustomerName = "Nguyễn Văn A2", OrderDate = "02/01/2024", StatusImage = "/Drawable/Icons/icon_success.png" },
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
