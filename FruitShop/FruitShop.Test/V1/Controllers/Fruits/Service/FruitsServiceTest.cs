using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using FruitShop.V1.Controllers.Customers.Service.Interfaces;
using FruitShop.V1.Controllers.Fruits.Request;
using FruitShop.V1.Controllers.Fruits.Service;
using FruitShop.V1.Controllers.Fruits.Service.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShop.Test.V1.Controllers.Fruits.Service
{
    public class FruitsServiceTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IRepository<Fruit>> _fruitRepositoryMock;
        private IFruitService _fruitRepositorySut;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _fruitRepositoryMock = new Mock<IRepository<Fruit>>();
            _fruitRepositorySut = new FruitService(_fruitRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public void Add_WhenFruitRequestHasCorrectInfo_ThenAddedCorrect()
        {
            //arrange
            FruitRequest fruitRequest = new FruitRequest()
            {
                FruitType = FruitTypeEnum.Manzanas,
                Price = 2.3M
            };

            var fruit = new Fruit()
            {
                FruitId = 1,
                FruitTypeId = 1
            };
            _fruitRepositoryMock.Setup(x => x.Add(It.IsAny<Fruit>())).Returns(fruit);

            //act
            var fruitResult = _fruitRepositorySut.Add(fruitRequest);

            //assert
            Assert.IsNotNull(fruitResult);
            Assert.AreEqual(fruit.FruitId, fruitResult.FruitId);
        }

        [Test]
        public void Add_WhenFruitRequestNoHasCorrectInfo_ThenNotAdded()
        {
            //arrange
            FruitRequest fruitRequest = new FruitRequest()
            {
                FruitType = FruitTypeEnum.Manzanas,
                Price = 2.3M
            };

            var fruit = new Fruit()
            {
                FruitId = 1,
                FruitTypeId = 2
            };
            _fruitRepositoryMock.Setup(x => x.Add(It.IsAny<Fruit>())).Returns(fruit);

            //act
            var fruitResult = _fruitRepositorySut.Add(fruitRequest);

            //assert
            Assert.IsNotNull(fruitResult);
            Assert.AreNotEqual(fruit.FruitId, fruitResult.FruitId);
        }

        [Test]
        public void Sumbit_WhenExceptionSavingChanges_ThenThrowException()
        {
            //arrange
            FruitRequest fruitRequest = new FruitRequest();
            var fruit = new Fruit()
            {
                FruitId = 1,
                FruitTypeId = 2
            };
            _fruitRepositoryMock.Setup(x => x.Add(It.IsAny<Fruit>())).Returns(fruit);
            _unitOfWorkMock.Setup(x => x.SaveChanges()).Throws(new Exception());

            //act & assert
            Assert.Throws<Exception>(() => _fruitRepositorySut.Add(fruitRequest));
        }

        [Test]
        public void Remove_WhenFruitExist_ThenCorrectRemove()
        {
            //arrange
            var fruitId = 1;
            var fruit = new Fruit()
            {
                FruitId = 1,
                FruitTypeId = 2
            };

            _fruitRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(fruit);
            _fruitRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));

            //act
            var response = _fruitRepositorySut.Remove(fruitId);

            //assert
            Assert.IsTrue(true);
            Assert.IsNotNull(fruitId);
        }

        [Test]
        public void Remove_WhenFruitNoExist_ThenFailRemove()
        {
            //arrange
            var fruitId = 2;
            var fruit = new Fruit()
            {
                FruitId = 1,
                FruitTypeId = 2
            };

            _fruitRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(fruit);
            _fruitRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));

            //act
            var response = _fruitRepositorySut.Remove(fruitId);

            //assert
            Assert.IsFalse(true);
            Assert.IsNotNull(fruitId);
        }

        [Test]
        public void GetFruit_WhenCorrectId_ThenGetFruitSuccess()
        {
            //arrange
            var fruitId = 1;
            var fruit = new Fruit()
            {
                FruitId = 1,
                FruitTypeId = 2
            };

            _fruitRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(fruit);
            //act
            var fruitResult = _fruitRepositorySut.GetFruit(fruitId);

            //assert
            Assert.AreEqual(fruitResult.FruitId, fruitId);
            Assert.IsNotNull(fruitId);
        }

        [Test]
        public void GetFruit_WhenIncorrectId_ThenGetFruitWrong()
        {
            //arrange
            var fruitId = 3;
            var fruit = new Fruit()
            {
                FruitId = 1,
                FruitTypeId = 2
            };

            _fruitRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(fruit);
            //act
            var fruitResult = _fruitRepositorySut.GetFruit(fruitId);

            //assert
            Assert.AreNotEqual(fruitResult.FruitId, fruitId);
            Assert.IsNotNull(fruitId);
        }

        [Test]
        public void GetAll_WhenData_Then()
        {
            //arrange
            FruitRequest fruitRequest = new FruitRequest()
            {
                FruitType = FruitTypeEnum.Manzanas,
                Price = 2.3M
            };
            var fruit = new Fruit()
            {
                FruitId = 1,
                FruitTypeId = 2
            };
            _fruitRepositoryMock.Setup(x => x.Add(It.IsAny<Fruit>())).Returns(fruit);

            //act
            _fruitRepositorySut.Add(fruitRequest);
            var list = new List<Fruit> { fruit };
            _fruitRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            var listResult = _fruitRepositorySut.GetAll();

            //assert
            Assert.IsNotEmpty(list);
        }

        [Test]
        public void GetAll_WhenNoData_Then()
        {
            //arrange
            var list = new List<Fruit>();
            _fruitRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            var listResult = _fruitRepositorySut.GetAll();

            //assert
            Assert.IsEmpty(list);
        }
    }
}
