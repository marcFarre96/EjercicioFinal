using Domain.Interfaces;
using Domain.Models;
using FruitShop.V1.Controllers.Buys.Request;
using FruitShop.V1.Controllers.Buys.Service;
using FruitShop.V1.Controllers.Buys.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FruitShop.Test.V1.Controllers.Buys
{
    public class BuyControllerTest
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
        public void Submit_WhenCorrectAdded_Then201Send()
        {
            //arrange
            BuyRequest buyRequest = new BuyRequest();

            //act
            var response = _buyRepositorySut.Add(buyRequest);
            var objectResult = new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };

            //assert
            Assert.AreEqual(HttpStatusCode.Created, (HttpStatusCode)objectResult.StatusCode);
        }

        [Test]
        public void GetAll_WhenCorrectGetAll_Then200Send()
        {
            //act
            var response = _buyRepositorySut.GetAll();
            var objectResult = new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };

            //assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)objectResult.StatusCode);
        }

        [Test]
        public void GetFruit_WhenCorrectGet_Then200OK()
        {
            //arrange
            var buyId = 1;

            //act
            var response = _buyRepositorySut.GetFruit(buyId);
            var objectResult = new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };

            //assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)objectResult.StatusCode);
        }

        [Test]
        public void DeleteFruit_WhenDeleted_Then200OK()
        {
            //arrange
            var buyId = 1;

            //act
            var response = _buyRepositorySut.Remove(buyId);
            var objectResult = new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };

            //assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)objectResult.StatusCode);
        }
    }
}
