﻿using System;
using BasketApi.Infrastructure.Entities;
using BasketApi.Web.Exceptions;
using BasketApi.Infrastructure.Interfaces;
using Moq;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using BasketApi.Web.Services;

namespace BasketApi.Web.Tests
{
    public class BasketServiceTests
    {
        private int _invalidId = -1;
        private Mock<IBasketRepository> _mockBasketRepo;

        public BasketServiceTests()
        {
            _mockBasketRepo = new Mock<IBasketRepository>();
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
            BasketService basketService = new BasketService(_mockBasketRepo.Object);
            Func<Task> setQuantities = async () => await basketService.SetQuantities(1, null);
            setQuantities.Should().Throw<ArgumentNullException>();
        }

    }

}
