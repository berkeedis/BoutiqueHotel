using Microsoft.AspNetCore.Mvc;
using BoutiqueHotel.business.Abstract;
using BoutiqueHotel.webUI.Models;

namespace BoutiqueHotel.webUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        public HomeController(IProductService productService)
        {
            this._productService = productService;
        }
        public IActionResult Index()
        {
            var productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetHomePageProducts()
            };
            return View(productViewModel);
        }

    }
}