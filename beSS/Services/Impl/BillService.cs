using System;
using System.Collections.Generic;
using System.Linq;
using beSS.Models;
using beSS.Models.RequestModels;
using beSS.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace beSS.Services.Impl
{
    public class BillService:IBillService
    {
        private readonly MasterDbContext _context;

        public BillService(MasterDbContext context)
        {
            _context = context;
        }
        public List<BillResponse> GetAll()
        {
            var listBill = _context.Bills
                .Include(b => b.Orders)
                .Select(b => new BillResponse()
                {
                    BillID = b.BillID,
                    UserID = b.UserID,
                    Orders = _context.Orders.Include(o=>o.Product)
                        .Where(o=>o.IDOB == b.BillID && o.UserID == b.UserID && o.IsinBill == true)
                        .Select(o=>new OrderResponse()
                        {
                            OrderID = o.OrderID,
                            UserID = o.UserID,
                            ProductID = o.Product.ProductID,
                            Name = o.Product.Name,
                            Description = o.Product.Description,
                            ImageURL = o.Product.ImageURL,
                            QuantityAvailable = o.Product.QuantityAvailable,
                            Price = o.Product.Price,
                            Size = o.Product.Size,
                            Brand = o.Product.Brand,
                            Categories = o.Product.Categories,
                            QuantityOrder = o.QuantityOrder,
                            TotalMoney = o.TotalMoney,
                            IsinBill = o.IsinBill
                        }).ToList(),
                    TotalBill = b.TotalBill,
                    AddressTranfer = b.AddressTranfer,
                    NameCustomer = b.NameCustomer,
                    PhoneNumber = b.PhoneNumber,
                    IsPayed = b.IsPayed
                }).ToList();
            return listBill;
        }

        public List<BillResponse> GetAllBillNoPayedByUser(Guid id)
        {
            var listBill = _context.Bills
                .Where(b=>b.UserID == id && b.IsPayed == false)
                .Select(b => new BillResponse()
                {
                    BillID = b.BillID,
                    UserID = b.UserID,
                    Orders = _context.Orders.Include(o=>o.Product)
                        .Where(o=>o.IDOB == b.BillID && o.UserID == b.UserID && o.IsinBill == true)
                        .Select(o=>new OrderResponse()
                        {
                            OrderID = o.OrderID,
                            UserID = o.UserID,
                            ProductID = o.Product.ProductID,
                            Name = o.Product.Name,
                            Description = o.Product.Description,
                            ImageURL = o.Product.ImageURL,
                            QuantityAvailable = o.Product.QuantityAvailable,
                            Price = o.Product.Price,
                            Size = o.Product.Size,
                            Brand = o.Product.Brand,
                            Categories = o.Product.Categories,
                            QuantityOrder = o.QuantityOrder,
                            TotalMoney = o.TotalMoney,
                            IsinBill = o.IsinBill
                        }).ToList(),
                    TotalBill = b.TotalBill,
                    AddressTranfer = b.AddressTranfer,
                    NameCustomer = b.NameCustomer,
                    PhoneNumber = b.PhoneNumber,
                    IsPayed = b.IsPayed
                }).ToList();
            return listBill;
        }

        public List<BillResponse> GetAllBillPayedByUser(Guid id)
        {
            var listBill = _context.Bills
                .Where(b=>b.UserID == id && b.IsPayed == true)
                .Select(b => new BillResponse()
                {
                    BillID = b.BillID,
                    UserID = b.UserID,
                    Orders = _context.Orders.Include(o=>o.Product)
                        .Where(o=>o.IDOB == b.BillID && o.UserID == b.UserID && o.IsinBill == true)
                        .Select(o=>new OrderResponse()
                        {
                            OrderID = o.OrderID,
                            UserID = o.UserID,
                            ProductID = o.Product.ProductID,
                            Name = o.Product.Name,
                            Description = o.Product.Description,
                            ImageURL = o.Product.ImageURL,
                            QuantityAvailable = o.Product.QuantityAvailable,
                            Price = o.Product.Price,
                            Size = o.Product.Size,
                            Brand = o.Product.Brand,
                            Categories = o.Product.Categories,
                            QuantityOrder = o.QuantityOrder,
                            TotalMoney = o.TotalMoney,
                            IsinBill = o.IsinBill
                        }).ToList(),
                    TotalBill = b.TotalBill,
                    AddressTranfer = b.AddressTranfer,
                    NameCustomer = b.NameCustomer,
                    PhoneNumber = b.PhoneNumber,
                    IsPayed = b.IsPayed
                }).ToList();
            return listBill;
        }

        public MessageResponse CreateBill(CreateBillRequest request)
        {
            var listOrderTarget = _context.Orders
                .Where(o => o.UserID == request.UserID && o.IsinBill == false)
                .Select(o => o).ToList();
            int totalBill = 0;
            foreach (var order in listOrderTarget)
            {
                totalBill = totalBill + order.TotalMoney;
            }

            var newBill = new Bill()
            {
                BillID = Guid.NewGuid(),
                UserID = request.UserID,
                Orders = listOrderTarget,
                TotalBill = totalBill,
                AddressTranfer = request.AddressTranfer,
                NameCustomer = request.NameCustomer,
                PhoneNumber = request.PhoneNumber,
                IsPayed = false
            };
            
            _context.Add(newBill);
            foreach (var order in listOrderTarget)
            {
                order.IsinBill = true;
                order.IDOB = newBill.BillID;
            }
            _context.SaveChanges();
            return new MessageResponse()
            {
                Status = 200,
                Message = "Success"
            };
        }

        public MessageResponse DeleteBill(Guid id)
        {
            var targetBill = _context.Bills.FirstOrDefault(b => b.BillID == id);
            _context.Remove(targetBill);
            _context.SaveChanges();
            return new MessageResponse()
            {
                Status = 200,
                Message = "Xóa thành công"
            };
        }

        public MessageResponse ConFirmBill(Guid id)
        {
            var targetBill = _context.Bills
                .FirstOrDefault(b => b.BillID == id);
            if (targetBill == null)
            {
                return new MessageResponse()
                {
                    Status = 404,
                    Message = "Not found this bill"
                };
            }

            targetBill.IsPayed = true;
            _context.SaveChanges();
            return new MessageResponse()
            {
                Status = 200,
                Message = "Success"
            };
        }
    }
}