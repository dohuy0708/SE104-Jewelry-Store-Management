using Jewelry_store_management.VIEWMODEL;
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

namespace Jewelry_store_management.GUI
{
    /// <summary>
    /// Interaction logic for scrSupplier.xaml
    /// </summary>
    public partial class scrSupplier : UserControl
    {
        public scrSupplier()
        {
            InitializeComponent();
            DataContext = new scrSupplierViewModel();
        }
    }
}
