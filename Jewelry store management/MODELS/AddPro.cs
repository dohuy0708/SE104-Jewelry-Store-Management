using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry_store_management.MODELS
{
    public class AddPro
    {
        public string SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string AddDate { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        public string StatusImage { get; set; }
        public AddPro(Supplier s, string productID, string productName, string addDate, int amount, string status, string statusImage)
        {
            SupplierID = s.SID;
            SupplierName = s.Name;
            ProductID = productID;
            ProductName = productName;
            AddDate = addDate;
            Amount = amount;
            Status = status;
            StatusImage = statusImage;
        }
    }
}
