﻿using System;
using System.Collections.Generic;
using System.Linq;
using beSS.Models;
using beSS.Models.RequestModels;
using beSS.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

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
                QuantityOrder = o.QuantityOrder,
                TotalMoney = o.TotalMoney,
                IsinCart = o.IsinCart
            }).ToList();
            return listOrder;
        }

        public OrderResponse CreateOrder(CreateOrder request)
        {
            if (request.QuantityOrder <= 0 )
            {
                throw new Exception("quantity Order > 0");
            }

            var checkOrder = _context.Orders
                .Include(o=>o.Product)
                .FirstOrDefault(o => o.UserID == request.UserID && o.Product.ProductID == request.ProductID && o.IsinCart == false);
            
            if (checkOrder!=null)
            {
                checkOrder.QuantityOrder = checkOrder.QuantityOrder + request.QuantityOrder;
                checkOrder.TotalMoney = checkOrder.QuantityOrder * checkOrder.Product.Price;
                _context.SaveChanges();
                return new OrderResponse()
                {
                    OrderID = checkOrder.OrderID,
                    UserID = checkOrder.UserID,
                    ProductID = checkOrder.Product.ProductID,
                    Name = checkOrder.Product.Name,
                    Description = checkOrder.Product.Description,
                    ImageURL = checkOrder.Product.ImageURL,
                    QuantityAvailable = checkOrder.Product.QuantityAvailable,
                    Price = checkOrder.Product.Price,
                    Size = checkOrder.Product.Size,
                    Brand = checkOrder.Product.Brand,
                    Categories = checkOrder.Product.Categories,
                    QuantityOrder = checkOrder.QuantityOrder,
                    TotalMoney = checkOrder.TotalMoney,
                    IsinCart = checkOrder.IsinCart
                };
            }

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
                QuantityOrder = request.QuantityOrder,
                TotalMoney = targetProduct.Price * request.QuantityOrder,
                IsinCart = false
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
                QuantityOrder = newOrder.QuantityOrder,
                TotalMoney = newOrder.TotalMoney,
                IsinCart = newOrder.IsinCart
            };
        }

        public OrderResponse EditOrder(EditOrder request)
        {
            throw new NotImplementedException();
        }

        public MessageResponse DeleteOrder(Guid id)
        {
            var targetOrder = _context.Orders.FirstOrDefault(o => o.OrderID == id);
            _context.Remove(targetOrder);
            return new MessageResponse()
            {
                Status = 200,
                Message = "Xóa thành công"
            };
        }
    }
}