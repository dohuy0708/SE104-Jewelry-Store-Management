﻿using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Jewelry_store_management.VIEWMODEL
{
    public class scrSupplierViewModel : BaseViewModel
    {
        // khai báo fields
        public ICommand SearchCommand { get; set; }
        public ICommand AddSupCommand { get; set; }
        public ICommand ShowDetailCommand { get; set; }
        public ICommand DeleteRowCommand { get; set; }

        private ObservableCollection<Supplier> supplierEntries;
        private readonly SupplierHelper _supplierHelper;

        public ObservableCollection<Supplier> SupplierEntries
        {
            get { return supplierEntries; }
            set
            {
                supplierEntries = value;
                OnPropertyChanged();
            }
        }
        private ServiceOrder selectedSupplier;
        public ServiceOrder SelectedSupplier
        {
            get { return selectedSupplier; }
            set
            {
                selectedSupplier = value;
                OnPropertyChanged();

            }
        }

        // hàm chính
        public scrSupplierViewModel()
        {
            // Khởi tạo SupplierHelper
            _supplierHelper = new SupplierHelper();

            // Khởi tạo danh sách ncc
            SupplierEntries = new ObservableCollection<Supplier>();

            // Thêm các mục khác nếu cần thiết

            // Khởi tạo lệnh tìm kiếm
            SearchCommand = new RelayCommand(Search);
            AddSupCommand = new RelayCommand(async _ => await AddSupClick());
            DeleteRowCommand = new RelayCommand<Supplier>(async supplier => await DeleteRow(supplier));
            ShowDetailCommand = new RelayCommand<Supplier>(ShowDetail);
            // Load suppliers
            LoadSuppliers();
        }


        private async Task DeleteRow(Supplier supplier)
        {
            // Hiển thị thông báo xác nhận xóa
            MessageBox_Window.ShowDialog("Bạn chắc chắn xóa nhà cung cấp!", "Chú ý", "\\Drawable\\Icons\\icon_attention.png", MessageBox_Window.MessageBoxButton.OkCancel);

            // Nếu người dùng chọn OK để xác nhận xóa
            if (MessageBox_Window.buttonResultClicked == MessageBox_Window.ButtonResult.OK)
            {
                if (supplier!= null)
                {
                    try
                    {
                        // Xóa đơn dịch vụ từ Firebase
                        await _supplierHelper.DeleteSupplier(supplier.SID);

                        // Xóa đơn dịch vụ khỏi danh sách trong ViewModel
                        SupplierEntries.Remove(supplier);

                        // Hiển thị thông báo thành công
                        MessageBox_Window.ShowDialog("Xóa nhà cung cấp thành công!", "Thành công", "\\Drawable\\Icons\\icon_success.png", MessageBox_Window.MessageBoxButton.OK);
                    }
                    catch (Exception ex)
                    {
                        // Hiển thị thông báo lỗi nếu việc xóa gặp lỗi
                        MessageBox.Show($"Error deleting service order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // Hiển thị thông báo lỗi nếu `serviceOrder` là null
                    MessageBox.Show("Error: Service order is null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }



        private void ShowDetail(Supplier supplier)
        {
            SupplierEntries.Clear();

            if (supplier != null)
            {
                var addSupView = new ReviewAddSupplier
                {
                    DataContext = new ReviewAddSupplierViewModel(supplier)
                };
                addSupView.ShowDialog();
                selectedSupplier= null;
            }
            else
            {
                MessageBox_Window.ShowDialog("Service order is null!", "Error", "\\Drawable\\Icons\\icon_error.png", MessageBox_Window.MessageBoxButton.OK);
            }

            LoadSuppliers();
        }

        private async Task AddSupClick()
        {
            var addSupView = new AddSuppierView()
            {
                DataContext = new AddSupplierViewModel()
            };
            addSupView.ShowDialog();

            // Reload suppliers after adding a new one
            await LoadSuppliers();
        }

        private async Task LoadSuppliers()
        {
            try
            {
                var suppliers = await _supplierHelper.GetAllSuppliers();
                SupplierEntries.Clear();
                foreach (var supplier in suppliers)
                {
                    SupplierEntries.Add(supplier);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
               
            }
        }

        // hàm chức năng
        private void Search(object parameter)
        {
            // Thực hiện logic tìm kiếm ở đây
        }
    }
}
