using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Services.Services.Cart.Dtos;
using Store.Services.Services.Payment;

namespace Store.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        
        [HttpPost]
        public async Task<ActionResult<CartDto>> CreateOrUpdatePaymentIntent(CartDto cart)
        {
            return Ok(await paymentService.CreateOrUpdatePaymentIntent(cart));
        }

    }
}
