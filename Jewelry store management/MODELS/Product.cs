using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Jewelry_store_management.MODELS
{
    public class Product
    {
      
        public Product() { }
        public string Name { get; set; }
        public string PID { get; set; }
        public string Description { get; set; } // mô tả 
        public string Category { get; set; } // Vàng / Trang sức
        public string SupplierName { get; set; }
        public decimal PurchasePrice { get; set; }
        public double Profit { get; set; }// lợi nhuận 
        public decimal SalePrice { get; set; }
        public string DateImport { get; set; }
        public double Weight { get; set; } // trọng lượng vàng, nhiu phân  
        public string Size { get; set; }
        public string Type { get; set; } //  vàng: nhẫn/ thẻ ; trang sức : nhẫn, dây chuyển, bông tai
        public string Material { get; set; } // chất liệu : 24k 18k bạc bạch kim 
        public int   Quantity { get; set; } // Tổng số lượng
        public int Remain { get; set; } // số lượng còn lại 
        public string ImageURL { get; set; }

        public Product(string name, string pID, string description, string category, string supplierName, decimal purchasePrice, double profit, decimal salePrice, string dateImport, double weight, string size, string type, string material, int number, string imageURL)
        {
            Name=name;
            PID=pID;
            Description=description;
            Category=category;
            SupplierName=supplierName;
            PurchasePrice=purchasePrice;
            Profit=profit;
            SalePrice=salePrice;
            DateImport=dateImport;
            this.Weight=weight;
            Size=size;
            Type=type;
            Material=material;
            Quantity=number;
            ImageURL=imageURL;
        }
        public Product(string name, string pID, decimal purchasePrice,string size,int number)
        {
            Name=name;
            PID=pID;
            PurchasePrice = purchasePrice;
            Size = size;
            Quantity = number;
        }
    }
}
