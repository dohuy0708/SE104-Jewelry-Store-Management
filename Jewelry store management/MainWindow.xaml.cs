using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FireSharp.Interfaces;
using FireSharp.Response;
using Jewelry_store_management.VIEW;
using Jewelry_store_management.VIEWMODEL;

//using FireSharp.Config;
//using FireSharp.Interfaces;
//using FireSharp.Response;

namespace Jewelry_store_management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int scr = 1;
        Color color4 = (Color)ColorConverter.ConvertFromString("SkyBlue");
        Color color5 = (Color)ColorConverter.ConvertFromString("AliceBlue");




        public MainWindow()
        {

            
            InitializeComponent();
           // DataContext = new MainWindowViewModel();
      
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        //// di chuyển màn hình
        //private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //        DragMove();
        //}
        //// move chuột vào nút màn hình Home 
        //private void menu_Home_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    menu_Home.Background = new SolidColorBrush(color4);
        //}
        //// clik nút màn hình Home
        //private void menu_Home_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    scr = 1;
        //   // DataContext = new scrHome();
        //    menu_Home.Background = new SolidColorBrush(color4);
        //    menu_Order.Background = new SolidColorBrush(color5);
        //    menu_Service.Background = new SolidColorBrush(color5);
        //    menu_Warehouse.Background = new SolidColorBrush(color5);
        //    menu_Addpro.Background = new SolidColorBrush(color5);
        //    menu_Supplier.Background = new SolidColorBrush(color5);
        //    menu_Account.Background = new SolidColorBrush(color5);
        //}
        //// move chuột ra khỏi nút màn hình Home
        //private void menu_Home_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    if (scr != 1)
        //        menu_Home.Background = new SolidColorBrush(color5);
        //}


        //// move chuột vào nút màn hình Order
        //private void menu_Order_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    menu_Order.Background = new SolidColorBrush(color4);
        //}
        //// clik nút màn hình Order
        //private void menu_Order_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    scr = 2;
        //   // scrOrder scrorder = new scrOrder();
        //   // DataContext = scrorder;
        //    menu_Home.Background = new SolidColorBrush(color5);
        //    menu_Order.Background = new SolidColorBrush(color4);
        //    menu_Service.Background = new SolidColorBrush(color5);
        //    menu_Warehouse.Background = new SolidColorBrush(color5);
        //    menu_Addpro.Background = new SolidColorBrush(color5);
        //    menu_Supplier.Background = new SolidColorBrush(color5);
        //    menu_Account.Background = new SolidColorBrush(color5);
        //}
        //// move chuột ra khỏi nút màn hình Order
        //private void menu_Order_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    if (scr != 2)
        //        menu_Order.Background = new SolidColorBrush(color5);
        //}



        //// move chuột vào nút màn hình Service
        //private void menu_Service_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    menu_Service.Background = new SolidColorBrush(color4);
        //}
        //// clik nút màn hình Service
        //private void menu_Service_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    scr = 3;
        //   // scrService scrservice = new scrService();
        //   // DataContext = scrservice;
        //    menu_Home.Background = new SolidColorBrush(color5);
        //    menu_Order.Background = new SolidColorBrush(color5);
        //    menu_Service.Background = new SolidColorBrush(color4);
        //    menu_Warehouse.Background = new SolidColorBrush(color5);
        //    menu_Addpro.Background = new SolidColorBrush(color5);
        //    menu_Supplier.Background = new SolidColorBrush(color5);
        //    menu_Account.Background = new SolidColorBrush(color5);
        //}
        //// move chuột ra khỏi nút màn hình Service
        //private void menu_Service_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    if (scr != 3)
        //        menu_Service.Background = new SolidColorBrush(color5);
        //}

        //// move chuột vào nút màn hình Warehouse
        //private void menu_Warehouse_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    menu_Warehouse.Background = new SolidColorBrush(color4);
        //}
        //// clik nút màn hình Warehouse
        //private void menu_Warehouse_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    scr = 4;
        //   // scrWarehouse scrwarehouse = new scrWarehouse();
        //  //  DataContext = scrwarehouse;
        //    menu_Home.Background = new SolidColorBrush(color5);
        //    menu_Order.Background = new SolidColorBrush(color5);
        //    menu_Service.Background = new SolidColorBrush(color5);
        //    menu_Warehouse.Background = new SolidColorBrush(color4);
        //    menu_Addpro.Background = new SolidColorBrush(color5);
        //    menu_Supplier.Background = new SolidColorBrush(color5);
        //    menu_Account.Background = new SolidColorBrush(color5);
        //}
        //// move chuột ra khỏi nút màn hình Warehouse
        //private void menu_Warehouse_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    if (scr != 4)
        //        menu_Warehouse.Background = new SolidColorBrush(color5);
        //}


        //// move chuột vào nút màn hình Addpro
        //private void menu_Addpro_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    menu_Addpro.Background = new SolidColorBrush(color4);
        //}
        //// clik nút màn hình Addpro
        //private void menu_Addpro_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    scr = 5;
        //    //scrAddpro scraddpro = new scrAddpro();
        //    //DataContext = scraddpro;
        //    menu_Home.Background = new SolidColorBrush(color5);
        //    menu_Order.Background = new SolidColorBrush(color5);
        //    menu_Service.Background = new SolidColorBrush(color5);
        //    menu_Warehouse.Background = new SolidColorBrush(color5);
        //    menu_Addpro.Background = new SolidColorBrush(color4);
        //    menu_Supplier.Background = new SolidColorBrush(color5);
        //    menu_Account.Background = new SolidColorBrush(color5);
        //}
        //// move chuột ra khỏi nút màn hình Addpro
        //private void menu_Addpro_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    if (scr != 5)
        //        menu_Addpro.Background = new SolidColorBrush(color5);
        //}


        //// move chuột vào nút màn hình Supplier
        //private void menu_Supplier_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    menu_Supplier.Background = new SolidColorBrush(color4);
        //}
        //// clik nút màn hình Supplier
        //private void menu_Supplier_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    scr = 6;
        //    //scrSupplier scrsupplier = new scrSupplier();
        //    //DataContext = scrsupplier;
        //    menu_Home.Background = new SolidColorBrush(color5);
        //    menu_Order.Background = new SolidColorBrush(color5);
        //    menu_Service.Background = new SolidColorBrush(color5);
        //    menu_Warehouse.Background = new SolidColorBrush(color5);
        //    menu_Addpro.Background = new SolidColorBrush(color5);
        //    menu_Supplier.Background = new SolidColorBrush(color4);
        //    menu_Account.Background = new SolidColorBrush(color5);
        //}
        //// move chuột ra khỏi nút màn hình Supplier
        //private void menu_Supplier_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    if (scr != 6)
        //        menu_Supplier.Background = new SolidColorBrush(color5);
        //}



        //// move chuột vào nút màn hình Account
        //private void menu_Account_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    menu_Account.Background = new SolidColorBrush(color4);
        //}
        //// clik nút màn hình Account
        //private void menu_Account_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    scr = 7;
        //    //scrAccount scraccount = new scrAccount();
        //    //DataContext = scraccount;
        //    menu_Home.Background = new SolidColorBrush(color5);
        //    menu_Order.Background = new SolidColorBrush(color5);
        //    menu_Service.Background = new SolidColorBrush(color5);
        //    menu_Warehouse.Background = new SolidColorBrush(color5);
        //    menu_Addpro.Background = new SolidColorBrush(color5);
        //    menu_Supplier.Background = new SolidColorBrush(color5);
        //    menu_Account.Background = new SolidColorBrush(color4);
        //}
        //// move chuột ra khỏi nút màn hìnhAccount
        //private void menu_Account_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    if (scr != 7)
        //        menu_Account.Background = new SolidColorBrush(color5);
        //}

    }
}
