using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Interfaces;
using FireSharp.Response;
using Jewelry_store_management.MODELS;

namespace Jewelry_store_management.HELPER
{
    public class UserHelper
    {
        private readonly IFirebaseClient _client;

        public UserHelper()
        {
            var config = new FireSharp.Config.FirebaseConfig
            {
                AuthSecret = "ny46fjmoYlYZTyuW2P2M51BfrQDb5zibqXo2MNqC",
                BasePath = "https://test-382ab-default-rtdb.firebaseio.com/"
            };

            _client = new FireSharp.FirebaseClient(config);
        }

        // thêm user
        public async Task AddUser(User newUser)
        {
            // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
            newUser.Password = HashPassword(newUser.Password);
            PushResponse response = await _client.PushAsync("users", newUser);
            newUser.Uid = response.Result.name;
        }

        // lấy tất cả các   user trong DB
        public async Task<List<User>> GetAllUsers()
        {
            FirebaseResponse response = await _client.GetAsync("users");
            var result = response.ResultAs<Dictionary<string, User>>();

            return result != null ? new List<User>(result.Values) : new List<User>();
        }
        // sửa user
        public async Task UpdateUser(User user)
        {
            FirebaseResponse response = await _client.UpdateAsync($"users/{user.Uid}", user);
        }
        // xóa user
        public async Task DeleteUser(User user)
        {
            FirebaseResponse response = await _client.DeleteAsync($"users/{user.Uid}");
        }


        /// Đăng nhập 

        // Lấy người dùng bằng email
        public async Task<User> GetUserByEmail(string email)
        {
            FirebaseResponse response = await _client.GetAsync("users");
            var users = response.ResultAs<Dictionary<string, User>>();

            foreach (var user in users.Values)
            {
                if (user.Email == email)
                    return user;
            }

            return null;
        }

        // Đăng ký người dùng
        public async Task<bool> RegisterUser(User newUser)
        {
            // Kiểm tra xem người dùng có tồn tại không
            var existingUser = await GetUserByEmail(newUser.Email);
            if (existingUser != null)
                return false; // Người dùng đã tồn tại, không thể đăng ký

            // Thêm người dùng mới vào database
            await AddUser(newUser);
            return true; // Đăng ký thành công
        }

        // Đăng nhập người dùng
        public async Task<User> Login(string email, string password)
        {
            var user = await GetUserByEmail(email);
            if (user != null && VerifyPassword(password, user.Password))
                return user; // Đăng nhập thành công

            return null; // Sai thông tin đăng nhập
        }

        // Mã hóa mật khẩu bằng SHA-256
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Kiểm tra mật khẩu đã mã hóa có khớp với mật khẩu gốc hay không
        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            string hashedInputPassword = HashPassword(inputPassword);
            return hashedInputPassword == hashedPassword;
        }

       
    }
}
