using System.Linq;
using BasketCore.Entities;
using Infrastructure;
using Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

namespace Tests
{
    public class InfrastructureTests
    {
        private readonly BasketDbContext _basketDbContext;
        private readonly BasketRepository _basketRepository;
        private readonly ITestOutputHelper _output;
        public InfrastructureTests(ITestOutputHelper output)
        {
            _output = output;
            DbContextOptions<BasketDbContext> dbOptions = new DbContextOptionsBuilder<BasketDbContext>()
                .UseInMemoryDatabase(databaseName: "TestBasket")
                .Options;
            _basketDbContext = new BasketDbContext(dbOptions);
            _basketRepository = new BasketRepository(_basketDbContext);
        }

        [Fact]
        public void GetsExistingBasket()
        {
            Basket existingBasket = new Basket(){ Id = 1 };
            existingBasket.AddItem(1, 10, 2);

            _basketDbContext.Baskets.Add(existingBasket);
            _basketDbContext.SaveChanges();

            int bId = existingBasket.Id;
            _output.WriteLine($"BasketId: {bId}");

            Basket basketFromRepo = _basketRepository.GetById(bId);
            basketFromRepo.Id.Should().Be(existingBasket.Id);

            BasketItem firstItem = basketFromRepo.Items.FirstOrDefault();
            firstItem.ProductItemId.Should().Be(existingBasket.Items.First().Id);
        }

    }
}
