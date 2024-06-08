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
        public string SupplierName { get; set; }
        public string SID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DatePurchase { get; set; }
        public double TotalPrice { get; set; }
        public double Voucher { get; set; }
        public double Cost { get; set; }
        public string Pay { get; set; } // ck hay tm
        public List<Product> ListPurchaseProduct { get; set; }

        public PurchaseOrder(string purchaseID, string supplierName, string sID, string phone, string address, string datePurchase, double totalPrice, double voucher, double cost, string pay, List<Product> listPurchaseProduct)
        {
            PurchaseID=purchaseID;
            SupplierName=supplierName;
            SID=sID;
            Phone=phone;
            Address=address;
            DatePurchase=datePurchase;
            TotalPrice=totalPrice;
            Voucher=voucher;
            Cost=cost;
            Pay=pay;
            ListPurchaseProduct=listPurchaseProduct;
        }

        public void CalculateTotalPrice()
        {
            TotalPrice = ListPurchaseProduct.Sum(product => product.SalePrice);
        }
        public void CalculateCost()
        {
            Cost = TotalPrice - Voucher;
        }

    }
}
