using System.Collections.Generic;
using BoutiqueHotel.business.Abstract;
using BoutiqueHotel.data.Abstract;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Order entity)
        {
            _unitOfWork.Orders.Create(entity);
            _unitOfWork.Save();
        }

        public List<Order> GetUserOrders(string userId)
        {
            return _unitOfWork.Orders.GetUserOrders(userId);
        }
    }
}