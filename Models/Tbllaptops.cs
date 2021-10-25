using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IdentitySecureApp.Models
{
    public partial class Tbllaptops
    {
        public Tbllaptops()
        {
            Tblorders = new HashSet<Tblorders>();
        }

        public int Id { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public int? Price { get; set; }
        public string SellerEmail { get; set; }

        public virtual ICollection<Tblorders> Tblorders { get; set; }
    }
}
