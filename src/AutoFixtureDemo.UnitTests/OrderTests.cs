using System.Linq;
using AutoFixtureDemo.Core;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using AutoFixture;
using AutoFixture.Xunit2;

namespace AutoFixtureDemo.UnitTests
{
    public class OrderTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public OrderTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ClearOrder_WhenContainsOrderDetails_RemovesAllItems()
        {
            // Arrange
            var product1 = new Product
            {
                ProductId = 1,
                Name = "Product 1",
                Description = "Product 1"
            };
            
            var product2 = new Product
            {
                ProductId = 2,
                Name = "Product 2",
                Description = "Product 2"
            };

            var order = new Order();
            var orderDetail1 = new OrderDetail()
                {OrderDetailId = 1, Product = product1, ProductId = 1, Quantity = 4, UnitPrice = 20m};
            var orderDetail2 = new OrderDetail()
                {OrderDetailId = 2, Product = product2, ProductId = 2, Quantity = 10, UnitPrice = 5m};

            order.OrderDetails.Add(orderDetail1);
            order.OrderDetails.Add(orderDetail2);

            // Act
            order.ClearOrder();

            // Assert
            order.OrderDetails.Should().HaveCount(0);
            order.OrderTotal.Should().Be(0);
        }

        [Fact]
        public void AutoFixture_ClearOrder_WhenContainsOrderDetails_RemovesAllItems()
        {
            // Arrange
            var fixture = new Fixture();
            var order = fixture.Create<Order>();
            
            foreach (var orderDetail in order.OrderDetails)
            {
                _testOutputHelper.WriteLine("Product Name {0}\tQuantity: {1}", orderDetail.Product.Name, orderDetail.Quantity);
            }

            // Act
            order.ClearOrder();

            // Assert
            order.OrderDetails.Should().HaveCount(0);
            order.OrderTotal.Should().Be(0);
        }

        [Theory, AutoData]
        public void AddItem_WithNewItem_IncreasesOrderDetailCountByOne(Order order)
        {
            // Arrange
            var currentItemCount = order.OrderDetailCount;
            var newProduct = new Product {Name = "Demo Product 1", Description = "Demo Product 1", ProductId = 999};

            // Act
            order.AddItem(newProduct, 2, 150m);

            // Assert            
            order.OrderDetailCount.Should().Be(currentItemCount + 1);
            order.OrderDetailCount.Should().Be(order.OrderDetails.Count);
        }

        [Theory, AutoData]
        public void AddItem_WitExisting_IncreasesQuantityCountButNotItemCount(Order order)
        {
            var currentItemCount = order.OrderDetailCount;
            var currentTotal = order.OrderTotal;

            var orderDetail = order.OrderDetails.First();
            var product = orderDetail.Product;

            order.AddItem(product, 2, 150m);
            
            order.OrderDetailCount.Should().Be(currentItemCount);
            order.OrderTotal.Should().BeGreaterThan(currentTotal);
        }
    }
}