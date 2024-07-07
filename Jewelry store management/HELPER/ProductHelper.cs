using Firebase.Storage;
using FireSharp.Interfaces;
using FireSharp.Response;
using Jewelry_store_management.MODELS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Jewelry_store_management.HELPER
{
    public class ProductHelper
    {
        private readonly IFirebaseClient _client;
        private readonly FirebaseStorage _storage;

        public ProductHelper()
        {
            _client = FirebaseConfigSingleton.GetClient();
            _storage = new FirebaseStorage("test-382ab.appspot.com");
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

        // Upload image to FirebaseStorage and get the URL
        // Upload image to FirebaseStorage and get the URL
        // Upload image to FirebaseStorage and get the URL
        public async Task<string> UploadImageAsync(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var imageUrl = await _storage
                    .Child("images")
                    .Child(Path.GetFileName(filePath))
                    .PutAsync(stream);

                return   imageUrl;
            }
        }

        // Giảm số lượng sản phẩm
        public async Task DecreaseProductQuantity(string productId, int quantity)
        {
            var product = await GetProduct(productId);
            if (product != null)
            {
                // Giảm số lượng sản phẩm
                product.Quantity -= quantity;
                // Cập nhật lại thông tin sản phẩm trong Firebase
                await UpdateProduct(product);
            }
        }
        // Tăng số lượng sản phẩm
        public async Task IncreaseProductQuantity(string productId, int quantity)
        {
            var product = await GetProduct(productId);
            if (product != null)
            {
                // Tăng số lượng sản phẩm
                product.Quantity += quantity;
                // Cập nhật lại thông tin sản phẩm trong Firebase
                await UpdateProduct(product);
            }
        }

        private string ConvertBitmapImageToBase64(BitmapImage bitmapImage)
        {
            if (bitmapImage == null)
                throw new ArgumentNullException(nameof(bitmapImage));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        public BitmapImage ConvertBase64ToBitmapImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }
            return bitmapImage;
        }


    }
}
