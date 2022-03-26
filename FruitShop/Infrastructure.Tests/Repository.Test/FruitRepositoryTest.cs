using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Infrastructure.Tests.Repository.Test
{
    public class FruitRepositoryTest
    {
        private IRepository<Fruit> _fruitRespositorySut;

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Add_WhenFruitHasInfo_ThenFruitAdded()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("FruitShop").Options))
            {
                //arrange
                var fruit = new Fruit()
                {
                    FruitId = 1,
                    FruitTypeId = 1,
                    Price = 3.4M
                };
                _fruitRespositorySut = new FruitRepository(myContext);

                //act
                var result = _fruitRespositorySut.Add(fruit);
                myContext.SaveChanges();

                //assert
                result = myContext.Fruit.First(x => x.FruitId == fruit.FruitId);
                Assert.IsNotNull(result);
                Assert.AreEqual(fruit.FruitId, result.FruitId);
                Assert.AreEqual(fruit.FruitTypeId, result.FruitTypeId);
                Assert.AreEqual(fruit.Price, result.Price);
            }
        }

        [Test]
        public void Delete_WhenFruitHasData_ThenFruitDeleted()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                var fruitId = 1;
                var fruit = new Fruit()
                {
                    FruitId = 1,
                    FruitTypeId = 1,
                    Price = 3.4M
                };
                _fruitRespositorySut = new FruitRepository(myContext);
                //act
                var result = _fruitRespositorySut.Add(fruit);
                myContext.SaveChanges();

                //arrange
                var currentFruit = myContext.Fruit.FirstOrDefault(x => x.FruitId == fruitId);
                _fruitRespositorySut.Delete(currentFruit.FruitId);
                myContext.SaveChanges();

                //assert
                CollectionAssert.DoesNotContain(_fruitRespositorySut.GetAll(), currentFruit);
            }
        }

        [Test]
        public void Get_WhenFruitIdExist_ThenGetArticle()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                //arrange
                var fruitId = 1;
                var fruit = new Fruit()
                {
                    FruitId = 1,
                    FruitTypeId = 1,
                    Price = 3.4M
                };
                _fruitRespositorySut = new FruitRepository(myContext);
                var result = _fruitRespositorySut.Add(fruit);
                myContext.SaveChanges();

                //act
                var currentFruit = myContext.Fruit.FirstOrDefault(x => x.FruitId == fruitId);
                result = _fruitRespositorySut.Get(currentFruit.FruitId);

                //assert
                Assert.IsNotNull(result);
                Assert.AreEqual(currentFruit.FruitId, result.FruitId);
            }
        }

        [Test]
        public void Get_WhenFruitIdNoExist_ThenNoGetArticle()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                //arrange
                var fruitId = 1;
                var fruit = new Fruit()
                {
                    FruitId = 3,
                    FruitTypeId = 1,
                    Price = 3.4M
                };
                _fruitRespositorySut = new FruitRepository(myContext);
                var result = _fruitRespositorySut.Add(fruit);
                myContext.SaveChanges();

                //act
                var currentFruit = myContext.Fruit.FirstOrDefault(x => x.FruitId == fruitId);

                //assert
                Assert.IsNull(currentFruit);
            }
        }

        [Test]
        public void Get_WhenExistOneFruit_ThenGetAllFruits()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                //arrange
                var fruit = new Fruit()
                {
                    FruitId = 3,
                    FruitTypeId = 1,
                    Price = 3.4M
                };
                _fruitRespositorySut = new FruitRepository(myContext);
                _fruitRespositorySut.Add(fruit);
                myContext.SaveChanges();

                //act
                var currentFruits = _fruitRespositorySut.GetAll();

                //assert
                Assert.IsNotEmpty(currentFruits);
            }
        }

        [Test]
        public void Get_WhenNoFruitsExist_ThenReturnNull()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                //arrange

                //act
                _fruitRespositorySut = new FruitRepository(myContext);
                var currentFruits = _fruitRespositorySut.GetAll();

                //assert
                Assert.IsEmpty(currentFruits);
            }
        }
    }
}
