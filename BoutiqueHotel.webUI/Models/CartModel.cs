using System.Collections.Generic;
using System.Linq;

namespace BoutiqueHotel.webUI.Models
{
    public class CartModel
    {
        public int CartId { get; set; }
        public List<CartItemModel> CartItems { get; set; }
        public double CartTotalPrice()
        {
            return CartItems.Sum(i => i.Price * i.Qantity);
        }

    }

    public class CartItemModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int Qantity { get; set; }

    }
}