using Domain.Interfaces;
using Domain.Models;
using FruitShop.V1.Controllers.Fruits.Request;
using FruitShop.V1.Controllers.Fruits.Response;
using FruitShop.V1.Controllers.Fruits.Service.Interfaces;
using NLog;
using System;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Fruits.Service
{
    public class FruitService : IFruitService
    {
        private readonly IRepository<Fruit> _fruitRepository;
        private readonly IUnitOfWork _unitOfWork;
        private Logger logger = LogManager.GetCurrentClassLogger();


        public FruitService(IRepository<Fruit> fruitRepository, IUnitOfWork unitOfWork)
        {
            _fruitRepository = fruitRepository;
            _unitOfWork = unitOfWork;
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
                logger.Info("Correct Added");
                return new FruitResponse(fruit);
            }
            catch (Exception)
            {
                logger.Error("Impossible Add");
                throw;
            }
        }

        public IEnumerable<FruitResponse> GetAll()
        {
            foreach (var fruit in _fruitRepository.GetAll())
            {
                yield return new FruitResponse(fruit);
            }
        }

        public FruitResponse GetFruit(int fruitId)
        {
            try
            {
                var currentFruit = _fruitRepository.Get(fruitId);
                logger.Info("Correct Get");
                return new FruitResponse(currentFruit);
            }
            catch (Exception)
            {
                logger.Error("Imposible Get");
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
