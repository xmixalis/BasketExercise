using System.Collections.Generic;
using BasketApi.Models;
using Xunit;
using FluentAssertions;
using System.Linq;
using System;
using System.Threading.Tasks;
using Xunit.Abstractions;
using System.Diagnostics;

namespace BasketApi.Client.Tests
{
    /// <summary>
    /// Basket service tests class
    /// </summary>
    public class BasketTests
    {
        //AWS
        BasketApiClient client = new BasketApiClient("http://basketapiweb-prod.us-west-2.elasticbeanstalk.com");
        //Azure
        //BasketApiClient client = new BasketApiClient("http://panchbasketapi-live.azurewebsites.net");

        private readonly ITestOutputHelper _output;
        public BasketTests(ITestOutputHelper output)
        {
            _output = output;
        }

        async Task<List<BasketRemoveItemResponse>> removeAllBasketItems(BasketModelResponse basketResponse)
        {
            List<BasketRemoveItemResponse> ret = new List<BasketRemoveItemResponse>();
            foreach (var b in basketResponse.Items)
            {
                BasketRemoveItemResponse r = await client.BasketService.RemoveBasketItem(basketResponse.BasketId, new BasketRemoveItemRequest() { ProductId = b.ProductId });
                ret.Add(r);
            }

            return ret;
        }

        async Task<BasketResponseBase> clearBasket(BasketModelResponse basketResponse)
        {
            return await client.BasketService.ClearBasketItem(basketResponse.BasketId);
        }
        List<BasketModelResponse> GetAllBaskets(HashSet<string> users)
        {
            List<BasketModelResponse> baskets = new List<BasketModelResponse>();
            users.AsParallel().ForAll(user =>
            {
                BasketModelResponse basketResponse = client.BasketService.GetBasketForUser(user.ToString()).Result;
                lock (baskets) { baskets.Add(basketResponse); }
            });
            return baskets;
        }

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
        public async void BasketItemsAreCleared()
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

            BasketResponseBase respClear =
                await client.BasketService.ClearBasketItem(response.BasketId);

            response = await client.BasketService.GetBasketForUser(userId);
            List<BasketModelItem> items = response.Items.ToList();
            items.Count.Should().Be(0);
        }

        [Fact]
        public async void BasketCreateAndAddAllItems()
        {
            string userId = "demouser@panch.com";

            List<ProductModelResponse> productsResponse = await client.ProductService.GetProductsAsync();
            BasketModelResponse basketResponse = await client.BasketService.GetBasketForUser(userId);

            await clearBasket(basketResponse);

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

        [Fact]
        public async void BasketLoadTest()
        {
            Stopwatch sw = new Stopwatch();

            HashSet<string> users = new HashSet<string>();
            //Generate users for test
            for (int i= 1; i <= 10; i++)
            {
                users.Add($"User{i}");
            }

            //Get all products
            sw.Start();
            List<ProductModelResponse> productsResponse = await client.ProductService.GetProductsAsync();
            sw.Stop();
            _output.WriteLine($"Get all products: {sw.Elapsed}");

            //Get all baskets for all users
            sw.Start();
            List<BasketModelResponse> baskets = GetAllBaskets(users);
            sw.Stop();
            _output.WriteLine($"Get all baskets 1: {sw.Elapsed}");
            baskets.Count().Should().Be(10);

            //Clear basket Items
            sw.Start();
            baskets.AsParallel().ForAll(b =>
            {
                BasketResponseBase r = clearBasket(b).Result;
            });
            sw.Stop();
            _output.WriteLine($"Clear basket items: {sw.Elapsed}");

            //Retrieve and confirm clearance
            sw.Start();
            baskets = GetAllBaskets(users);
            sw.Stop();
            _output.WriteLine($"Get all baskets 2: {sw.Elapsed}");
            baskets.AsParallel().ForAll(basket =>
            {
                basket.Items.Count().Should().Be(0);
            });

            //Add all products to all baskets 
            sw.Start();
            baskets.AsParallel().ForAll(basket=>
            {
                foreach (ProductModelResponse i in productsResponse)
                {
                    BasketAddItemResponse resp = client.BasketService.AddBasketItem(basket.BasketId, new BasketAddItemRequest()
                    {
                        Price = i.Price,
                        ProductId = i.ProductId,
                        Quantity = 1
                    }).Result;
                };
            });
            sw.Stop();
            _output.WriteLine($"Add all products: {sw.Elapsed}");

            //Confirm products addition
            sw.Start();
            baskets = GetAllBaskets(users);
            sw.Stop();
            _output.WriteLine($"Get all baskets 3: {sw.Elapsed}");

            baskets.Count().Should().Be(10);
            baskets.AsParallel().ForAll(basket =>
            {
                basket.Items.Count().Should().Be(productsResponse.Count());
            });
        }
    }
}
