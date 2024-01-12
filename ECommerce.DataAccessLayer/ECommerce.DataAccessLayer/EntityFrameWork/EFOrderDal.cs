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
    public class EFOrderDal : GenericRepository<Order>, IOrderDal
    {
        public EFOrderDal(ECommerceContext context) : base(context)
        {
        }

        public async Task CreateOrderAsync(Order order)
        {
           await _context.Orders.AddAsync(order);
            Basket basket = await _context.Baskets.Where(x => x.UserId == order.UserId && x.ProductId == order.ProductId).FirstOrDefaultAsync();
            _context.Baskets.Remove(basket);
            Product product = await _context.Products.Where(x => x.Id == order.ProductId).FirstOrDefaultAsync();
            product.Stock -= order.Piece;
            _context.Products.Update(product);

        }

        public IQueryable<Order> GetOrdersByUserId(int userId)
        {
            return _context.Orders.Where(x => x.UserId == userId);
        }
    }
}
