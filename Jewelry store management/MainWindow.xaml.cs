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
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Jewelry_store_management.VIEW;

namespace Jewelry_store_management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret="ny46fjmoYlYZTyuW2P2M51BfrQDb5zibqXo2MNqC",
            BasePath="https://test-382ab-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient firebaseClient = null;

        public MainWindow()
        {

            TestView test= new TestView();
            test.Show();
            /*InitializeComponent();
            firebaseClient = new FireSharp.FirebaseClient(config);
            if (firebaseClient != null)
            {
                MessageBox.Show("Connect successed");
            }*/

        }
    }
}
