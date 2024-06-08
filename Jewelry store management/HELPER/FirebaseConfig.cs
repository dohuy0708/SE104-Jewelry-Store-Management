using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry_store_management.HELPER
{
    public class FirebaseConfigSingleton
    {
        private static readonly IFirebaseConfig _config = new FirebaseConfig
        {
            AuthSecret = "ny46fjmoYlYZTyuW2P2M51BfrQDb5zibqXo2MNqC",
            BasePath = "https://test-382ab-default-rtdb.firebaseio.com/"
        };

        public static IFirebaseClient GetClient()
        {
            return new FireSharp.FirebaseClient(_config);
        }
    }
}
