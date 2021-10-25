using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySecureApp.Models
{
    public class ordersViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public int? Price { get; set; }
        public string SellerEmail { get; set; }
    }
}
