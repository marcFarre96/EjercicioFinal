using Domain.Interfaces;
using Domain.Models;
using FruitShop.V1.Controllers.Fruits.Request;
using FruitShop.V1.Controllers.Fruits.Response;
using FruitShop.V1.Controllers.Fruits.Service.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FruitShop.V1.Controllers.Fruits.Service
{
    public class FruitService : IFruitService
    {
        private readonly IRepository<Fruit> _fruitRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public FruitService(IRepository<Fruit> fruitRepository, IUnitOfWork unitOfWork, ILogger logger)
        {
            _fruitRepository = fruitRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public FruitResponse Add(FruitRequest fruitRequest)
        {
            try
            {
                var fruit = new Fruit()
                {
                    FruitTypeId = (short)fruitRequest.FruitType,
                    Price = fruitRequest.Price
                };

                _fruitRepository.Add(fruit);
                _unitOfWork.SaveChanges();
                _logger.Info("Correct Added");
                return new FruitResponse(fruit);
            }
            catch (Exception)
            {
                _logger.Error("Impossible Add");
                throw;
            }
        }

        public IEnumerable<FruitResponse> GetAll()
        {
            return from fruit in _fruitRepository.GetAll()
                   select new FruitResponse(fruit);
        }

        public FruitResponse GetFruit(int fruitId)
        {
            try
            {
                var currentFruit = _fruitRepository.Get(fruitId);
                _logger.Info("Correct Get");
                return new FruitResponse(currentFruit);
            }
            catch (Exception)
            {
                _logger.Error("Imposible Get");
                throw;
            }
        }

        public bool Remove(int fruitId)
        {
            try
            {
                _fruitRepository.Delete(fruitId);

                if (_unitOfWork.SaveChanges() > 0)
                {
                    _logger.Info("Correct Deleted");
                    return true;
                }
                _logger.Info("Incorrect Deleted");
                return false;
            }
            catch (Exception)
            {
                _logger.Error("Error");
                throw;
            }
        }
    }
}
