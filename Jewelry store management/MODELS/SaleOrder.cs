using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry_store_management.MODELS
{
    public class SaleOrder
    {
        public SaleOrder() { }
        public string SaleId { get; set; }
        public string CustomerName { get; set; }
        public string CPhone { get; set; }
        public string CEmail { get; set; }
        public string CAddress { get; set; }
        public string DateSale { get; set; }
        public double TotalPrice { get; set; }
        public double Voucher { get; set; }
        public double Cost { get; set; }
        public string Pay { get; set; } // ck hay tm

        public List<Product> ListSaleProduct { get; set; }

        public SaleOrder(string saleId, string customerName, string cPhone, string cEmail, string cAddress, string dateSale, double totalPrice, double voucher, double cost, string pay, List<Product> listSaleProduct)
        {
            SaleId=saleId;
            CustomerName=customerName;
            CPhone=cPhone;
            CEmail=cEmail;
            CAddress=cAddress;
            DateSale=dateSale;
            TotalPrice=totalPrice;
            Voucher=voucher;
            Cost=cost;
            Pay=pay;
            ListSaleProduct=listSaleProduct;
        }

        public void CalculateTotalPrice()
        {
            TotalPrice = ListSaleProduct.Sum(product => product.SalePrice);
        }
        public void CalculateCost()
        {
            Cost = TotalPrice - Voucher;
        }
    }
}
