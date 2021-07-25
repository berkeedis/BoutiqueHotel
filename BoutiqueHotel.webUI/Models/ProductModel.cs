using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.webUI.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "You must enter a product name")]
        public string Name { get; set; }
        [Required]
        [Range(0, 100000, ErrorMessage = "You must enter a correct price")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "You must enter a product content")]
        public string Content { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "You must enter a product URL")]
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}