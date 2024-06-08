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
    public class ProductHelper
    {
        private readonly IFirebaseClient _client;
        public ProductHelper()
        {
            _client = FirebaseConfigSingleton.GetClient();
        }

        // Thêm product
        public async Task AddProduct(Product product)
        {
            SetResponse response = await _client.SetAsync($"Products/{product.PID}", product);
        }

        // Lấy product theo ID
        public async Task<Product> GetProduct(string productId)
        {
            FirebaseResponse response = await _client.GetAsync($"Products/{productId}");
            return response.ResultAs<Product>();
        }

        // Cập nhật product
        public async Task UpdateProduct(Product product)
        {
            FirebaseResponse response = await _client.UpdateAsync($"Products/{product.PID}", product);
        }

        // Xóa product
        public async Task DeleteProduct(string productId)
        {
            FirebaseResponse response = await _client.DeleteAsync($"Products/{productId}");
        }

        // Lấy danh sách tất cả các product
        public async Task<List<Product>> GetAllProducts()
        {
            FirebaseResponse response = await _client.GetAsync("Products");
            Dictionary<string, Product> products = response.ResultAs<Dictionary<string, Product>>();

            return products != null ? new List<Product>(products.Values) : new List<Product>();
        }

        // Tìm kiếm product theo tên, trả về một danh sách
        public async Task<List<Product>> SearchProductByName(string name)
        {
            List<Product> allProducts = await GetAllProducts();
            return allProducts.Where(p => p.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        // Tìm kiếm product theo ID, trả về một product
        public async Task<Product> SearchProductById(string productId)
        {
            return await GetProduct(productId);
        }
    }
}
