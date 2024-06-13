using Jewelry_store_management.GUI;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace Jewelry_store_management.VIEWMODEL
{
    public class MainWindowViewModel : BaseViewModel
    {

        // khai báo
        public ICommand MenuHomeCommand { get; set; }
        public ICommand MenuOrderCommand { get; set; }
        public ICommand MenuServiceCommand { get; set; }
        public ICommand MenuWarehouseCommand { get; set; }
        public ICommand MenuAddproCommand { get; set; }
        public ICommand MenuSupplierCommand { get; set; }
        public ICommand MenuAccountCommand { get; set; }
 
        public scrHome scrHome;
        public scrAccount scrAccount;
        public scrAddpro scrAddpro;
        public scrOrder scrOrder;
        public scrService scrService;
        public scrSupplier scrSupplier;
        public scrWarehouse scrWarehouse;


        private int _scr =1;

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
            set { 
                _scr = value;
               
                OnPropertyChanged(); }
        }

        // khai báo currentView 
        private object _currentScr;
        public object CurrentScr
        {
            get { return _currentScr; }
            set { _currentScr = value; OnPropertyChanged(); }
        }


        // hàm update contentView
        private void UpdateView()

        {
             
            switch (Scr)
            {
                case 1:
                    CurrentScr= scrHome; break;
                case 2:
                    CurrentScr = scrOrder; break;
                case 3:
                    CurrentScr =scrService; break;
                case 4:
                    CurrentScr = scrWarehouse; break;
                case 5:
                    CurrentScr = scrAddpro; break;
                case 6:
                    CurrentScr = scrSupplier; break;
                case 7:
                    CurrentScr = scrAccount; break;
                default:
                    break;
            }
        }


        // tô màu 
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

     

        public MainWindowViewModel()
        {
            scrWarehouse = new scrWarehouse();
            scrSupplier = new scrSupplier();    
            scrAccount = new scrAccount();  
            scrHome = new scrHome();
            scrAddpro = new scrAddpro();
            scrOrder = new scrOrder();
            scrService = new scrService();
            UpdateView();


            MenuHomeCommand = new RelayCommand(async _ => await MenuHomeClick());
            MenuOrderCommand = new RelayCommand(async _ => await MenuOrderClick());
            MenuServiceCommand = new RelayCommand(async _ => await MenuServiceClick());
            MenuWarehouseCommand = new RelayCommand(async _ => await MenuWarehouseClick());
            MenuAddproCommand = new RelayCommand(async _ => await MenuAddproClick());
            MenuSupplierCommand = new RelayCommand(async _ => await MenuSupplierClick());
            MenuAccountCommand = new RelayCommand(async _ => await MenuAccountClick());

            MenuHomeBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuOrderBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuServiceBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuWarehouseBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuAddproBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuSupplierBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));
            MenuAccountBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("AliceBlue"));




        }



        private async Task MenuHomeClick()
        {
            MessageBox.Show("SCr1");
            Scr = 1;
            UpdateView();
            SetMenuBackgrounds(MenuHomeBackground);
        }

        private async Task  MenuOrderClick()
        {
            Scr = 2;
            UpdateView() ;
            SetMenuBackgrounds(MenuOrderBackground);
        }

        private async Task  MenuServiceClick()
        {
            Scr = 3;
            UpdateView();
            SetMenuBackgrounds(MenuServiceBackground);
        }

        private async Task MenuWarehouseClick()
        {
            Scr = 4;
            UpdateView();
            SetMenuBackgrounds(MenuWarehouseBackground);
        }

        private async Task MenuAddproClick()
        {
            Scr = 5;
            UpdateView();
            SetMenuBackgrounds(MenuAddproBackground);
        }

        private async Task MenuSupplierClick()
        {
            Scr = 6;
            UpdateView();
            SetMenuBackgrounds(MenuSupplierBackground);
        }

        private async Task MenuAccountClick()
        {
            Scr = 7;
            UpdateView();
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



       
       
    }
}
