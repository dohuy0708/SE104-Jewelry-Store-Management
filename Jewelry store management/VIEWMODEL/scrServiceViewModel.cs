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
    public class scrServiceViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<ServiceOrder> serviceEntries;

        public ObservableCollection<ServiceOrder> ServiceEntries
        {
            get { return serviceEntries; }
            set
            {
                serviceEntries = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; set; }

        public scrServiceViewModel()
        {
            // Khởi tạo danh sách đơn hàng
            ServiceEntries = new ObservableCollection<ServiceOrder>
        {
            new ServiceOrder { ServiceID = "DV1", CustomerName = "Nguyễn Văn A1", DateOrder = "01/01/2024", statusImage = "/Drawable/Icons/icon_success.png", Cost=5000000 },
            new ServiceOrder { ServiceID = "DV2", CustomerName = "Nguyễn Văn A2", DateOrder = "09/01/2024", statusImage = "/Drawable/Icons/icon_success.png", Cost=3000000 },
            new ServiceOrder { ServiceID = "DV3", CustomerName = "Nguyễn Văn A3", DateOrder = "06/01/2023", statusImage = "/Drawable/Icons/icon_success.png", Cost=9000000 },
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
