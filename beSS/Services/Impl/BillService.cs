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
            List<BillResponse> listBill = _context.Bills
                .Select(b => new BillResponse()
                {
                    BillID = b.BillID,
                    UserID = b.UserID,
                    Orders = _context.Carts
                        .Include(c=>c.Orders)
                        .FirstOrDefault(c=>c.UserID == b.UserID)
                        .Orders.Select(o=>new OrderResponse()
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
                            IsinCart = o.IsinCart
                        }).ToList(),
                    TotalBill = _context.Carts
                        .FirstOrDefault(c=>c.UserID == b.UserID).TotalMoneyCart,
                    AddressTranfer = b.AddressTranfer,
                    NameCustomer = b.NameCustomer,
                    PhoneNumber = b.PhoneNumber,
                    IsPayed = b.IsPayed
                }).ToList();
            return listBill;
        }

        public List<BillResponse> GetAllBillByUser(Guid id)
        {
            var listBill = _context.Bills
                .Select(b => new BillResponse()
                {
                    BillID = b.BillID,
                    UserID = b.UserID,
                    Orders = _context.Carts
                        .Include(c=>c.Orders)
                        .FirstOrDefault(c=>c.UserID == b.UserID)
                        .Orders.Select(o=>new OrderResponse()
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
                            IsinCart = o.IsinCart
                        }).ToList(),
                    TotalBill = _context.Carts
                        .FirstOrDefault(c=>c.UserID == b.UserID).TotalMoneyCart,
                    AddressTranfer = b.AddressTranfer,
                    NameCustomer = b.NameCustomer,
                    PhoneNumber = b.PhoneNumber,
                    IsPayed = b.IsPayed
                }).ToList();

            return listBill;
        }

        public MessageResponse CreateBill(CreateBillRequest request)
        {
            var newBill = new Bill()
            {
                BillID = Guid.NewGuid(),
                UserID = request.UserID,
                Cart = _context.Carts
                    .FirstOrDefault(c => c.UserID == request.UserID && c.IsinBill == false),
                AddressTranfer = request.AddressTranfer,
                NameCustomer = request.NameCustomer,
                PhoneNumber = request.PhoneNumber,
                IsPayed = false//(false == chua xac nhan - true == xac nhan)
            };
            _context.Carts.FirstOrDefault(c => c.UserID == request.UserID && c.IsinBill == false)!.IsinBill = true;
            _context.Carts.Include(c=>c.Orders)
                .FirstOrDefault(c => c.UserID == request.UserID && c.IsinBill == false)!.Orders.ForEach(
                o=>o.IsinCart = true);
            _context.Add(newBill);
            _context.SaveChanges();
            
            /*return new BillResponse()
            {
                BillID = newBill.BillID,
                UserID = newBill.UserID,
                Cart = new CartResponse()
                {
                    CartID = newBill.Cart.CartID,
                    UserID = newBill.Cart.UserID,
                    Orders = _context.Orders
                        .Include(o=>_context.Products)
                        .Where(o=>o.UserID == newBill.UserID)
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
                            TotalMoney = o.TotalMoney
                        }).ToList(),
                },
                AddressTranfer = newBill.AddressTranfer,
                NameCustomer = newBill.NameCustomer,
                PhoneNumber = newBill.PhoneNumber,
                Status = newBill.Status
            };*/
            
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
    }
}