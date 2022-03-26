using Domain.Interfaces;
using Domain.Models;
using FruitShop.V1.Controllers.Buys.Request;
using FruitShop.V1.Controllers.Buys.Response;
using FruitShop.V1.Controllers.Buys.Service.Interfaces;
using NLog;
using System;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Buys.Service
{
    public class BuyService : IBuyService
    {
        private readonly IRepository<Buy> _buyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public BuyService(IRepository<Buy> buyRepository, IUnitOfWork unitOfWork)
        {
            _buyRepository = buyRepository;
            _unitOfWork = unitOfWork;
        }

        public BuyResponse Add(BuyRequest buyRequest)
        {
            try
            {
                var buy = new Buy()
                {
                    CustomerId = buyRequest.CustomerId,
                    FruitId = buyRequest.FruitId,
                    Quantity = buyRequest.Quantity,
                };

                _buyRepository.Add(buy);
                _unitOfWork.SaveChanges();
                logger.Info("Correct Added");
                return new BuyResponse(buy);
            }
            catch (Exception)
            {
                logger.Error("Impossible Add");
                throw;
            }  
        }

        public IEnumerable<BuyResponse> GetAll()
        {
            foreach (var buy in _buyRepository.GetAll())
            {
                yield return new BuyResponse(buy);
            }
        }

        public BuyResponse GetFruit(int buyId)
        {
            try
            {
                var currentBuy = _buyRepository.Get(buyId);
                logger.Info("Correct Get");
                return new BuyResponse(currentBuy);
            }
            catch (Exception)
            {
                logger.Error("Imposible Get");
                throw;
            }    
        }

        public bool Remove(int buyId)
        {
            try
            {
                _buyRepository.Delete(buyId);

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
