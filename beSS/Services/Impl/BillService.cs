using System;
using System.Collections.Generic;
using System.Linq;
using beSS.Models;
using beSS.Models.RequestModels;

namespace beSS.Services.Impl
{
    public class BillService:IBillService
    {
        private readonly MasterDbContext _context;

        public BillService(MasterDbContext context)
        {
            _context = context;
        }
        public List<Bill> GetAll()
        {
            var listBill = _context.Bills
                .Select(b => b).ToList();
            return listBill;
        }

        public List<Bill> GetAllBillByUser(Guid id)
        {
            var listBill = _context.Bills
                .Select(b => b).ToList();
            var listBillResponse = new List<Bill>();
            foreach (var bill in listBill)
            {
                if (bill.UserID == id && bill.Status == false)
                {
                    listBillResponse.Add(bill);
                }
            }

            return listBillResponse;
        }

        public Bill CreateBill(CreateBillRequest request)
        {
            var CartRequest = _context.Carts
                .FirstOrDefault(c=>c.UserID == request.UserID);
            var newBill = new Bill()
            {
                BillID = Guid.NewGuid(),
                UserID = request.UserID,
                Cart = CartRequest,
                AddressTranfer = request.AddressTranfer,
                NameCustomer = request.NameCustomer,
                PhoneNumber = request.PhoneNumber,
                Status = false
            };
            _context.Remove(CartRequest);
            _context.SaveChanges();
            return newBill;
        }

        public Bill DeleteBill(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}