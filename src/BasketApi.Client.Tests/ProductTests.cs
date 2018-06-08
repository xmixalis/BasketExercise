using System.Collections.Generic;
using BasketApi.Models;
using Xunit;
using FluentAssertions;
using System.Linq;
using System;

namespace BasketApi.Client.Tests
{
    public class ProductTests
    {
        BasketApiClient client = new BasketApiClient("http://localhost:54000");

        [Fact]
        public async void SampleProductsAreListed()
        {
            List<ProductModelResponse> response = await client.ProductService.GetProductsAsync();

            response.Should().NotBeNull();
            response.Count.Should().BeGreaterThan(0);
        }
    }
}
