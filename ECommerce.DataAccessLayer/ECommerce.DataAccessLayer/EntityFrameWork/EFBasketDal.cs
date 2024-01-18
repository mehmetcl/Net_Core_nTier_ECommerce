using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.EntityFrameWork
{
    public class EFBasketDal : GenericRepository<Basket>, IBasketDal
    {
        public EFBasketDal(ECommerceContext context) : base(context)
        {
        }

        public async Task<Basket> GetBasketByProductIdAndUserId(string userId, int productId)
        {
            return await _context.Baskets.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefaultAsync();
        }

        public IQueryable<Basket> GetBaskets(string userId)
        {
            return _context.Baskets.Where(x => x.UserId == userId);
        }

        public async Task<Basket> ProductIdAndUserId(Basket basket)
        {
            return _context.Baskets.Where(x => x.UserId == basket.UserId && x.ProductId == basket.ProductId).FirstOrDefault();
        }
    }
}
