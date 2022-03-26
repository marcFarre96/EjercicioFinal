using Domain.Interfaces;
using Domain.Models;
using FruitShop.V1.Controllers.Customers.Request;
using FruitShop.V1.Controllers.Customers.Service;
using FruitShop.V1.Controllers.Customers.Service.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FruitShop.Test.V1.Controllers.Customers.Service
{
    public class CustomerServiceTest
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
        public void Add_WhenCustomerRequestHasCorrectInfo_ThenAddedCorrect()
        {
            //arrange
            CustomerRequest customerRequest = new CustomerRequest()
            {
                Name = "Marc"
            };

            var customer = new Customer()
            {
                CustomerId  = 1,
                Name = "Marc"
            };
            _customerRepositoryMock.Setup(x => x.Add(It.IsAny<Customer>())).Returns(customer);

            //act
            var customerResult = _customerRepositorySut.Add(customerRequest);

            //assert
            Assert.IsNotNull(customerResult);
            Assert.AreEqual(customer.CustomerId, customerResult.CustomerId);
            Assert.AreEqual(customer.Name, customerResult.Name);
        }

        [Test]
        public void Add_WhenCustomerRequestNoHasCorrectInfo_ThenNotAdded()
        {
            //arrange
            CustomerRequest customerRequest = new CustomerRequest()
            {
                Name = "Marc"
            };

            var customer = new Customer()
            {
                CustomerId = 4,
                Name = "Maria"
            };
            _customerRepositoryMock.Setup(x => x.Add(It.IsAny<Customer>())).Returns(customer);

            //act
            var customerResult = _customerRepositorySut.Add(customerRequest);

            //assert
            Assert.IsNotNull(customerResult);
            Assert.AreNotEqual(customer.Name, customerResult.Name);
        }

        [Test]
        public void Sumbit_WhenExceptionSavingChanges_ThenThrowException()
        {
            //arrange
            CustomerRequest customerRequest = new CustomerRequest();
            var customer = new Customer()
            {
                CustomerId = 4,
                Name = "Maria"
            };
            _customerRepositoryMock.Setup(x => x.Add(It.IsAny<Customer>())).Returns(customer); 
            _unitOfWorkMock.Setup(x => x.SaveChanges()).Throws(new Exception());

            //act & assert
            Assert.Throws<Exception>(() => _customerRepositorySut.Add(customerRequest));
        }

        [Test]
        public void Remove_WhenCustomerExist_ThenCorrectRemove()
        {
            //arrange
            var customerId = 4;
            var customer = new Customer()
            {
                CustomerId = 4,
                Name = "Maria"
            };

            _customerRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(customer);
            _customerRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));

            //act
            var response = _customerRepositorySut.Remove(customerId);

            //assert
            Assert.IsTrue(true);
        }

        [Test]
        public void Remove_WhenCustomerNoExist_ThenFailRemove()
        {
            //arrange
            var customerId = 1;
            var customer = new Customer()
            {
                CustomerId = 4,
                Name = "Maria"
            };

            _customerRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(customer);
            _customerRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));

            //act
            var response = _customerRepositorySut.Remove(customerId);

            //assert
            Assert.IsFalse(false);
        }

        [Test]
        public void GetCustomer_WhenCorrectId_ThenGetCustomerSuccess()
        {
            //arrange
            var customerId = 1;
            var customer = new Customer()
            {
                CustomerId = 1,
                Name = "Maria"
            };
            _customerRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(customer); 

            //act
            var customerResult = _customerRepositorySut.GetCustomer(customerId);

            //assert
            Assert.AreEqual(customerResult.CustomerId, customerId);
        }

        [Test]
        public void GetCustomer_WhenIncorrectId_ThenGetCustomerWrong()
        {
            //arrange
            var customerId = 1;
            var customer = new Customer()
            {
                CustomerId = 1,
                Name = "Maria"
            };
            _customerRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(customer);

            //act
            var customerResult = _customerRepositorySut.GetCustomer(customerId);

            //Assert
            Assert.AreNotEqual(customerResult.CustomerId, customerId);
        }

        [Test]
        public void GetAll_WhenData_Then()
        {
            //arrange
            CustomerRequest customerRequest = new CustomerRequest()
            {
                Name = "Marc"
            };

            var customer = new Customer()
            {
                CustomerId = 4,
                Name = "Maria"
            };
            _customerRepositoryMock.Setup(x => x.Add(It.IsAny<Customer>())).Returns(customer);

            //act
            _customerRepositorySut.Add(customerRequest);
            var list = new List<Customer> { customer };
            _customerRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            var listResult = _customerRepositorySut.GetAll();

            //assert
            Assert.IsNotEmpty(list);
        }

        [Test]
        public void GetAll_WhenNoData_Then()
        {
            //arrange
            var list = new List<Customer>();
            _customerRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            var listResult = _customerRepositorySut.GetAll();

            //assert
            Assert.IsEmpty(list);
        }
    }
}
