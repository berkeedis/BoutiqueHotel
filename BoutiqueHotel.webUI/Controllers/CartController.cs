using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueHotel.business.Abstract;
using BoutiqueHotel.entity;
using BoutiqueHotel.webUI.Identity;
using BoutiqueHotel.webUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueHotel.webUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private IOrderService _orderService;
        private UserManager<User> _userManager;
        public CartController(ICartService cartService, IOrderService orderService, UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));
            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = (double)i.Product.Price,
                    Content = i.Product.Content,
                    ImageUrl = i.Product.ImageUrl,
                    Qantity = i.Qantity
                }).ToList()
            });
        }
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.AddToCart(userId, productId, quantity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.DeleteFromCart(userId, productId);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));
            var orderModel = new OrderModel();

            orderModel.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product.Name,
                    Price = (double)i.Product.Price,
                    Content = i.Product.Content,
                    ImageUrl = i.Product.ImageUrl,
                    Qantity = i.Qantity
                }).ToList()
            };
            return View(orderModel);

        }
        [HttpPost]
        public IActionResult Checkout(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var cart = _cartService.GetCartByUserId(userId);

                model.CartModel = new CartModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(i => new CartItemModel()
                    {
                        CartItemId = i.Id,
                        ProductId = i.ProductId,
                        Name = i.Product.Name,
                        Price = (double)i.Product.Price,
                        Content = i.Product.Content,
                        ImageUrl = i.Product.ImageUrl,
                        Qantity = i.Qantity
                    }).ToList()
                };
                SaveOrder(model, userId);
                ClearCart(model.CartModel.CartId);
                return View("Success");
            }
            return View(model);
        }

        private void ClearCart(int cartId)
        {
            _cartService.ClearCart(cartId);
        }

        private void SaveOrder(OrderModel model, string userId)
        {
            var order = new Order();


            order.OrderNumber = new Random().Next(1111, 9999).ToString();
            order.OrderDate = new DateTime();
            order.UserId = userId;
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.RoomNumber = model.RoomNumber;
            order.TableNumber = model.TableNumber;
            order.OrderNote = model.OrderNote;
            order.PaymentType = model.PaymentType;
            order.OrderStatus = model.OrderStatus;

            order.OrderItems = new List<entity.OrderItem>();

            foreach (var item in model.CartModel.CartItems)
            {
                var orderItem = new OrderItem()
                {
                    Price = item.Price,
                    Qantity = item.Qantity,
                    ProductId = item.ProductId
                };
                order.OrderItems.Add(orderItem);
            }
            _orderService.Create(order);

        }
    }
}