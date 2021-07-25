using BoutiqueHotel.business.Abstract;
using BoutiqueHotel.data.Abstract;
using BoutiqueHotel.entity;

namespace BoutiqueHotel.business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddToCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);

            if (cart != null)
            {
                var index = cart.CartItems.FindIndex(i => i.ProductId == productId);
                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Qantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Qantity += quantity;
                }
                _unitOfWork.Carts.Update(cart);
                _unitOfWork.Save();
            }
        }

        public void ClearCart(int cartId)
        {
            _unitOfWork.Carts.ClearCart(cartId);
        }

        public void DeleteFromCart(string userId, int productId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                _unitOfWork.Carts.DeleteFromCart(cart.Id, productId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _unitOfWork.Carts.GetByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
            _unitOfWork.Carts.Create(new Cart() { UserId = userId });
            _unitOfWork.Save();
        }
    }
}