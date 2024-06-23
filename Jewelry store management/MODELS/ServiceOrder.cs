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
        public List<Product> ListServiceProduct { get; set; }
        public string Describe{ get; set; }
        public string Status{ get; set; }
        public string statusImage{ get; set; }

        public ServiceOrder(string serviceID, string serviceName, string customerName, string cPhone, string cEmail, string cAddress, string dateOrder, double totalPrice, double voucher, double cost, string pay, List<Product> listServiceProduct, string describe, string status, string StatusImage)
        {
            ServiceID = serviceID;
            ServiceName = serviceName;
            CustomerName = customerName;
            CPhone = cPhone;
            CEmail = cEmail;
            CAddress = cAddress;
            DateOrder = dateOrder;
            TotalPrice = totalPrice;
            Voucher = voucher;
            Cost = cost;
            Pay = pay;
            ListServiceProduct = listServiceProduct;
            Describe = describe;
            Status = status;
            statusImage = StatusImage;
        }
        

        public void CalculateTotalPrice()
        {
            TotalPrice = (double)ListServiceProduct.Sum(product => product.SalePrice);
        }
        public void CalculateCost()
        {
            Cost = TotalPrice - Voucher;
        }

    }
}
