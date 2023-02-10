using System;
using System.Collections.Generic;
using System.Linq;
using beSS.Models;
using beSS.Models.RequestModels;
using beSS.Models.ViewModels;

namespace beSS.Services.Impl
{
    public class ProductService:IProductService
    {
        private readonly MasterDbContext _context;

        public ProductService(MasterDbContext context)
        {
            _context = context;
        }
        public List<ProductResponse> GetProduct()
        {
            var listProduct = _context.Products
                .Select(p => new ProductResponse()
                {
                    ProductID = p.ProductID,
                    Name = p.Name,
                    Description = p.Description,
                    ImageURL = p.ImageURL,
                    QuantityAvailable = p.QuantityAvailable,
                    Price = p.Price,
                    Size = p.Size,
                    Brand = p.Brand,
                    Categorys = p.Categories
                }).ToList();
            return listProduct;
        }

        public MessageResponse CreateProduct(CreateProduct request)
        {
            var listCategory = new List<Category>();
            request.CategoryIDs.ForEach(id =>
            {
                var targetCategory = _context.Categories
                    .FirstOrDefault(c => c.CategoryID == id);
                if (targetCategory == null)
                {
                    throw new Exception("not found this category");
                }
                listCategory.Add(targetCategory);
            });
            var newProduct = new Product()
            {
                ProductID = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ImageURL = request.ImageURL,
                QuantityAvailable = request.QuantityAvailable,
                Price = request.Price,
                Size = request.Size,
                Brand = request.Brand,
                Categories = listCategory
            };
            _context.Add(newProduct);
            _context.SaveChanges();

            return new MessageResponse()
            {
                Status = 200,
                Message = "Success"
            };
        }

        public ProductResponse EditProduct(EditProduct request)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}