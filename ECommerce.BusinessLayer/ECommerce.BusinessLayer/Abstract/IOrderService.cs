using ECommerce.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Abstract
{
    public interface IOrderService:IGenericService<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

        Task<Order> CreateOrderAsync(Order order);
    }
}
