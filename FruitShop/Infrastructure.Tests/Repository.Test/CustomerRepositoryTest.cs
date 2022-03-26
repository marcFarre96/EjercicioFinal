using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Infrastructure.Tests.Repository.Test
{
    public class CustomerRepositoryTest
    {
        private IRepository<Customer> _customerRespositorySut;

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Add_WhenCustomerHasInfo_ThenCustomerAdded()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("FruitShop").Options))
            {
                //arrange
                var customer = new Customer()
                {
                    CustomerId = 1,
                    Name = "Marc"
                };
                _customerRespositorySut = new CustomerRepository(myContext);

                //act
                var result = _customerRespositorySut.Add(customer);
                myContext.SaveChanges();

                //assert
                result = myContext.Customer.First(x => x.CustomerId == customer.CustomerId);
                Assert.IsNotNull(result);
                Assert.AreEqual(customer.CustomerId, result.CustomerId);
                Assert.AreEqual(customer.Name, result.Name);
            }
        }

        [Test]
        public void Delete_WhenCustomerExist_ThenCustomerDeleted()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                var customerId = 1;
                var customer = new Customer()
                {
                    CustomerId = 1,
                    Name = "Marc"
                };
                _customerRespositorySut = new CustomerRepository(myContext);
                //act
                var result = _customerRespositorySut.Add(customer);
                myContext.SaveChanges();

                //arrange
                var currentCustomer = myContext.Customer.FirstOrDefault(x => x.CustomerId == customerId);
                _customerRespositorySut.Delete(currentCustomer.CustomerId);
                myContext.SaveChanges();

                //assert
                CollectionAssert.DoesNotContain(_customerRespositorySut.GetAll(), currentCustomer);
            }
        }

        [Test]
        public void Get_WhenCustomerIdExist_ThenGetCustomer()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                //arrange
                var customerId = 1;
                var customer = new Customer()
                {
                    CustomerId = 1,
                    Name = "Marc"
                };
                _customerRespositorySut = new CustomerRepository(myContext);
                //act
                var result = _customerRespositorySut.Add(customer);
                myContext.SaveChanges();

                //act
                var currentCustomer = myContext.Customer.FirstOrDefault(x => x.CustomerId == customerId);
                result = _customerRespositorySut.Get(currentCustomer.CustomerId);

                //assert
                Assert.IsNotNull(result);
                Assert.AreEqual(currentCustomer.CustomerId, result.CustomerId);
            }
        }

        [Test]
        public void Get_WhenCustomerIdNoExist_ThenNoGetCustomer()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                //arrange
                var customerId = 1;
                var customer = new Customer()
                {
                    CustomerId = 3,
                    Name = "Marc"
                };
                _customerRespositorySut = new CustomerRepository(myContext);

                //act
                var result = _customerRespositorySut.Add(customer);
                myContext.SaveChanges();

                //act
                var currentCustomer = myContext.Customer.FirstOrDefault(x => x.CustomerId == customerId);

                //assert
                Assert.IsNull(currentCustomer);
            }
        }

        [Test]
        public void Get_WhenExistOneCustomer_ThenGetAllCustomers()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                //arrange
                var customer = new Customer()
                {
                    CustomerId = 3,
                    Name = "Marc"
                };
                _customerRespositorySut = new CustomerRepository(myContext);
                _customerRespositorySut.Add(customer);
                myContext.SaveChanges();

                //act
                var currentCustomers = _customerRespositorySut.GetAll();

                //assert
                Assert.IsNotEmpty(currentCustomers);
            }
        }

        [Test]
        public void Get_WhenNoCustomerExist_ThenReturnNull()
        {
            using (var myContext = new FruitStoreDbContext(new DbContextOptionsBuilder<FruitStoreDbContext>().UseInMemoryDatabase("ShoppingStore").Options))
            {
                //arrange

                //act
                _customerRespositorySut = new CustomerRepository(myContext);
                var currentCustomers = _customerRespositorySut.GetAll();

                //assert
                Assert.IsEmpty(currentCustomers);
            }
        }
    }
}
