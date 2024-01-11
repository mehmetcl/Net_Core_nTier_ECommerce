using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.EntityFrameWork
{
    public class EFProductDal : GenericRepository<Product>, IProductDal
    {
        public EFProductDal(ECommerceContext context) : base(context)
        {

        }
        public async Task<List<Product>> GetProductWithCategoryAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
           

        }
    }
}
