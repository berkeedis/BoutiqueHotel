using System;
using System.Collections.Generic;

namespace BoutiqueHotel.entity
{
    public class Order
    {
        public int Id { get; set; }
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
        public List<OrderItem> OrderItems { get; set; }
    }
}
