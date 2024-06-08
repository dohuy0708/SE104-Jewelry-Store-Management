using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry_store_management.MODELS
{
    public class Supplier
    {
        public string Name { get; set; }
        public string SID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Supplier() { }
        public Supplier(string name, string sID, string phone, string address)
        {
            Name=name;
            SID=sID;
            Phone=phone;
            Address=address;

        }


        private static Random random = new Random();
        public string GenerateId()
        {
            const string prefix = "SP"; // dinh nghia tien to co dinh

            // tao 4 so ngan nhien
            int randomNumber = random.Next(10000);

            // dinh dang thanh chuoi 4 ky tu
            string randomNumberString = randomNumber.ToString("D4");

            // ket hop thanh ID
            string id = prefix + randomNumberString;

            return id;
        }
       

    }
}
