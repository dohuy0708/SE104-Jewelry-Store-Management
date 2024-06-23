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
        public string DateDelivery { get; set; }
        public double TotalPrice { get; set; }
       
        public List<Product> ListServiceProduct { get; set; }
        public string Describe{ get; set; }
        public string Status{ get; set; }
      
        public ServiceOrder(string serviceID, string serviceName, string customerName, string cPhone, string cEmail, string cAddress, string dateOrder,string dateDelivery, double totalPrice, double voucher, double cost, string pay, List<Product> listServiceProduct, string describe, string status, string StatusImage)
        {
            ServiceID = serviceID;
            ServiceName = serviceName;
            CustomerName = customerName;
            CPhone = cPhone;
            CEmail = cEmail;
            CAddress = cAddress;
            DateOrder = dateOrder;
            DateDelivery = dateDelivery;
            TotalPrice = totalPrice;
        
            ListServiceProduct = listServiceProduct;
            Describe = describe;
            Status = status;
    
        }
        

        public void CalculateTotalPrice()
        {
            TotalPrice = (double)ListServiceProduct.Sum(product => product.SalePrice);
        }
      
    }
}
