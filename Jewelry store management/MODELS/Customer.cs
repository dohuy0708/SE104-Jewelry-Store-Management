using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry_store_management.MODELS
{
    public class Customer
    {
        public Customer() { }
        public string Name { get; set; }
        public string CID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Customer(string name, string cID, string phone, string email, string address)
        {
            Name=name;
            CID=cID;
            Phone=phone;
            Email=email;
            Address=address;
        }
    }
}
