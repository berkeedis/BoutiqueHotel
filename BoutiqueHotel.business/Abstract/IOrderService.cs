using System.Collections.Generic;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.business.Abstract
{
    public interface IOrderService
    {
        void Create(Order entity);
        List<Order> GetUserOrders(string userId);
    }
}