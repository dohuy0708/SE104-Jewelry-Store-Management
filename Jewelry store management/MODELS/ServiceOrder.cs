using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry_store_management.MODELS
{
    public class ServiceOrder
    {
        public ServiceOrder() { }
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string CustomerName { get; set; }
        public string CPhone { get; set; }
        public string CEmail { get; set; }
        public string CAddress { get; set; }
        public string DateOrder { get; set; }
        public double TotalPrice { get; set; }
        public double Voucher { get; set; }
        public double Cost { get; set; }
        public string Pay { get; set; } // ck hay tm
        public List<Product> ListSaleProduct { get; set; }
        public string Describe{ get; set; }

    }
}
