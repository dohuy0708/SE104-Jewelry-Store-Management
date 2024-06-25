using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
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


namespace Jewelry_store_management.VIEWMODEL
{
    public class scrHomeViewModel : INotifyPropertyChanged
    {
        public ICommand AddOrderCommand { get; set; }
        public ICommand AddServiceOrderCommand { get; set; }
        public ICommand ViewGoldPriceCommand { get; set; }

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

        private List<string> _weeks;
        public List<string> Weeks
        {
            get => _weeks;
            set
            {
                _weeks = value;
                OnPropertyChanged();
            }
        }

        public scrHomeViewModel()
        {
            AddOrderCommand = new RelayCommand(ExecuteAddOrder);
            AddServiceOrderCommand = new RelayCommand(ExecuteAddServiceOrder);
            ViewGoldPriceCommand = new RelayCommand(ExecuteViewGoldPrice);
            SalesSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> { 400, 800, 650, 700, 600, 450, 750 }
                }
            };

            Weeks = new List<string> { "Tuần 5", "Tuần 6", "Tuần 7", "Tuần 8", "Tuần 9", "Tuần 10", "Tuần 11" };
        }

        private void ExecuteAddOrder(object parameter)
        {
           
        }

        private void ExecuteAddServiceOrder(object parameter)
        {
           
        }

        private void ExecuteViewGoldPrice(object parameter)
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}
