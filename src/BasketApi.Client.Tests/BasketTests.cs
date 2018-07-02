using System.Collections.Generic;
using BasketApi.Models;
using Xunit;
using FluentAssertions;
using System.Linq;
using System;

namespace BasketApi.Client.Tests
{
    /// <summary>
    /// Basket service tests class
    /// </summary>
    public class BasketTests
    {
        BasketApiClient client = new BasketApiClient("http://basketapiweb-prod.us-west-2.elasticbeanstalk.com");

        [Fact]
        public async void BasketIsCreatedAndRetrievedForUser()
        {
            string userId = Guid.NewGuid().ToString();
            BasketModelResponse response = await client.BasketService.GetBasketForUser(userId);

            response.Should().NotBeNull();
            response.BasketId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async void BasketItemIsAddedToBasket()
        {
            string userId = Guid.NewGuid().ToString();
            BasketModelResponse response = await client.BasketService.GetBasketForUser(userId);

            BasketAddItemResponse resp = await client.BasketService.AddBasketItem(response.BasketId, new BasketAddItemRequest()
            {
                Price = 10.50M,
                ProductId = 3,
                Quantity = 2
            });

            resp.Should().NotBeNull();
            resp.Success.Should().Be(true);

            response = await client.BasketService.GetBasketForUser(userId);
            List<BasketModelItem> items = response.Items.ToList();
            items.Count.Should().Be(1);
            items.Select(i => i.Price).First().Should().Be(10.50M);
        }

        [Fact]
        public async void BasketItemQuantityIsUpdatedWhenSameProductIsAddedToBasket()
        {
            string userId = Guid.NewGuid().ToString();
            BasketModelResponse response = await client.BasketService.GetBasketForUser(userId);

            BasketAddItemResponse resp = await client.BasketService.AddBasketItem(response.BasketId, new BasketAddItemRequest()
            {
                Price = 10.50M,
                ProductId = 3,
                Quantity = 2
            });

            resp = await client.BasketService.AddBasketItem(response.BasketId, new BasketAddItemRequest()
            {
                Price = 10.50M,
                ProductId = 3,
                Quantity = 1
            });

            resp.Should().NotBeNull();
            resp.Success.Should().Be(true);

            response = await client.BasketService.GetBasketForUser(userId);
            List<BasketModelItem> items = response.Items.ToList();
            items.Count.Should().Be(1);
            items.Sum(i=>i.Quantity).Should().Be(3);
        }

        [Fact]
        public async void BasketItemIsAddedWhenOtherProductIsAddedToBasket()
        {
            string userId = Guid.NewGuid().ToString();
            BasketModelResponse response = await client.BasketService.GetBasketForUser(userId);

            BasketAddItemResponse resp = await client.BasketService.AddBasketItem(response.BasketId, new BasketAddItemRequest()
            {
                Price = 10.50M,
                ProductId = 1,
                Quantity = 2
            });

            resp = await client.BasketService.AddBasketItem(response.BasketId, new BasketAddItemRequest()
            {
                Price = 20.00M,
                ProductId = 3,
                Quantity = 1
            });

            resp.Should().NotBeNull();
            resp.Success.Should().Be(true);

            response = await client.BasketService.GetBasketForUser(userId);
            List<BasketModelItem> items = response.Items.ToList();
            items.Count.Should().Be(2);
        }

        [Fact]
        public async void BasketItemIsUpdated()
        {
            string userId = Guid.NewGuid().ToString();
            BasketModelResponse response = await client.BasketService.GetBasketForUser(userId);

            BasketAddItemResponse respAdd = await client.BasketService.AddBasketItem(response.BasketId, new BasketAddItemRequest()
            {
                Price = 10.50M,
                ProductId = 1,
                Quantity = 2
            });
            respAdd.Should().NotBeNull();
            respAdd.Success.Should().Be(true);

            List<BasketUpdateItem> updateItems = new List<BasketUpdateItem>();
            updateItems.Add(new BasketUpdateItem() { ProductId = 1, Quantity = 1 });
            BasketUpdateResponse respUpd = await client.BasketService.UpdateBasketItem(
                response.BasketId,
                new BasketUpdateItemsRequest()
                {
                    Items = updateItems
                });

            respUpd.Should().NotBeNull();
            respUpd.Success.Should().Be(true);

            response = await client.BasketService.GetBasketForUser(userId);
            List<BasketModelItem> items = response.Items.ToList();

            items.Count.Should().Be(1);
            items.First().Quantity.Should().Be(1);
        }

        [Fact]
        public async void BasketItemIsRemoved()
        {
            string userId = Guid.NewGuid().ToString();
            BasketModelResponse response = await client.BasketService.GetBasketForUser(userId);

            BasketAddItemResponse resp = await client.BasketService.AddBasketItem(response.BasketId, new BasketAddItemRequest()
            {
                Price = 10.50M,
                ProductId = 1,
                Quantity = 2
            });

            resp = await client.BasketService.AddBasketItem(response.BasketId, new BasketAddItemRequest()
            {
                Price = 20.00M,
                ProductId = 3,
                Quantity = 1
            });

            resp.Should().NotBeNull();
            resp.Success.Should().Be(true);

            BasketRemoveItemResponse respDel =
                await client.BasketService.RemoveBasketItem(response.BasketId, new BasketRemoveItemRequest()
                {
                    ProductId = 1
                });

            response = await client.BasketService.GetBasketForUser(userId);
            List<BasketModelItem> items = response.Items.ToList();
            items.Count.Should().Be(1);

            items.First().ProductId.Should().Be(3);
        }

        [Fact]
        public async void BasketCreateAndAddAllItems()
        {
            string userId = "demouser@panch.com";

            List<ProductModelResponse> productsResponse = await client.ProductService.GetProductsAsync();
            BasketModelResponse basketResponse = await client.BasketService.GetBasketForUser(userId);
            foreach(var b in basketResponse.Items)
            {
                await client.BasketService.RemoveBasketItem(basketResponse.BasketId, new BasketRemoveItemRequest() { ProductId = b.ProductId });
            }

            foreach (ProductModelResponse i in  productsResponse)
            {
                BasketAddItemResponse resp = await client.BasketService.AddBasketItem(basketResponse.BasketId, new BasketAddItemRequest()
                {
                    Price = i.Price,
                    ProductId = i.ProductId,
                    Quantity = 1
                });
            };

            basketResponse = await client.BasketService.GetBasketForUser(userId);
            basketResponse.Items.Count().Should().Be(productsResponse.Count());
        }
    }
}
