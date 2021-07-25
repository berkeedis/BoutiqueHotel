using System.Collections.Generic;
using System.Linq;
using BoutiqueHotel.business.Abstract;
using BoutiqueHotel.entity;
using BoutiqueHotel.webUI.Identity;
using BoutiqueHotel.webUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueHotel.webUI.Controllers
{
    public class OrderController : Controller
    {
        private IProductService _productService;
        private IOrderService _orderService;
        private UserManager<User> _userManager;

        public OrderController(IProductService productService, IOrderService orderService, UserManager<User> userManager)
        {
            _productService = productService;
            _orderService = orderService;
            _userManager = userManager;
        }
        public IActionResult List(string category, int page = 1)
        {
            const int pageSize = 8;
            var productViewModel = new ProductListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _productService.GetCountByCategory(category),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    CurrentCategory = category
                },
                Products = _productService.GetProductsByCategory(category, page, pageSize)
            };
            return View(productViewModel);
        }

        public IActionResult Details(string url)
        {
            if (url == null)
            {
                return NotFound();
            }
            Product product = _productService.GetProductDetails(url);
            if (product == null)
            {
                return NotFound();
            }
            return View(new ProductDetailModel
            {
                Product = product,
                Categories = product.ProductCategories.Select(i => i.Category).ToList()
            });
        }

        public IActionResult Search(string q)
        {
            var productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetSearchResult(q)
            };
            return View(productViewModel);
        }

        public IActionResult GetUserOrders()
        {
            var userId = _userManager.GetUserId(User);
            var orders = _orderService.GetUserOrders(userId);

            var orderListModel = new List<OrderListModel>();
            OrderListModel orderModel;

            foreach (var order in orders)
            {
                orderModel = new OrderListModel();

                orderModel.OrderId = order.Id;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.OrderDate = order.OrderDate;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.RoomNumber = order.RoomNumber;
                orderModel.TableNumber = order.TableNumber;
                orderModel.OrderNote = order.OrderNote;
                orderModel.PaymentType = order.PaymentType;
                orderModel.OrderStatus = order.OrderStatus;

                orderModel.OrderItems = order.OrderItems.Select(i => new OrderItemModel()
                {
                    OrderItemId = i.Id,
                    Name = i.Product.Name,
                    Content = i.Product.Content,
                    Price = (double)i.Price,
                    Qantity = i.Qantity,
                    ImageUrl = i.Product.ImageUrl
                }).ToList();

                orderListModel.Add(orderModel);
            }

            return View("UserOrders", orderListModel);
        }

        public IActionResult GetAllUserOrders()
        {
            var orders = _orderService.GetUserOrders(null);

            var orderListModel = new List<OrderListModel>();
            OrderListModel orderModel;

            foreach (var order in orders)
            {
                orderModel = new OrderListModel();

                orderModel.OrderId = order.Id;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.OrderDate = order.OrderDate;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.RoomNumber = order.RoomNumber;
                orderModel.TableNumber = order.TableNumber;
                orderModel.OrderNote = order.OrderNote;
                orderModel.PaymentType = order.PaymentType;
                orderModel.OrderStatus = order.OrderStatus;

                orderModel.OrderItems = order.OrderItems.Select(i => new OrderItemModel()
                {
                    OrderItemId = i.Id,
                    Name = i.Product.Name,
                    Content = i.Product.Content,
                    Price = (double)i.Price,
                    Qantity = i.Qantity,
                    ImageUrl = i.Product.ImageUrl
                }).ToList();

                orderListModel.Add(orderModel);
            }

            return View("UserOrders", orderListModel);
        }

    }
}