using System.Collections.Generic;
using System.Linq;
using BoutiqueHotel.data.Abstract;
using BoutiqueHotel.entity;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueHotel.data.Concrete.EfCore
{
    public class EfCoreOrderRepository : EfCoreGenericRepository<Order>, IOrderRepository
    {
        public EfCoreOrderRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }
        public List<Order> GetUserOrders(string userId)
        {
            var orders = ShopContext.Orders
                                .Include(i => i.OrderItems)
                                .ThenInclude(i => i.Product)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                orders = orders.Where(i => i.UserId == userId);
            }

            return orders.ToList();

        }
    }
}