using System;
using System.Collections.Generic;
using System.Linq;
using beSS.Models;
using beSS.Models.RequestModels;
using beSS.Models.ViewModels;

namespace beSS.Services.Impl
{
    public class OrderService:IOrderService
    {
        private readonly MasterDbContext _context;

        public OrderService(MasterDbContext context)
        {
            _context = context;
        }
        public List<OrderResponse> GetOrder()
        {
            var listOrder = _context.Orders.Select(o => new OrderResponse()
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
            }).ToList();
            return listOrder;
        }

        public OrderResponse CreateOrder(CreateOrder request)
        {
            var targetProduct = _context.Products.FirstOrDefault(p => p.ProductID == request.ProductID);
            if (targetProduct == null)
            {
                throw new Exception("not found");
            }

            var newOrder = new Order()
            {
                OrderID = Guid.NewGuid(),
                UserID = request.UserID,
                Product = targetProduct,
                TotalMoney = targetProduct.Price * request.QuantityOrder
            };
            _context.Add(newOrder);
            _context.SaveChanges();
            return new OrderResponse()
            {
                OrderID = newOrder.OrderID,
                UserID = newOrder.UserID,
                ProductID = newOrder.Product.ProductID,
                Name = newOrder.Product.Name,
                Description = newOrder.Product.Description,
                ImageURL = newOrder.Product.ImageURL,
                QuantityAvailable = newOrder.Product.QuantityAvailable,
                Price = newOrder.Product.Price,
                Size = newOrder.Product.Size,
                Brand = newOrder.Product.Brand,
                Categories = newOrder.Product.Categories,
                TotalMoney = newOrder.TotalMoney
            };
        }

        public OrderResponse EditOrder(EditOrder request)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrder(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}