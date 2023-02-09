using System;
using System.Collections.Generic;
using beSS.Models;
using beSS.Models.RequestModels;

namespace beSS.Services
{
    public interface ICategoryService
    {
        List<Category> GetCategory();
        Category CreateCategory(CreateCategory request);
        Category EditCategory(EditCategory request);
        bool DeleteCategory(Guid id);
    }
}