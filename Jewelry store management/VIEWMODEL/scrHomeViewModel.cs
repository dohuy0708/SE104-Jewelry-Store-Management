using OxyPlot;
 
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using LiveCharts.Wpf;
using LiveCharts;
using Jewelry_store_management.VIEW;
using LiveCharts.Defaults;
using Jewelry_store_management.HELPER;


namespace Jewelry_store_management.VIEWMODEL
{
    public class scrHomeViewModel: BaseViewModel
    {


        private readonly SaleOrderHelper _saleOrderHelper;
        private readonly ServiceOrderHelper _serviceOrderHelper;

        public ICommand AddOrderCommand { get; set; }
        public ICommand AddServiceOrderCommand { get; set; }
        public ICommand AddProductCommand { get; set; } 
        public ICommand ViewGoldPriceCommand { get; set; }

        public ICommand StatisticCommand { get; set; }


        private SeriesCollection _salesSeries;
        public SeriesCollection SalesSeries
        {
            get => _salesSeries;
            set
            {
                _salesSeries = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<double> _monthlySales;
        public ObservableCollection<double> MonthlySales
        {
            get { return _monthlySales; }
            set
            {
                _monthlySales = value;
                OnPropertyChanged();
            }
        }


        private List<string> _month;
        public List<string> Months
        {
            get => _month;
            set
            {
                _month = value;
                OnPropertyChanged();
            }
        }


        private SeriesCollection _pieSeries;
        public SeriesCollection PieSeries
        {
            get => _pieSeries;
            set
            {
                _pieSeries = value;
                OnPropertyChanged();
            }
        }

        private Func<ChartPoint, string> _amountService;
        public Func<ChartPoint, string> AmountService
        {
            get => _amountService;
            set
            {
                _amountService = value;
                OnPropertyChanged();
            }
        }

        private Func<ChartPoint, string> _amountProduct;
        public Func<ChartPoint, string> AmountProduct
        {
            get => _amountProduct;
            set
            {
                _amountProduct = value;
                OnPropertyChanged();
            }
        }
 

        private ObservableValue _serviceValue;
        public ObservableValue ServiceValue
        {
            get => _serviceValue;
            set
            {
                _serviceValue = value;
                OnPropertyChanged();
            }
        }

        private ObservableValue _productValue;
        public ObservableValue ProductValue
        {
            get => _productValue;
            set
            {
                _productValue = value;
                OnPropertyChanged();
            }
        }

        private DateTime? date;
        public DateTime? Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        public scrHomeViewModel()
        {
            // khởi tạo helper
            _saleOrderHelper = new SaleOrderHelper();
            _serviceOrderHelper = new ServiceOrderHelper();

            // khai báo command
            AddOrderCommand = new RelayCommand(async _ => await AddOrderClick());
            AddProductCommand = new RelayCommand(async _ => await AddProClick());
            AddServiceOrderCommand = new RelayCommand(async _ => await AddServiceOrderClick());
            ViewGoldPriceCommand = new RelayCommand(async param => await ViewGoldPriceClick(param));
            StatisticCommand = new RelayCommand(async _ => await StatisticClick());

            MonthlySales = new ObservableCollection<double> { 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            CalculateMonthlySales();
            // khởi tạo line chart
            SalesSeries = new SeriesCollection
            {
               new ColumnSeries
               {
                   Title = "Doanh thu",
                   Values = new ChartValues<double>(MonthlySales),
                   MaxColumnWidth = 25
               }
            };


            Months = new List<string> { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
           
            



            ServiceValue = new ObservableValue(1);
            ProductValue = new ObservableValue(1);

            // khởi tạo PieChart
            PieSeries = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Dịch vụ",
                    Values = new ChartValues<ObservableValue> { ServiceValue  },
                    DataLabels = true,
                    LabelPoint = chartPoint => $"{chartPoint.Y} ({chartPoint.Participation:P})",
                     Fill = System.Windows.Media.Brushes.CadetBlue // Màu đỏ cho "Dịch vụ"
                },
                new PieSeries
                {
                    Title = "Sản phẩm",
                    Values = new ChartValues<ObservableValue> {  ProductValue},
                    DataLabels = true,
                    LabelPoint = chartPoint => $"{chartPoint.Y} ({chartPoint.Participation:P})",
                      Fill = System.Windows.Media.Brushes.SteelBlue // Màu đỏ cho "Dịch vụ"

                }
            };

            // set ngày
                 Date = DateTime.Now;

            StatisticClick();



           
        }
        private void UpdateChartValues()
        {
            if (SalesSeries != null && SalesSeries.Count > 0)
            {
                var columnSeries = SalesSeries[0] as ColumnSeries;
                if (columnSeries != null)
                {
                    columnSeries.Values = new ChartValues<double>(MonthlySales);
                }
            }
        }
        public async Task CalculateMonthlySales()
        {
            MonthlySales.Clear();

            for (int month = 1; month <= 12; month++)
            {
                double totalSaleOrder = await _saleOrderHelper.GetTotalOrderValue(month);
                double totalServiceOrder = await _serviceOrderHelper.GetTotalOrderValue(month);

                double totalRevenue = (totalSaleOrder + totalServiceOrder) / 1_000_000.0; // Convert to millions
                MonthlySales.Add(totalRevenue);
            }
            // t8 
          /*  MonthlySales.Add(0);
            MonthlySales.Add(0);
            MonthlySales.Add(0);
            MonthlySales.Add(0);
            MonthlySales.Add(0);*/

            UpdateChartValues();

        }

      
        // CLick thống kê theo ngày
        private async Task StatisticClick()
        {

            if (Date.HasValue)
            {
                DateTime selectedDate = Date.Value;

                // Lấy tổng giá trị tất cả các hóa đơn sản phẩm của ngày được chọn
                double totalProductValue = await _saleOrderHelper.GetTotalOrderValue(selectedDate);

                // Lấy tổng giá trị tất cả các hóa đơn dịch vụ của ngày được chọn
                double totalServiceValue = await _serviceOrderHelper.GetTotalOrderValue(selectedDate);

                // Cập nhật giá trị cho PieChart
                UpdatePieChartValues(totalServiceValue, totalProductValue);
            }
        }

        public void UpdatePieChartValues(double serviceValue, double productValue)
        {
            // Chuẩn hóa thành đơn vị triệu đồng
            double serviceValueInMillion = serviceValue / 1000000.0;
            double productValueInMillion = productValue / 1000000.0;

            ServiceValue.Value = serviceValueInMillion;
            ProductValue.Value = productValueInMillion;

            OnPropertyChanged(nameof(ServiceValue));
            OnPropertyChanged(nameof(ProductValue));
        }


        private async Task ViewGoldPriceClick(object parameter)
        {
          string url=parameter as string;
            if (!string.IsNullOrEmpty(url))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName=url,
                    UseShellExecute = true
                });
            }
        }







        private async Task AddServiceOrderClick()
        {
            var addSerView = new ServiceView
            {
                DataContext = new ServiceViewModel()
            };
            addSerView.ShowDialog();
        }

        private async Task AddProClick()
        {
            var PurchaseProView = new PurchaseOrderView
            {
                DataContext = new PurchaseOderViewModel()
            };
            PurchaseProView.ShowDialog();
        }

        private async Task AddOrderClick()
        {
            var BillView = new BillView
            {
                DataContext = new BillViewModel()
            };
            BillView.ShowDialog();
           
        }
    }

}
