using System;
using System.Collections.Generic;

namespace beSS.Models
{
    public class Cart
    {
        public Guid CartID { get; set; }
        public Guid UserID { get; set; }
        
        public List<Order> Orders { get; set; }
        public int TotalMoneyCart { get; set; }
        public bool IsinBill { get; set; }
    }
}