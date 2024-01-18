using ECommerce.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Abstract
{
    public interface IBasketService:IGenericService<Basket>
    {
        Task<IEnumerable<Basket>> GetBasketsAsync(string userId);

        Task<Basket> GetBasketByProductIdAndUserIdAsync(string userId, int productId);

        Task<Basket> AddBasketsAsync(Basket basket);
    }
}
