using System.Linq;
using BasketApi.Infrastructure.Entities;
using BasketApi.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using System;

namespace BasketApi.Infrastructure.Tests
{
    /// <summary>
    /// Database fixture for the database tests provided
    /// </summary>
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            DbContextOptions<BasketDbContext> dbOptions = new DbContextOptionsBuilder<BasketDbContext>()
                .UseInMemoryDatabase(databaseName: "TestBasket")
                .Options;
            basketDbContext = new BasketDbContext(dbOptions);
            basketRepository = new BasketRepository(basketDbContext);

            //setup some data
            Basket existingBasket = new Basket() { Id = TestBasketId };
            existingBasket.AddItem(TestItemId, 10, 2);

            basketDbContext.Baskets.Add(existingBasket);
            basketDbContext.SaveChanges();
        }

        public void Dispose()
        {
            // clean up test data from the database
            // if db is not in memory
        }

        public int TestBasketId => 1; 
        public int TestItemId => 10;

        public BasketDbContext basketDbContext { get; private set; }
        public BasketRepository basketRepository { get; private set; }
    }

    /// <summary>
    /// Database tests class
    /// </summary>
    public class DatabaseTests : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture fixture;
        private readonly ITestOutputHelper _output;

        public DatabaseTests(ITestOutputHelper output, DatabaseFixture fixture)
        {
            _output = output;
            this.fixture = fixture;
        }

        [Fact]
        public async void GetExistingBasket()
        {
            _output.WriteLine($"BasketId: {fixture.TestBasketId}");

            Basket basketFromRepo = await fixture.basketRepository.GetByIdAsync(fixture.TestBasketId);
            basketFromRepo.Should().NotBeNull();
            basketFromRepo.Id.Should().Be(fixture.TestBasketId);
        }

        [Fact]
        public async void GetExistingBasketItem()
        {
            _output.WriteLine($"BasketId: {fixture.TestBasketId}");
            _output.WriteLine($"BasketItemId: {fixture.TestItemId}");

            Basket basketFromRepo = await fixture.basketRepository.GetByIdAsync(fixture.TestBasketId);
            BasketItem firstItem = basketFromRepo.Items.FirstOrDefault();

            firstItem.Should().NotBeNull();
            firstItem.ProductItemId.Should().Be(fixture.TestItemId);
        }
    }
}
