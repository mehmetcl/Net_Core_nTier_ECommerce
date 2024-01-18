using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.UnitOfWork;
using ECommerce.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Concrete
{
    public class BasketService : GenericService<Basket>, IBasketService
    {
        private readonly IBasketDal _basketDal;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofWork;

        public BasketService(IGenericDal<Basket> genericDal, IUnitOfWork unitofWork, IBasketDal basketDal, IMapper mapper) : base(genericDal, unitofWork)
        {
            _basketDal = basketDal;
            _mapper = mapper;
        }

        public async Task<Basket> AddBasketsAsync(Basket basket)
        {
            var baskets = await _basketDal.ProductIdAndUserId(basket);
            if (baskets == null)
            {
                await _basketDal.AddAsync(basket);
                await _unitofWork.CommitAsync();
                return basket;
            }
            else
            {
                baskets.Piece += basket.Piece;
                _basketDal.Update(basket);
                return basket;
            }
        }

        public async Task<Basket> GetBasketByProductIdAndUserIdAsync(string userId, int productId)
        {
            var baskets = await _basketDal.GetBasketByProductIdAndUserId(userId, productId);
            if (baskets == null)
            {
                throw new NotFoundException("There are no items in the cart");

            }


            return baskets;
        }

        public async Task<IEnumerable<Basket>> GetBasketsAsync(string userId)
        {
            var Baskets = await _basketDal.GetBaskets(userId).ToListAsync();

            if (Baskets.Count <= 0)
            {
                throw new NotFoundException($"No User Id({userId} found from basket)");
            }
            return Baskets;
        }
    }
}
