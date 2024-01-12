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
    public class OrderService : GenericService<Order>, IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofWork;
        public OrderService(IGenericDal<Order> genericDal, IUnitOfWork unitofWork, IOrderDal orderDal, IMapper mapper) : base(genericDal, unitofWork)
        {
            _orderDal = orderDal;
            _mapper = mapper;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
          await _orderDal.CreateOrderAsync(order);
            await _unitofWork.CommitAsync();    
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderDal.GetOrdersByUserId(userId).ToListAsync();
            if (orders.Count <= 0)
            {
                throw new NotFoundException($"No User Id ({userId}) found from basket");
            }
            return orders;
        }
    }
}
