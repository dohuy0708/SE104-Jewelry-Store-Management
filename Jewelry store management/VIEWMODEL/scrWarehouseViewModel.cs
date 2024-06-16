using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Jewelry_store_management.MODELS;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Jewelry_store_management.VIEWMODEL
{
    public class scrWarehouseViewModel : BaseViewModel
    {
        private ObservableCollection<Product> productEntries;
        public ObservableCollection<Product> ProductEntries
        {
            get { return productEntries; }
            set
            {
                productEntries = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<String> CategoryList;
        public ObservableCollection<String> TypeList;
        public ObservableCollection<String> MaterialList;
        public ObservableCollection<Filter> CategoryFilterList;
        public ICollectionView CategoryFilteredItems { get; set; }


        public ICommand SearchCommand { get; set; }

        public scrWarehouseViewModel()
        {
            // Khởi tạo danh sách pro
            ProductEntries = new ObservableCollection<Product>
        {
            new Product { PID = "SP11", Name = "Dây chuyền lục bảo", Number=200, ImageURL="/Drawble/Images/Logo.png" },
            new Product { PID = "SP66", Name = "Ngọc đại pháp sư", Number=100, ImageURL="/Drawble/Images/Logo.png" },
            new Product { PID = "SP33", Name = "Nhẫn máu", Number=300, ImageURL="/Drawble/Images/Logo.png" },
            new Product { PID = "SP34", Name = "Nhẫn máu cam", Number=400, ImageURL="/Drawble/Images/Logo.png" },
            new Product { PID = "SP35", Name = "Nhẫn máu xanh", Number=600, ImageURL="/Drawble/Images/Logo.png" },
            new Product { PID = "SP36", Name = "Nhẫn máu cam xanh", Number=500, ImageURL="/Drawble/Images/Logo.png" },
            new Product { PID = "SP37", Name = "Nhẫn máu blue", Number=700, ImageURL="/Drawble/Images/Logo.png" },
            new Product { PID = "SP38", Name = "Nhẫn máu orange", Number=900, ImageURL="/Drawble/Images/Logo.png" },
            new Product { PID = "SP39", Name = "Nhẫn máu blue orange", Number=800, ImageURL="/Drawble/Images/Logo.png" },
            new Product { PID = "SP23", Name = "Nhẫn máu orange blue", Number=1100, ImageURL="/Drawble/Images/Logo.png" },
            new Product { PID = "SP13", Name = "Nhẫn máu xanh cam", Number=1000, ImageURL="/Drawble/Images/Logo.png" },
            //new Supplier { SID = "NCC2", Name = "Cty TNHH TV2", Address="TP. Hà Nội" },
            //new Supplier { SID = "NCC3", Name = "Cty H-Jewelry", Address="Bình Dương" },
            // Thêm các mục khác nếu cần thiết
        };

            // Khởi tạo lệnh tìm kiếm
            SearchCommand = new RelayCommand(Search);
            //Khởi tạo danh sách category
           CategoryList = new ObservableCollection<string> { "Vàng", "Trang Sức" };
            CategoryFilterList = new ObservableCollection<Filter>(CategoryList.Select(c => new Filter { Name = c }));
            CategoryFilteredItems = CollectionViewSource.GetDefaultView(ProductEntries);
            CategoryFilteredItems.Filter = FilterItems;
        }
        private bool FilterItems(object item)
        {
            if (item is Product currentItem)
            {
                return CategoryFilterList.Any(f => f.IsSelected && f.Name == currentItem.Category);
            }
            return false;
        }

        private void Search(object parameter)
        {
            // Thực hiện logic tìm kiếm ở đây
        }

        public class Filter: BaseViewModel
        {
            private bool isSelected;
            public string Name { get; set; }

            public bool IsSelected
            {
                get => isSelected;
                set
                {
                    if (isSelected != value)
                    {
                        isSelected = value;
                        OnPropertyChanged(nameof(IsSelected));
                    }
                }
            }
        }
    }
}
