using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Repository.Cart;
using Store.Repository.Interfaces;
using Store.Services.Services.Cart.CartServices;
using Store.Services.Services.Cart.Dtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.Data.Entities.Product;

namespace Store.Services.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICartService cartService;

        public PaymentService(IConfiguration configuration, IUnitOfWork unitOfWork, ICartService cartService)
        {
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
            this.cartService = cartService;
        }
        public async Task<CartDto> CreateOrUpdatePaymentIntent(CartDto cartDto)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
            if (cartDto is null)
                throw new Exception("Cart is null");
            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod, int>().GetById(cartDto.DeliveryMethodId.Value);

            double shippingPrice = deliveryMethod.Price;

            // check that the incoming prices equals the real prices in db
            foreach (var item in cartDto.cartItems)
            {
                var product = await unitOfWork.Repository<Product, int>().GetById(item.ProductId);
                if (product.Price != item.Price)
                {
                    item.Price = product.Price;
                }
            }
            var service = new PaymentIntentService();

            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(cartDto.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(cartDto.cartItems.Sum(i => i.Quantity * (i.Price * 100)) + (shippingPrice * 100)),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var intent = await service.CreateAsync(options);
                paymentIntent = await service.GetAsync(intent.Id);
                cartDto.PaymentIntentId = paymentIntent.Id;
                cartDto.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)(cartDto.cartItems.Sum(i => i.Quantity * (i.Price * 100)) + (shippingPrice * 100)),
                };
                await service.UpdateAsync(cartDto.PaymentIntentId, options);
            }

            await cartService.UpdateAsync(cartDto);

            return cartDto;
        }

        public Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            throw new NotImplementedException();
        }
    }
}
