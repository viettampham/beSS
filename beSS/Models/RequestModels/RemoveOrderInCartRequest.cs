using System;

namespace beSS.Models.RequestModels
{
    public class RemoveOrderInCartRequest
    {
        public Guid UserID { get; set; }
        public Guid OrderID { get; set; }
    }
}