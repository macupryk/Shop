using Moq;
using NUnit.Framework;
using ShopApi.Common;
using ShopApi.Entities;
using ShopApi.Repositories;
using ShopApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopNUnitTestProject
{
    class ServiceTest
    {
        public PaginatedList<Item> items = new PaginatedList<Item>();
        public  Mock<IBaseRepository<Item>> repository;
        public  BaseService<Item> service;

        [SetUp]
        public void Setup()
        {
            items.Add(new Item { Id = 1L, Name = "Item1", Description = "Item1 Description", Price = 100, Stock = 10, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow });
            items.Add(new Item { Id = 2L, Name = "Item2", Description = "Item2 Description", Price = 200, Stock = 5, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow });
            items.Add(new Item { Id = 3L, Name = "Item3", Description = "Item3 Description", Price = 300, Stock = 50, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow });
            items.Add(new Item { Id = 4L, Name = "Item4", Description = "Item4 Description", Price = 250, Stock = 15, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow });
            items.Add(new Item { Id = 5L, Name = "Item5", Description = "Item5 Description", Price = 400, Stock = 105, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow });

            repository = new Mock<IBaseRepository<Item>>();
            repository.Setup(mr => mr.GetAsync(null, null)).ReturnsAsync(items);

            repository.Setup(mr => mr.Update(It.IsAny<Item>())).Callback<Item>(((i) => items[0].Stock= i.Stock));

            repository.Setup(mr => mr.GetAsync(It.IsAny<object>())).Returns((Int64 id) =>
            {
                return Task.FromResult(
                  (from item in items
                   where item.Id == id  
                   select item).FirstOrDefault()
                  );
            });

            var unitOfwork = new Mock<IUnitOfWork>();
            service = new BaseService<Item>(repository.Object, unitOfwork.Object);
        }

        [Test]
        public async Task GetItemsTest()
        {
            var result =await service.GetAsync();
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(5,result.Count);
            Assert.Pass();
        }
        [Test]
        public async Task GetItemTest()
        {
            Int64 id = 1;
            var result = await service.GetAsync(id);
            Assert.IsNotNull(result);
            Assert.AreEqual("Item1", result.Name);
            Assert.Pass();
        }

        [Test]
        public async Task UpdateItemTest()
        {
            Int64 id = 1;
            var result = await service.GetAsync(id);
            var originalStock = result.Stock;
            result.Stock -= 1;
            await service.UpdateAsync(result);
            Assert.AreEqual(originalStock-1, items.First(x=>x.Id==id).Stock);
            Assert.Pass();
        }
    }
}
