using System;
using BasketCore;
using BasketCore.Entities;
using BasketCore.Exceptions;
using BasketCore.Interfaces;
using Moq;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;

namespace Tests.BasketCore.Services
{
    public class BasketServiceTests
    {
        private int _invalidId = -1;
        private Mock<IAsyncRepository<Basket>> _mockBasketRepo;

        public BasketServiceTests()
        {
            _mockBasketRepo = new Mock<IAsyncRepository<Basket>>();
        }

        [Fact]
        public void SetQuantitiesThrowsGivenInvalidBasketId()
        {
            BasketService basketService = new BasketService(_mockBasketRepo.Object);
            Func<Task> setQuantities = async () => await basketService.SetQuantities(_invalidId, new System.Collections.Generic.Dictionary<string, int>());
            setQuantities.Should().Throw<EntityNotFoundException>();
        }

        [Fact]
        public void SetQuantitiesThrowsGivenNullQuantities()
        {
            BasketService basketService = new BasketService(null);
            Func<Task> setQuantities = async () => await basketService.SetQuantities(1, null);
            setQuantities.Should().Throw<ArgumentNullException>();
        }

    }

}
