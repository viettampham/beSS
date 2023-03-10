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

        public List<string> Brand()
        {
            var listProduct = _context.Products
                .Select(p => p);
            var listBrand = new List<string>();
            foreach (var p in listProduct)
            {
                listBrand.Add(p.Brand);
            }
            for (var i = 0; i < listBrand.Count-1; i++)
            {
                for (var j = i; j < listBrand.Count; j++)
                {
                    if (listBrand[i].ToLower() == listBrand[j].ToLower())
                    {
                        listBrand.Remove(listBrand[j]);
                    }
                }
            }
            return listBrand;
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
                    DisplayPrice = p.Price.ToString("#,## VNĐ"),
                    Size = p.Size,
                    Brand = p.Brand,
                    Categorys = p.Categories
                }).ToList();
            return listProduct;
        }

        public List<ProductResponse> GetProductByBrand(string brand)
        {
            var listProduct = _context.Products
                .Where(p => p.Brand.ToLower() == brand.ToLower())
                .Select(p => new ProductResponse()
                {
                    ProductID = p.ProductID,
                    Name = p.Name,
                    Description = p.Description,
                    ImageURL = p.ImageURL,
                    QuantityAvailable = p.QuantityAvailable,
                    Price = p.Price,
                    DisplayPrice = p.Price.ToString("#,## VNĐ"),
                    Size = p.Size,
                    Brand = p.Brand,
                    Categorys = p.Categories
                }).ToList();
            return listProduct;
        }

        public List<ProductResponse> GetProductByCategoryID(Guid id)
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
                DisplayPrice = p.Price.ToString("#,## VNĐ"),
                Size = p.Size,
                Brand = p.Brand,
                Categorys = p.Categories
            }).ToList();
            var listProductResponse = new List<ProductResponse>();
            foreach (var productResponse in listProduct)
            {
                foreach (var category in productResponse.Categorys)
                {
                    if (category.CategoryID == id)
                    {
                        listProductResponse.Add(productResponse);
                    }
                }
            }
            return listProductResponse;
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

        public MessageResponse EditProduct(EditProduct request)
        {
            var targetProduct = _context.Products.FirstOrDefault(p => p.ProductID == request.ProductID);
            if (targetProduct == null)
            {
                return new MessageResponse()
                {
                    Status = 401,
                    Message = "Not found this product"
                };
            }
            
            //targetProduct.Categories.Clear();

            var categorys = new List<Category>();
            foreach (var id in request.CategorieIDs)
            {
                var targetCategory = _context.Categories.FirstOrDefault(c => c.CategoryID == id);
                if (targetCategory == null)
                {
                    return new MessageResponse()
                    {
                        Status = 401,
                        Message = "Not found this category"
                    };
                }

                if (targetCategory != null)
                {
                    categorys.Add(targetCategory);
                }
                else
                {
                    categorys = new List<Category>() { targetCategory };
                }
            }
            
            targetProduct.Name = request.Name;
            targetProduct.Description = request.Description;
            targetProduct.ImageURL = request.ImageURL;
            targetProduct.QuantityAvailable = request.QuantityAvailable;
            targetProduct.Price = request.Price;
            targetProduct.Size = request.Size;
            targetProduct.Brand = request.Brand;
            targetProduct.Categories = categorys;
            _context.SaveChanges();
            return new MessageResponse()
            {
                Status = 200,
                Message = "Update success"
            };
        }

        public MessageResponse DeleteProduct(Guid id)
        {
            var targetProduct = _context.Products.FirstOrDefault(p => p.ProductID == id);
            _context.Remove(targetProduct);
            _context.SaveChanges();
            if (targetProduct == null)
            {
                return new MessageResponse()
                {
                    Status = 404,
                    Message = "Not found thí product in database"
                };
            }
            return new MessageResponse()
            {
                Status = 200,
                Message = "Xóa thành công"
            };
        }

        public List<ProductResponse> SearchProduct(string request)
        {
            var listProduct = _context.Products
                .Where(p => p.Brand.ToLower() == request.ToLower() || p.Name.ToLower().Contains(request.ToLower()))
                .Select(p => new ProductResponse()
                {
                    ProductID = p.ProductID,
                    Name = p.Name,
                    Description = p.Description,
                    ImageURL = p.ImageURL,
                    QuantityAvailable = p.QuantityAvailable,
                    Price = p.Price,
                    DisplayPrice = p.Price.ToString("#,## VND"),
                    Size = p.Size,
                    Brand = p.Brand,
                    Categorys = p.Categories
                }).ToList();
            return listProduct;
        }
    }
}