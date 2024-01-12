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
        IQueryable<Basket> GetBaskets(int userId);

        Task<Basket> ProductIdAndUserId(Basket basket);
        Task<Basket> GetBasketByProductIdAndUserId(int userId, int productId);
    }
}
