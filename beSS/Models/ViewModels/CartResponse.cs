using System;
using System.Collections.Generic;

namespace beSS.Models.ViewModels
{
    public class CartResponse
    {
        public Guid CartID { get; set; }
        public Guid UserID { get; set; }
        
        public List<OrderResponse> Orders { get; set; }
        public int TotalMoneyCart { get; set; }
    }
}