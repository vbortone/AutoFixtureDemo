﻿namespace AutoFixtureDemo.Core
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Product Product { get; set; }
    }
}