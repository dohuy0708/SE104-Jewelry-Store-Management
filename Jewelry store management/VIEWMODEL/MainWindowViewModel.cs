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
        public scrAccountViewModel scrAccount;
        public scrAddpro scrAddpro;
        public scrOrder scrOrder;
        public scrService scrService;
        public scrSupplier scrSupplier;
        public scrWarehouse scrWarehouse;


        private int _scr =1;

        private Brush _menuHomeBackground;
        private Brush _menuOrderBackground;
        private Brush _menuServiceBackground = Brushes.Transparent;
        private Brush _menuWarehouseBackground = Brushes.Transparent;
        private Brush _menuAddproBackground = Brushes.Transparent;
        private Brush _menuSupplierBackground = Brushes.Transparent;
        private Brush _menuAccountBackground = Brushes.Transparent;

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


        
     

        public MainWindowViewModel()
        {
            scrWarehouse = new scrWarehouse();
            scrSupplier = new scrSupplier();    
            scrAccount = new scrAccountViewModel();  
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

           




        }

        private void ResetMenuBackgrounds()
        {
            MenuHomeBackground = Brushes.Transparent;
            MenuOrderBackground = Brushes.Transparent;
            MenuServiceBackground = Brushes.Transparent;
            MenuWarehouseBackground = Brushes.Transparent;
            MenuAddproBackground = Brushes.Transparent;
            MenuSupplierBackground = Brushes.Transparent;
            MenuAccountBackground = Brushes.Transparent;
        }

        private async Task MenuHomeClick()
        {
           
            Scr = 1;
            UpdateView();
            ResetMenuBackgrounds();
            MenuHomeBackground = Brushes.SkyBlue ;
        }

        private async Task  MenuOrderClick()
        {
            ResetMenuBackgrounds();
            MenuOrderBackground = Brushes.SkyBlue;
            Scr = 2;
            UpdateView() ;
            
        }

        private async Task  MenuServiceClick()
        {
            ResetMenuBackgrounds();
            MenuServiceBackground = Brushes.SkyBlue;
            Scr = 3;
            UpdateView();
            
        }

        private async Task MenuWarehouseClick()
        {
            ResetMenuBackgrounds();
            MenuWarehouseBackground = Brushes.SkyBlue;
            Scr = 4;
            UpdateView();
           
        }

        private async Task MenuAddproClick()
        {
            ResetMenuBackgrounds();
            MenuAddproBackground = Brushes.SkyBlue;
            Scr = 5;
            UpdateView();
            
        }

        private async Task MenuSupplierClick()
        {
            ResetMenuBackgrounds();
            MenuSupplierBackground = Brushes.SkyBlue;
            Scr = 6;
            UpdateView();
            
        }

        private async Task MenuAccountClick()
        {
            ResetMenuBackgrounds();
            MenuAccountBackground = Brushes.SkyBlue;
            Scr = 7;
            UpdateView();
             
        }


 



    }
}
