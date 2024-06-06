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
        private ObservableCollection<BitmapImage> imagelist { get; set; }
        public ObservableCollection<BitmapImage> Imagelist
        {
            get { return imagelist; }
            set 
            { 
                imagelist = value;
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
        public AddProductViewModel()
        {
            Supplierlist = new ObservableCollection<string>();
            GetSupplierlist();
            Imagelist = new ObservableCollection<BitmapImage>();
            AddImageCommand = new RelayCommand(_ => AddImage());
            RemoveImageCommand = new RelayCommand<object>(RemoveImage);
        }
        private void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(filename);
                    bitmap.EndInit();
                    Imagelist.Add(bitmap);
                }
            }
        }
         private void RemoveImage(object obj)
        {
            BitmapImage image = obj as BitmapImage;
            if (image != null && Imagelist.Contains(image))
            {
                Imagelist.Remove(obj as BitmapImage);
                image.StreamSource?.Dispose();
            }
        }   
        // Hàm lấy list nhà cung cấp
        private void GetSupplierlist()
        {
            Supplierlist.Add("aaa");
            Supplierlist.Add("bbb");
            Supplierlist.Add("ccc");

        }
    }
}
