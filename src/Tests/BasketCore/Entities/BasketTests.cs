using System.Linq;
using BasketCore.Entities;
using FluentAssertions;
using Xunit;

namespace Tests.BasketCore.Entities
{
    public class BasketTests
    {
        private int _productItemId = 1;
        private decimal _testUnitPrice = 10;
        private int _testQuantity = 2;

        [Fact]
        public void AddsBasketItemIfNotPresent()
        {
            Basket basket = new Basket();
            basket.AddItem(_productItemId, _testUnitPrice, _testQuantity);

            BasketItem firstItem = basket.Items.Single();
            firstItem.ProductItemId.Should().Be(_productItemId);
            firstItem.UnitPrice.Should().Be(_testUnitPrice);
            firstItem.Quantity.Should().Be(_testQuantity);
        }

        [Fact]
        public void IncrementsQuantityOfItemIfPresent()
        {
            Basket basket = new Basket();
            basket.AddItem(_productItemId, _testUnitPrice, _testQuantity);
            basket.AddItem(_productItemId, _testUnitPrice, _testQuantity);

            BasketItem firstItem = basket.Items.Single();
            firstItem.Quantity.Should().Be(_testQuantity * 2);
        }

        [Fact]
        public void KeepsOriginalUnitPriceIfMoreItemsAdded()
        {
            Basket basket = new Basket();
            basket.AddItem(_productItemId, _testUnitPrice, _testQuantity);
            basket.AddItem(_productItemId, _testUnitPrice * 2, _testQuantity);

            BasketItem firstItem = basket.Items.Single();
            firstItem.UnitPrice.Should().Be(_testUnitPrice);
        }

        [Fact]
        public void DefaultsToQuantityOfOne()
        {
            Basket basket = new Basket();
            basket.AddItem(_productItemId, _testUnitPrice);

            BasketItem firstItem = basket.Items.Single();
            firstItem.Quantity.Should().Be(1);
        }
    }

}
