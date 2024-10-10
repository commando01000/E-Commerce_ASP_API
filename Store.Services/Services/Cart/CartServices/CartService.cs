using Microsoft.IdentityModel.Tokens;
using Store.Repository.Cart;
using Store.Repository.Cart.Interfaces;
using Store.Services.Services.Cart.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services.Cart.CartServices
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _cartRepository.DeleteAsync(id);
        }

        public async Task<CartDto> GetAsync(Guid id)
        {
            var cart = await _cartRepository.GetAsync(id);

            if (cart == null)
            {
                return new CartDto();
            }

            var mappedCart = new CartDto
            {
                id = cart.id,
                shippingCost = cart.shippingCost.Value,
                cartItems = cart.cartItems
            };
            return mappedCart;
        }

        public async Task<CartDto> UpdateAsync(CartDto cart)
        {
            if (cart.id is null)
                cart.id = GenerateRandomCartId();

            var mappedCart = new CustomerCart
            {
                id = cart.id,
                shippingCost = cart.shippingCost,
                cartItems = cart.cartItems
            };

            var updatedCart = await _cartRepository.UpdateAsync(mappedCart);

            var mappedUpdatedCart = new CartDto
            {
                id = updatedCart.id,
                shippingCost = updatedCart.shippingCost.Value,
                cartItems = updatedCart.cartItems
            };

            return mappedUpdatedCart;
        }
        private string GenerateRandomCartId()
        {
            return $"{Guid.NewGuid().ToString()}";
        }
    }
}
