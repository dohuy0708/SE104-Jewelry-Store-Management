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
    public class ServiceOrderHelper
    {
        private readonly IFirebaseClient _client;
        public ServiceOrderHelper() 
        {
            _client = FirebaseConfigSingleton.GetClient();
        }

        // Thêm service order
        public async Task AddServiceOrder(ServiceOrder serviceOrder)
        {
            SetResponse response = await _client.SetAsync($"ServiceOrders/{serviceOrder. ServiceID}", serviceOrder);
        }

        // Lấy service order theo ID
        public async Task<ServiceOrder> GetServiceOrder(string serviceOrderId)
        {
            FirebaseResponse response = await _client.GetAsync($"ServiceOrders/{serviceOrderId}");
            return response.ResultAs<ServiceOrder>();
        }

        // Cập nhật service order
        public async Task UpdateServiceOrder(ServiceOrder serviceOrder)
        {
            FirebaseResponse response = await _client.UpdateAsync($"ServiceOrders/{serviceOrder.ServiceID}", serviceOrder);
        }

        // Xóa service order
        public async Task DeleteServiceOrder(string serviceOrderId)
        {
            FirebaseResponse response = await _client.DeleteAsync($"ServiceOrders/{serviceOrderId}");
        }

        // Lấy danh sách tất cả các service order
        public async Task<List<ServiceOrder>> GetAllServiceOrders()
        {
            FirebaseResponse response = await _client.GetAsync("ServiceOrders");
            Dictionary<string, ServiceOrder> serviceOrders = response.ResultAs<Dictionary<string, ServiceOrder>>();

            return serviceOrders != null ? new List<ServiceOrder>(serviceOrders.Values) : new List<ServiceOrder>();
        }

        // Tìm kiếm service order theo tên, trả về một danh sách
        public async Task<List<ServiceOrder>> SearchServiceOrderByName(string name)
        {
            List<ServiceOrder> allServiceOrders = await GetAllServiceOrders();
            return allServiceOrders.Where(so => so.CustomerName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        // Tìm kiếm service order theo ID, trả về một service order
        public async Task<ServiceOrder> SearchServiceOrderById(string serviceOrderId)
        {
            return await GetServiceOrder(serviceOrderId);
        }
    }
}
