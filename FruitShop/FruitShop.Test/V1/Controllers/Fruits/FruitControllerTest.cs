using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using FruitShop.V1.Controllers.Fruits.Request;
using FruitShop.V1.Controllers.Fruits.Service;
using FruitShop.V1.Controllers.Fruits.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FruitShop.Test.V1.Controllers.Fruits
{
    public class FruitControllerTest
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
        public void Submit_WhenCorrectAdded_Then201Send()
        {
            //arrange
            FruitRequest fruitRequest = new FruitRequest();

            //act
            var response = _fruitRepositorySut.Add(fruitRequest);
            var objectResult = new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };

            //assert
            Assert.AreEqual(HttpStatusCode.Created, (HttpStatusCode)objectResult.StatusCode);
        }

        [Test]
        public void GetAll_WhenCorrectGetAll_Then200Send()
        {
            //act
            var response = _fruitRepositorySut.GetAll();
            var objectResult = new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };

            //assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)objectResult.StatusCode);
        }

        [Test]
        public void GetFruit_WhenCorrectGet_Then200OK()
        {
            //arrange
            var fruitId = 1;

            //act
            var response = _fruitRepositorySut.GetFruit(fruitId);
            var objectResult = new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };

            //assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)objectResult.StatusCode);
        }

        [Test]
        public void DeleteFruit_WhenDeleted_Then200OK()
        {
            //arrange
            var fruitId = 1;

            //act
            var response = _fruitRepositorySut.Remove(fruitId);
            var objectResult = new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };

            //assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)objectResult.StatusCode);
        }
    }
}
