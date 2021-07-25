using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.webUI.Models
{
    public class OrderListModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TableNumber { get; set; }
        public string RoomNumber { get; set; }
        public string OrderNote { get; set; }
        public string PaymentType { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }

        public double TotalPrice()
        {
            return OrderItems.Sum(i => i.Price * i.Qantity);
        }
    }

    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Qantity { get; set; }
        public string Content { get; set; }
    }
}