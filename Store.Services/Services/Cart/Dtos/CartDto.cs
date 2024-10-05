using Store.Repository.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services.Cart.Dtos
{
    public class CartDto
    {
        public string? id { get; set; }
        public double shippingCost { get; set; }
        public List<CartItem> cartItems { get; set; } = new List<CartItem>();
    }
}
