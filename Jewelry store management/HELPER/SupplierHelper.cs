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
    public class SupplierHelper
    {

        // khởi tạo + kết nối firebase
        private readonly IFirebaseClient _client;

        public SupplierHelper()
        {
            _client = FirebaseConfigSingleton.GetClient();
        }



        // thêm supplier
        public async Task AddSupplier(Supplier supplier)
        {
            SetResponse response = await _client.SetAsync($"Suppliers/{supplier.SID}", supplier);
        }

        // lấy supplier theo ID
        public async Task<Supplier> GetSupplier(string supplierId)
        {
            FirebaseResponse response = await _client.GetAsync($"Suppliers/{supplierId}");
            return response.ResultAs<Supplier>();
        }

        // cập nhật SUP
        public async Task UpdateSupplier(Supplier supplier)
        {
            FirebaseResponse response = await _client.UpdateAsync($"Suppliers/{supplier.SID}", supplier);
        }

        // xóa Sup
        public async Task DeleteSupplier(string supplierId)
        {
            FirebaseResponse response = await _client.DeleteAsync($"Suppliers/{supplierId}");
        }
        
        // lấy list tất cả SUP
        public async Task<List<Supplier>> GetAllSuppliers()
        {
            FirebaseResponse response = await _client.GetAsync("Suppliers");
            Dictionary<string, Supplier> suppliers = response.ResultAs<Dictionary<string, Supplier>>();

            return suppliers != null ? new List<Supplier>(suppliers.Values) : new List<Supplier>();
        }

        // tìm kiếm SUP theo têm, trả về một list
        public async Task<List<Supplier>> SearchSupplierByName(string name)
        {
            List<Supplier> allSuppliers = await GetAllSuppliers();
            return allSuppliers.Where(s => s.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        // tìm kiếm Sup theo Id, tả về 1 SUP
        public async Task<Supplier> SearchSupplierById(string supplierId)
        {
            return await GetSupplier(supplierId);
        }
    }
}
