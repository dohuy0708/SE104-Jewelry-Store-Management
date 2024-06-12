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
using Jewelry_store_management.VIEWMODEL;


namespace Jewelry_store_management.GUI
{
    /// <summary>
    /// Interaction logic for scrOrder.xaml
    /// </summary>
    public partial class scrOrder : UserControl
    {
        public scrOrder()
        {
            InitializeComponent();
            DataContext = new scrOrderViewModel();
        }
    }
}
