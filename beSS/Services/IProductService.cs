using System;
using System.Collections.Generic;
using beSS.Models;
using beSS.Models.RequestModels;
using beSS.Models.ViewModels;

namespace beSS.Services
{
    public interface IProductService
    {
        List<string> Brand();
        List<ProductResponse> GetProduct();
        List<ProductResponse> GetProductByBrand(string brand);
        List<ProductResponse> GetProductByCategoryID(Guid id);
        MessageResponse CreateProduct(CreateProduct request);
        ProductResponse EditProduct(EditProduct request);
        bool DeleteProduct(Guid id);
    }
}