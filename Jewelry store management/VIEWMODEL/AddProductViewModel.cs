using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Jewelry_store_management.VIEWMODEL
{
    public class AddProductViewModel:BaseViewModel
    {
        public ICommand AddImageCommand { get; set; }
        public ICommand RemoveImageCommand { get; set; }

        // List hình ảnh
        private String image{ get; set; }
        public String Image
        {
            get { return image; }
            set 
            { 
                image = value;
                OnPropertyChanged();
            }
        }

        // List nhà cung cấp
        private ObservableCollection<String> supplierlist { get; set; }
        public ObservableCollection<String> Supplierlist
        {
            get { return supplierlist; }
            set
            {
                supplierlist = value;
                OnPropertyChanged();
            }
        }
        private String selectedSupplier;
        public String SelectedSupplier
        {
            get { return selectedSupplier; }
            set
            {
                selectedSupplier = value;
                OnPropertyChanged();
            }
        }
        private bool hasImage;
        public bool HasImage
        {
            get { return hasImage; }
            set
            {
                hasImage = value;
                OnPropertyChanged(nameof(HasImage));
            }
        }
        public AddProductViewModel()
        {
            Supplierlist = new ObservableCollection<string>();
            GetSupplierlist();
            AddImageCommand = new RelayCommand(_ => AddImage());
            RemoveImageCommand = new RelayCommand<object>(RemoveImage);
        }
        private void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (String filename in openFileDialog.FileNames)
                {
                    Image = filename;
                }
            }
        }
         private void RemoveImage(object obj)
        {
            BitmapImage image = obj as BitmapImage;
            if (image != null)
            {
                image = null;
                image.StreamSource?.Dispose();
            }
        }   
        // Hàm lấy list nhà cung cấp
        private void GetSupplierlist()
        {
            Supplierlist.Add("aaa");
            Supplierlist.Add("bbb");
            Supplierlist.Add("ccc");
            if (Image != null)
            {
                HasImage = true;
            }
            else {
                hasImage = false;
            }
        }
    }
}
