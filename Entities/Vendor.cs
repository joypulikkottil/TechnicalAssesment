// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DOF_POList_EFCore.Entities
{
    public partial class Vendor
    {
        public Vendor()
        {
            Orders = new HashSet<Order>();
        }

        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool? Status { get; set; }
        public bool? Deleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}