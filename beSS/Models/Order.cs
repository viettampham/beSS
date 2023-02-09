using System;
using System.Collections.Generic;

namespace beSS.Models
{
    public class Order
    {
        public Guid OrderID { get; set; }
        public Guid UserID { get; set; }
        public Product Product { get; set; }
        public int TotalMoney { get; set; }
    }
}