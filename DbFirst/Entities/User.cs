﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DbFirst.Entities
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int? OrganizationId { get; set; }
        public bool? Status { get; set; }
        public bool? Deleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual Organization Organization { get; set; }
    }
}