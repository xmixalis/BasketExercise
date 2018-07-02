using System.Collections.Generic;
using BasketApi.Models;
using Xunit;
using FluentAssertions;
using System.Linq;
using System;

namespace BasketApi.Client.Tests
{
    /// <summary>
    /// Products service tests class
    /// </summary>
    public class ProductTests
    {
        BasketApiClient client = new BasketApiClient("http://basketapiweb-prod.us-west-2.elasticbeanstalk.com");

        [Fact]
        public async void SampleProductsAreListed()
        {
            List<ProductModelResponse> response = await client.ProductService.GetProductsAsync();

            response.Should().NotBeNull();
            response.Count.Should().BeGreaterThan(0);
        }
    }
}
