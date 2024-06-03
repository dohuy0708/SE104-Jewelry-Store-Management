using FireSharp.Config;
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
    public class UserHelper
    {
       
        
            private IFirebaseClient _client;

            public UserHelper()
            {
                var config = new FirebaseConfig
                {
                    AuthSecret="ny46fjmoYlYZTyuW2P2M51BfrQDb5zibqXo2MNqC",
                    BasePath="https://test-382ab-default-rtdb.firebaseio.com/"
                };

                _client = new FireSharp.FirebaseClient(config);
            }

            public async Task<List<User>> GetAllUsers()
            {
                FirebaseResponse response = await _client.GetAsync("users");
                var result = response.ResultAs<Dictionary<string, User>>();

                return result != null ? new List<User>(result.Values) : new List<User>();
            }

            public async Task AddUser(User newUser)
            {
                PushResponse response = await _client.PushAsync("users", newUser);
                newUser.Uid = response.Result.name;
            }

            public async Task UpdateUser(User user)
            {
                FirebaseResponse response = await _client.UpdateAsync($"users/{user.Uid}", user);
            }

            public async Task DeleteUser(User user)
            {
                FirebaseResponse response = await _client.DeleteAsync($"users/{user.Uid}");
            }
        }

    

}
