using System.Collections.Generic;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.webUI.Models
{
    public class ProductDetailModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}