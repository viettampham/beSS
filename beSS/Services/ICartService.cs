using System;
using System.Collections.Generic;
using beSS.Models;
using beSS.Models.RequestModels;
using beSS.Models.ViewModels;

namespace beSS.Services
{
    public interface ICartService
    {
        CartResponse GetCartByUser(Guid id);
        List<Cart> GetList();
        MessageResponse CreateCart(Guid id);
        MessageResponse DeleteCart(Guid id);
        MessageResponse RemoveOrderInCart(RemoveOrderInCartRequest request);
    }
}