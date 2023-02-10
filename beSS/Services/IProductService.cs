using System;
using System.Collections.Generic;
using beSS.Models;
using beSS.Models.RequestModels;
using beSS.Models.ViewModels;

namespace beSS.Services
{
    public interface IProductService
    {
        List<ProductResponse> GetProduct();
        MessageResponse CreateProduct(CreateProduct request);
        ProductResponse EditProduct(EditProduct request);
        bool DeleteProduct(Guid id);
    }
}