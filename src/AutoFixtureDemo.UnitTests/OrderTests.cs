using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoFixtureDemo.Core;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace AutoFixtureDemo.UnitTests
{
    [ExcludeFromCodeCoverage]
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
            var fixture = new Fixture();
            var order = fixture.Create<Order>();

            foreach (var orderDetail in order.OrderDetails)
            {
                _testOutputHelper.WriteLine("Product Name: {0}\tQuantity: {1}\tUnit Price: {2}",
                    orderDetail.Product.Name,
                    orderDetail.Quantity, orderDetail.UnitPrice);
            }

            order.ClearOrder();
            order.OrderDetails.Should().HaveCount(0);
            order.OrderTotal.Should().Be(0);
        }

        [Theory, AutoData]
        public void AddItem_WithNewItem_IncreasesOrderDetailCountByOne(Order order)
        {
            var currentItemCount = order.OrderDetailCount;
            var newProduct = new Product {Name = "Demo Product 1", Description = "Demo Product 1", ProductId = 999};
            order.AddItem(newProduct, 2, 150m);
            
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
            var currentQuantity = orderDetail.Quantity;
            
            order.AddItem(product, 2, 150m);
            
            order.OrderDetailCount.Should().Be(currentItemCount);
            order.OrderTotal.Should().BeGreaterThan(currentTotal);
        }

    }
}