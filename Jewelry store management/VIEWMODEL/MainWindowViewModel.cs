using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Jewelry_store_management.VIEWMODEL
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private int _scr;
        private Brush _menuHomeBackground;
        private Brush _menuOrderBackground;
        private Brush _menuServiceBackground;
        private Brush _menuWarehouseBackground;
        private Brush _menuAddproBackground;
        private Brush _menuSupplierBackground;
        private Brush _menuAccountBackground;

        public int Scr
        {
            get { return _scr; }
            set { _scr = value; OnPropertyChanged(); }
        }

        public Brush MenuHomeBackground
        {
            get { return _menuHomeBackground; }
            set { _menuHomeBackground = value; OnPropertyChanged(); }
        }

        public Brush MenuOrderBackground
        {
            get { return _menuOrderBackground; }
            set { _menuOrderBackground = value; OnPropertyChanged(); }
        }

        public Brush MenuServiceBackground
        {
            get { return _menuServiceBackground; }
            set { _menuServiceBackground = value; OnPropertyChanged(); }
        }

        public Brush MenuWarehouseBackground
        {
            get { return _menuWarehouseBackground; }
            set { _menuWarehouseBackground = value; OnPropertyChanged(); }
        }

        public Brush MenuAddproBackground
        {
            get { return _menuAddproBackground; }
            set { _menuAddproBackground = value; OnPropertyChanged(); }
        }

        public Brush MenuSupplierBackground
        {
            get { return _menuSupplierBackground; }
            set { _menuSupplierBackground = value; OnPropertyChanged(); }
        }

        public Brush MenuAccountBackground
        {
            get { return _menuAccountBackground; }
            set { _menuAccountBackground = value; OnPropertyChanged(); }
        }

        public ICommand MenuHomeCommand { get; set; }
        public ICommand MenuOrderCommand { get; set; }
        public ICommand MenuServiceCommand { get; set; }
        public ICommand MenuWarehouseCommand { get; set; }
        public ICommand MenuAddproCommand { get; set; }
        public ICommand MenuSupplierCommand { get; set; }
        public ICommand MenuAccountCommand { get; set; }


        public MainWindowViewModel()
        {
            MenuHomeCommand = new RelayCommand(_ => MenuHomeClick());
            MenuOrderCommand = new RelayCommand(_ => MenuOrderClick());
            MenuServiceCommand = new RelayCommand(_ => MenuServiceClick());
            MenuWarehouseCommand = new RelayCommand(_ => MenuWarehouseClick());
            MenuAddproCommand = new RelayCommand(_ => MenuAddproClick());
            MenuSupplierCommand = new RelayCommand(_ => MenuSupplierClick());
            MenuAccountCommand = new RelayCommand(_ => MenuAccountClick());

            MenuHomeBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuOrderBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuServiceBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuWarehouseBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuAddproBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuSupplierBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuAccountBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
        }



        private void MenuHomeClick()
        {
            Scr = 1;
            SetMenuBackgrounds(MenuHomeBackground);
        }

        private void MenuOrderClick()
        {
            Scr = 2;
            SetMenuBackgrounds(MenuOrderBackground);
        }

        private void MenuServiceClick()
        {
            Scr = 3;
            SetMenuBackgrounds(MenuServiceBackground);
        }

        private void MenuWarehouseClick()
        {
            Scr = 4;
            SetMenuBackgrounds(MenuWarehouseBackground);
        }

        private void MenuAddproClick()
        {
            Scr = 5;
            SetMenuBackgrounds(MenuAddproBackground);
        }

        private void MenuSupplierClick()
        {
            Scr = 6;
            SetMenuBackgrounds(MenuSupplierBackground);
        }

        private void MenuAccountClick()
        {
            Scr = 7;
            SetMenuBackgrounds(MenuAccountBackground);
        }



        private void SetMenuBackgrounds(Brush activeBrush)
        {
            MenuHomeBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuOrderBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuServiceBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuWarehouseBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuAddproBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuSupplierBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuAccountBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));

            if (activeBrush == MenuHomeBackground) MenuHomeBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("SkyBlue"));
            if (activeBrush == MenuOrderBackground) MenuOrderBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("SkyBlue"));
            if (activeBrush == MenuServiceBackground) MenuServiceBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("SkyBlue"));
            if (activeBrush == MenuWarehouseBackground) MenuWarehouseBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("SkyBlue"));
            if (activeBrush == MenuAddproBackground) MenuAddproBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("SkyBlue"));
            if (activeBrush == MenuSupplierBackground) MenuSupplierBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("SkyBlue"));
            if (activeBrush == MenuAccountBackground) MenuAccountBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("SkyBlue"));
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string Name=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}
