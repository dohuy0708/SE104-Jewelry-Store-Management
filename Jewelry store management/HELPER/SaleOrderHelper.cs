using FireSharp.Interfaces;
using FireSharp.Response;
using Jewelry_store_management.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry_store_management.HELPER
{
    public class SaleOrderHelper
    {
        private readonly IFirebaseClient _client;
        public SaleOrderHelper()
        {
            _client = FirebaseConfigSingleton.GetClient();
        }

        // Thêm đơn hàng bán hàng
        public async Task AddSaleOrder(SaleOrder saleOrder)
        {
            SetResponse response = await _client.SetAsync($"SaleOrders/{saleOrder.SaleId}", saleOrder);
        }

        // Lấy đơn hàng bán hàng theo ID
        public async Task<SaleOrder> GetSaleOrder(string saleOrderId)
        {
            FirebaseResponse response = await _client.GetAsync($"SaleOrders/{saleOrderId}");
            return response.ResultAs<SaleOrder>();
        }

        // Cập nhật đơn hàng bán hàng
        public async Task UpdateSaleOrder(SaleOrder saleOrder)
        {
            FirebaseResponse response = await _client.UpdateAsync($"SaleOrders/{saleOrder.SaleId}", saleOrder);
        }

        // Xóa đơn hàng bán hàng
        public async Task DeleteSaleOrder(string saleOrderId)
        {
            FirebaseResponse response = await _client.DeleteAsync($"SaleOrders/{saleOrderId}");
        }

        // Lấy danh sách tất cả các đơn hàng bán hàng
        public async Task<List<SaleOrder>> GetAllSaleOrders()
        {
            FirebaseResponse response = await _client.GetAsync("SaleOrders");
            Dictionary<string, SaleOrder> saleOrders = response.ResultAs<Dictionary<string, SaleOrder>>();

            return saleOrders != null ? new List<SaleOrder>(saleOrders.Values) : new List<SaleOrder>();
        }

        // Tìm kiếm đơn hàng bán hàng theo tên khách hàng, trả về một danh sách
        public async Task<List<SaleOrder>> SearchSaleOrderByCustomerName(string customerName)
        {
            List<SaleOrder> allSaleOrders = await GetAllSaleOrders();
            return allSaleOrders.Where(so => so.CustomerName.IndexOf(customerName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        // Tìm kiếm đơn hàng bán hàng theo ID, trả về một đơn hàng bán hàng
        public async Task<SaleOrder> SearchSaleOrderById(string saleOrderId)
        {
            return await GetSaleOrder(saleOrderId);
        }


        public async Task<double> GetTotalOrderValue(DateTime selectedDate)
        {
            string selectedDateString = selectedDate.ToString("yyyy-MM-dd"); // Chuyển DateTime sang string theo định dạng yyyy-MM-dd
            List<SaleOrder> allSaleOrders = await GetAllSaleOrders();
            double totalValue = allSaleOrders
                .Where(so => so.DateSale== selectedDateString) // So sánh với selectedDateString
                .Sum(so => so.TotalPrice);
            return totalValue;
        }

        // Lấy tổng doanh thu của các đơn hàng bán hàng cho một tháng cụ thể
        // Lấy tổng giá trị đơn hàng bán hàng trong một tháng cụ thể
        public async Task<double> GetTotalOrderValue( int month)
        {
            List<SaleOrder> allSaleOrders = await GetAllSaleOrders();
            double totalValue = allSaleOrders
                .Where(so =>
                {
                    DateTime dateSale;
                    if (DateTime.TryParse(so.DateSale, out dateSale))
                    {
                        return   dateSale.Month == month;
                    }
                    return false;
                })
                .Sum(so => so.TotalPrice);
            return totalValue;
        }

    }
}
