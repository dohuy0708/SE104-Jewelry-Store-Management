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
    public class PurchaseOrderHelper
    {
        private readonly IFirebaseClient _client;
        public PurchaseOrderHelper() 
        {
            _client = FirebaseConfigSingleton.GetClient();
        }

        // Thêm đơn hàng mua hàng
        public async Task AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            SetResponse response = await _client.SetAsync($"PurchaseOrders/{purchaseOrder.PurchaseID}", purchaseOrder);
        }

        // Lấy đơn hàng mua hàng theo ID
        public async Task<PurchaseOrder> GetPurchaseOrder(string purchaseOrderId)
        {
            FirebaseResponse response = await _client.GetAsync($"PurchaseOrders/{purchaseOrderId}");
            return response.ResultAs<PurchaseOrder>();
        }

        // Cập nhật đơn hàng mua hàng
        public async Task UpdatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            FirebaseResponse response = await _client.UpdateAsync($"PurchaseOrders/{purchaseOrder.PurchaseID}", purchaseOrder);
        }

        // Xóa đơn hàng mua hàng
        public async Task DeletePurchaseOrder(string purchaseOrderId)
        {
            FirebaseResponse response = await _client.DeleteAsync($"PurchaseOrders/{purchaseOrderId}");
        }

        // Lấy danh sách tất cả các đơn hàng mua hàng
        public async Task<List<PurchaseOrder>> GetAllPurchaseOrders()
        {
            FirebaseResponse response = await _client.GetAsync("PurchaseOrders");
            Dictionary<string, PurchaseOrder> purchaseOrders = response.ResultAs<Dictionary<string, PurchaseOrder>>();

            return purchaseOrders != null ? new List<PurchaseOrder>(purchaseOrders.Values) : new List<PurchaseOrder>();
        }

        // Tìm kiếm đơn hàng mua hàng theo tên nhà cung cấp, trả về một danh sách
        public async Task<List<PurchaseOrder>> SearchPurchaseOrderBySupplierName(string supplierName)
        {
            List<PurchaseOrder> allPurchaseOrders = await GetAllPurchaseOrders();
            return allPurchaseOrders.Where(po => po.SupplierName.IndexOf(supplierName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        // Tìm kiếm đơn hàng mua hàng theo ID, trả về một đơn hàng mua hàng
        public async Task<PurchaseOrder> SearchPurchaseOrderById(string purchaseOrderId)
        {
            return await GetPurchaseOrder(purchaseOrderId);
        }
    }
}
