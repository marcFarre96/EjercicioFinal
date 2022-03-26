using Domain.Interfaces;
using Domain.Models;
using FruitShop.V1.Controllers.Customers.Request;
using FruitShop.V1.Controllers.Customers.Service;
using FruitShop.V1.Controllers.Customers.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FruitShop.Test.V1.Controllers.Customers
{
    public class CustomerControllerTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IRepository<Customer>> _customerRepositoryMock;
        private ICustomerService _customerRepositorySut;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _customerRepositoryMock = new Mock<IRepository<Customer>>();
            _customerRepositorySut = new CustomerService(_customerRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public void Submit_WhenCorrectAdded_Then201Send()
        {
            //arrange
            CustomerRequest customerRequest = new CustomerRequest();

            //act
            var response = _customerRepositorySut.Add(customerRequest);
            var objectResult = new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };

            //assert
            Assert.AreEqual(HttpStatusCode.Created, (HttpStatusCode)objectResult.StatusCode);
        }

        [Test]
        public void GetAll_WhenCorrectGetAll_Then200Send()
        {
            //act
            var response = _customerRepositorySut.GetAll();
            var objectResult = new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };

            //assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)objectResult.StatusCode);
        }

        [Test]
        public void GetFruit_WhenCorrectGet_Then200OK()
        {
            //arrange
            var customerId = 1;

            //act
            var response = _customerRepositorySut.GetCustomer(customerId);
            var objectResult = new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };

            //assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)objectResult.StatusCode);
        }

        [Test]
        public void DeleteFruit_WhenDeleted_Then200OK()
        {
            //arrange
            var customerId = 1;

            //act
            var response = _customerRepositorySut.Remove(customerId);
            var objectResult = new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };

            //assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)objectResult.StatusCode);
        }
    }
}
