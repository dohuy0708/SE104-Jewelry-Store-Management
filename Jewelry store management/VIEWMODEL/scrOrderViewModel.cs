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
    public class scrOrderViewModel : BaseViewModel
    {

        private ObservableCollection<SaleOrder> orderEntries;

        public ObservableCollection<SaleOrder> OrderEntries
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
            OrderEntries = new ObservableCollection<SaleOrder>
        {
            new SaleOrder { SaleId = "D1", CustomerName = "Nguyễn Văn A1", DateSale = "01/01/2024", statusImage = "/Drawable/Icons/icon_success.png", Cost=3000000 },
            new SaleOrder { SaleId = "D2", CustomerName = "Nguyễn Văn A2", DateSale = "02/01/2024", statusImage = "/Drawable/Icons/icon_success.png", Cost=30000000 },
            new SaleOrder { SaleId = "D3", CustomerName = "Nguyễn Văn A3", DateSale = "06/01/2023", statusImage = "/Drawable/Icons/icon_success.png", Cost=20000000 },
            // Thêm các mục khác nếu cần thiết
        };

            // Khởi tạo lệnh tìm kiếm
            SearchCommand = new RelayCommand(Search);
        }

        private void Search(object parameter)
        {
            // Thực hiện logic tìm kiếm ở đây
        }
       
    }
}
