using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry_store_management.MODELS
{
    public  class PurchaseOrder
    {
        public PurchaseOrder() { }
        
        public string PurchaseID { get; set; }
        public string CustomerName { get; set; }
        public string SID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DatePurchase { get; set; }
        public double TotalPrice { get; set; }
        public double Voucher { get; set; }
        public double Cost { get; set; }
        public string Pay { get; set; } // ck hay tm
        public List<Product> ListPurchaseProduct { get; set; }

    }
}
