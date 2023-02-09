using System;

namespace beSS.Models
{
    public class Bill
    {
        public Guid BillID { get; set; }
        public Guid UserID { get; set; }
        public Cart Cart { get; set; }
        public string AddressTranfer { get; set; }
        public string NameCustomer { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
    }
}