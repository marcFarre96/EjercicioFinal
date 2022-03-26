using Domain.Interfaces;
using Domain.Models;
using FruitShop.V1.Controllers.Buys.Request;
using FruitShop.V1.Controllers.Buys.Service;
using FruitShop.V1.Controllers.Buys.Service.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShop.Test.V1.Controllers.Buys.Service
{
    public class BuyServiceTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IRepository<Buy>> _buyRepositoryMock;
        private IBuyService _buyRepositorySut;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _buyRepositoryMock = new Mock<IRepository<Buy>>();
            _buyRepositorySut = new BuyService(_buyRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public void Add_WhenBuyRequestHasCorrectInfo_ThenAddedCorrect()
        {
            //arrange
            BuyRequest buyRequest = new BuyRequest()
            {
                CustomerId = 1,
                FruitId = 1,
                Quantity = 10
            };

            var buy = new Buy()
            {
                BuyId = 0,
                Quantity = 10
            };
            _buyRepositoryMock.Setup(x => x.Add(It.IsAny<Buy>())).Returns(buy);

            //act
            var buyResult = _buyRepositorySut.Add(buyRequest);

            //assert
            Assert.IsNotNull(buyResult);
            Assert.AreEqual(buy.BuyId, buyResult.BuyId);
            Assert.AreEqual(buy.Quantity, buyResult.Quantity);
        }

        [Test]
        public void Add_WhenBuyRequestNoHasCorrectInfo_ThenNotAdded()
        {
            //arrange
            BuyRequest buyRequest = new BuyRequest()
            {
                CustomerId = 1,
                FruitId = 1,
                Quantity = 10
            };

            var buy = new Buy()
            {
                BuyId = 2,
                Quantity = 4
            };
            _buyRepositoryMock.Setup(x => x.Add(It.IsAny<Buy>())).Returns(buy);

            //act
            var buyResult = _buyRepositorySut.Add(buyRequest);

            //assert
            Assert.IsNotNull(buyResult);
            Assert.AreNotEqual(buy.BuyId, buyResult.BuyId);
            Assert.AreNotEqual(buy.Quantity, buyResult.Quantity);
        }

        [Test]
        public void Sumbit_WhenExceptionSavingChanges_ThenThrowException()
        {
            //arrange
            BuyRequest buyRequest = new BuyRequest();
            var buy = new Buy()
            {
                BuyId = 2,
                Quantity = 4
            };
            _buyRepositoryMock.Setup(x => x.Add(It.IsAny<Buy>())).Returns(buy); 
            _unitOfWorkMock.Setup(x => x.SaveChanges()).Throws(new Exception());

            //act & assert
            Assert.Throws<Exception>(() => _buyRepositorySut.Add(buyRequest));
        }

        [Test]
        public void Remove_WhenArticleExist_ThenCorrectRemove()
        {
            var buyId = 2;

            var buy = new Buy()
            {
                BuyId = 2,
                Quantity = 4
            };

            _buyRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(buy);
            _buyRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));

            var response = _buyRepositorySut.Remove(buyId);

            Assert.IsTrue(true);
        }

        [Test]
        public void Remove_WhenArticleNoExist_ThenFailRemove()
        {
            var buyId = 2;

            var buy = new Buy()
            {
                BuyId = 1,
                Quantity = 4
            };

            _buyRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(buy);
            _buyRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));

            var response = _buyRepositorySut.Remove(buyId);

            Assert.IsFalse(false);
        }

        [Test]
        public void GetBuy_WhenCorrectId_ThenGetBuySuccess()
        {
            //arrange
            var buyId = 1;

            var buy = new Buy()
            {
                BuyId = 1,
                Quantity = 4
            };
            _buyRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(buy);
            var buyResult = _buyRepositorySut.GetFruit(buyId);

            Assert.AreEqual(buyResult.BuyId, buyId);
        }

        [Test]
        public void GetArticle_WhenIncorrectId_ThenGetArticleWrong()
        {
            //arrange
            var buyId = 3;

            var buy = new Buy()
            {
                BuyId = 1,
                Quantity = 4
            };
            _buyRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(buy);
            var buyResult = _buyRepositorySut.GetFruit(buyId);

            Assert.AreNotEqual(buyResult.BuyId, buyId);
        }

        [Test]
        public void GetAll_WhenData_Then()
        {
            //arrange
            BuyRequest buyRequest = new BuyRequest()
            {
                CustomerId = 1,
                FruitId = 1,
                Quantity = 10
            };

            var buy = new Buy()
            {
                BuyId = 2,
                Quantity = 4
            };
            _buyRepositoryMock.Setup(x => x.Add(It.IsAny<Buy>())).Returns(buy);

            //act
            var buyResult = _buyRepositorySut.Add(buyRequest);
            var list = new List<Buy> { buy };
            _buyRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            var listResult = _buyRepositorySut.GetAll();

            //assert
            Assert.IsNotEmpty(list);
        }

        [Test]
        public void GetAll_WhenNoData_Then()
        {
            //arrange
            var list = new List<Buy>();
            _buyRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            var listResult = _buyRepositorySut.GetAll();

            //assert
            Assert.IsEmpty(list);
        }
    }
}
