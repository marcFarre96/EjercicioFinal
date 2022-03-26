using Domain.Interfaces;
using Domain.Models;
using FruitShop.V1.Controllers.Customers.Request;
using FruitShop.V1.Controllers.Customers.Response;
using FruitShop.V1.Controllers.Customers.Service.Interfaces;
using NLog;
using System;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Customers.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public CustomerService(IRepository<Customer> customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public CustomerResponse Add(CustomerRequest customerRequest)
        {
            try
            {
                var customer = new Customer()
                {
                    Name = customerRequest.Name
                };

                _customerRepository.Add(customer);
                _unitOfWork.SaveChanges();
                logger.Info("Correct Added");
                return new CustomerResponse(customer);
            }
            catch (Exception)
            {
                logger.Error("Impossible Add");
                throw;
            }
        }

        public IEnumerable<CustomerResponse> GetAll()
        {
            foreach (var customer in _customerRepository.GetAll())
            {
                yield return new CustomerResponse(customer);
            }
        }

        public CustomerResponse GetCustomer(int customerId)
        {
            try
            {
                var currentCustomer = _customerRepository.Get(customerId);
                logger.Info("Correct Get");
                return new CustomerResponse(currentCustomer);
            }
            catch (Exception)
            {
                logger.Error("Imposible Get");
                throw;
            }
        }

        public bool Remove(int customerId)
        {
            try
            {
                _customerRepository.Delete(customerId);

                if (_unitOfWork.SaveChanges() > 0)
                {
                    logger.Info("Correct Deleted");
                    return true;
                }
                logger.Info("Incorrect Deleted");
                return false;
            }
            catch (Exception)
            {
                logger.Error("Error");
                throw;
            }
        }
    }
}
