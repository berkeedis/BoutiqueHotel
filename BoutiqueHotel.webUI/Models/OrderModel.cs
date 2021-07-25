using System.ComponentModel.DataAnnotations;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.webUI.Models
{
    public class OrderModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string RoomNumber { get; set; }
        [Required]
        public string TableNumber { get; set; }
        public string OrderNote { get; set; }
        public string PaymentType { get; set; }
        public string OrderStatus { get; set; }
        public CartModel CartModel { get; set; }
    }
}