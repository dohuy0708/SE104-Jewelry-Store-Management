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
    }
}
