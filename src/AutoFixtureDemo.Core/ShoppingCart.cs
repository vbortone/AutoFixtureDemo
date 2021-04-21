using System;
using System.Collections.Generic;

namespace AutoFixtureDemo.Core
{
    public class ShoppingCart
    {
        private Dictionary<Product, int> _cartItems = new();

        public Guid ShoppingCardId { get; set; }

        public void AddCartItem(Product product, int itemCount)
        {
            // TODO: Implement AddCartItem
        }

        public void UpdateQuantity(Product product, int itemCount)
        {
            // TODO: Implement UpdateQuantity
        }

        public void CheckOut()
        {
            // TODO: Implement PlaceOrder
        }
    }
}