using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Infrastructure.Tests.Repository.Test
{
    public class BuyRepositoryTest
    {
        private IRepository<Buy> _buyRespositorySut;

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Add_WhenBuyHasInfo_ThenBuyAdded()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("FruitShop").Options))
            {
                //arrange
                var buy = new Buy()
                {
                    BuyId = 1,
                    Quantity = 1,
                    TotalPrice = 2
                };
                _buyRespositorySut = new BuyRepository(myContext);

                //act
                var result = _buyRespositorySut.Add(buy);
                myContext.SaveChanges();

                //assert
                result = myContext.Buy.First(x => x.BuyId == buy.BuyId);
                Assert.IsNotNull(result);
                Assert.AreEqual(buy.BuyId, result.BuyId);
                Assert.AreEqual(buy.Quantity, result.Quantity);
                Assert.AreEqual(buy.TotalPrice, result.TotalPrice);
            }
        }

        [Test]
        public void Delete_WhenBuyHasData_ThenBuyDeleted()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                var buyId = 1;
                var buy = new Buy()
                {
                    BuyId = 1,
                    Quantity = 1,
                    TotalPrice = 2
                };
                _buyRespositorySut = new BuyRepository(myContext);
                //act
                var result = _buyRespositorySut.Add(buy);
                myContext.SaveChanges();

                //arrange
                var currentBuy = myContext.Buy.FirstOrDefault(x => x.BuyId == buyId);
                _buyRespositorySut.Delete(currentBuy.BuyId);
                myContext.SaveChanges();

                //assert
                CollectionAssert.DoesNotContain(_buyRespositorySut.GetAll(), currentBuy);
            }
        }

        [Test]
        public void Get_WhenBuyIdExist_ThenGetBuy()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                //arrange
                var buyId = 1;
                var buy = new Buy()
                {
                    BuyId = 1,
                    Quantity = 1,
                    TotalPrice = 2
                };
                _buyRespositorySut = new BuyRepository(myContext);
                //act
                var result = _buyRespositorySut.Add(buy);
                myContext.SaveChanges();

                //act
                var currentBuy = myContext.Buy.FirstOrDefault(x => x.BuyId == buyId);
                result = _buyRespositorySut.Get(currentBuy.BuyId);

                //assert
                Assert.IsNotNull(result);
                Assert.AreEqual(currentBuy.BuyId, result.BuyId);
            }
        }

        [Test]
        public void Get_WhenBuyIdNoExist_ThenNoGetBuy()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                //arrange
                var buyId = 1;
                var buy = new Buy()
                {
                    BuyId = 3,
                    Quantity = 1,
                    TotalPrice = 2
                };
                _buyRespositorySut = new BuyRepository(myContext);
                //act
                var result = _buyRespositorySut.Add(buy);
                myContext.SaveChanges();

                //act
                var currentBuy = myContext.Buy.FirstOrDefault(x => x.BuyId == buyId);

                //assert
                Assert.IsNull(currentBuy);
            }
        }

        [Test]
        public void Get_WhenExistOneBuy_ThenGetAllBuys()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                //arrange
                var buy = new Buy()
                {
                    BuyId = 1,
                    Quantity = 1,
                    TotalPrice = 2
                };
                _buyRespositorySut = new BuyRepository(myContext);
                _buyRespositorySut.Add(buy);
                myContext.SaveChanges();

                //act
                var currentBuys = _buyRespositorySut.GetAll();

                //assert
                Assert.IsNotEmpty(currentBuys);
            }
        }

        [Test]
        public void Get_WhenNoBuysExist_ThenReturnNull()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                //act
                _buyRespositorySut = new BuyRepository(myContext);
                var currentBuys = _buyRespositorySut.GetAll();

                //assert
                Assert.IsEmpty(currentBuys);
            }
        }
    }
}

