using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry_store_management.MODELS
{
    public class User
    {
        public string Name { get; set; }
        public string Uid { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ImageURL { get; set; }
        public string Address { get; set; }
       

        public User() { }
        public User(string name, string Uid, string Email, string Pass,string Phone, string ProfileURL, string address)
        {
            this.Name = name;
            this.Uid = Uid;
            this.Email = Email;
            this.Phone = Phone;
            this.ImageURL = ProfileURL;
            this.Password = Pass;
            Address=address;
        }


    }
   
}
