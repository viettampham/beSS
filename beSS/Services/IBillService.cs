using System;
using System.Collections.Generic;
using beSS.Models;
using beSS.Models.RequestModels;
using beSS.Models.ViewModels;

namespace beSS.Services
{
    public interface IBillService
    {
        List<BillResponse> GetAll();
        List<BillResponse> GetAllBillByUser(Guid id);
        MessageResponse CreateBill(CreateBillRequest request);
        MessageResponse DeleteBill(Guid id);
    }
}