using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoFixtureDemo.Core
{
    public class Order
    {
        public int OrderId { get; set; }
        public string DateCreated { get; set; }
        public string DateShipped { get; set; }
        public string CustomerId { get; set; }
        public string Status { get; set; }
        public string ShippingId { get; set; }
        
        public Address ShippingAddress { get; set; } = new Address();
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public void PlaceOrder()
        {
            // TODO: Implement PlaceOrder
        }

        public void AddItem(Product product, int quantity, decimal price)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero");
            }

            if (price <= 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero");
            }

            var orderDetail = OrderDetails.SingleOrDefault(e => string.Equals(e.Product.Name, product.Name, StringComparison.CurrentCultureIgnoreCase));
            if (orderDetail == null)
            {
                orderDetail = new OrderDetail {Product = product};
                OrderDetails.Add(orderDetail);
            }

            orderDetail.Quantity += quantity;
            orderDetail.UnitPrice = price;
        }

        public void ClearOrder()
        {
            OrderDetails.Clear();
        }

        public decimal OrderTotal
        {
            get
            {
                decimal total = 0;
                foreach (var orderDetail in OrderDetails)
                {
                    total += (orderDetail.Quantity * orderDetail.UnitPrice);
                }

                return total;
            }
        }

        public int OrderDetailCount => OrderDetails.Count;

        public Customer Customer { get; set; }
    }
}
