using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.webUI.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "You must enter category name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must enter category URL")]
        public string Url { get; set; }

        public List<Product> Products { get; set; }
    }
}