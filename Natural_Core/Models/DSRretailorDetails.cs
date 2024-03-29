﻿using System;
using System.Collections.Generic;
using System.Text;
#nullable disable
namespace Natural_Core.Models
{
    public class DSRretailorDetails
    {
        public string Executive { get; set; }
        public string Distributor { get; set; }
        public string Retailor { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string OrderBy { get; set; }
        public Decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
