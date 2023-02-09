using System;
using System.Collections.Generic;

namespace beSS.Models.RequestModels
{
    public class CreateCart
    {
        public Guid UserID { get; set; }
        public List<Guid> OrderIDs { get; set; }
    }
}