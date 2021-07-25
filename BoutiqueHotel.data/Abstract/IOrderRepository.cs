using System.Collections.Generic;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.data.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetUserOrders(string userId);
    }
}