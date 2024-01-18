using ECommerce.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Abstract
{
    public interface IBasketDal:IGenericDal<Basket>
    {
        IQueryable<Basket> GetBaskets(string userId);

        Task<Basket> ProductIdAndUserId(Basket basket);
        Task<Basket> GetBasketByProductIdAndUserId(string userId, int productId);
    }
}
