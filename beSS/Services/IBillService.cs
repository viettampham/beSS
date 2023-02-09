using System;
using System.Collections.Generic;
using beSS.Models;
using beSS.Models.RequestModels;

namespace beSS.Services
{
    public interface IBillService
    {
        List<Bill> GetAll();
        List<Bill> GetAllBillByUser(Guid id);
        Bill CreateBill(CreateBillRequest request);
        Bill DeleteBill(Guid id);
    }
}