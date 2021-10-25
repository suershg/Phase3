using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace IdentitySecureApp.Models
{
    public partial class Tblorders
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? LaptopId { get; set; }
        public string CustomerMail { get; set; }
        public bool? Paid { get; set; }

        public virtual Tbllaptops Laptop { get; set; }
    }
}