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
    public class EFCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        public EFCategoryDal(ECommerceContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByIdProductsAsync(int categoryid)
        {
            return await _context.Categories.Include(x => x.Products).Where(x => x.Id == categoryid).SingleOrDefaultAsync();
        }
    }
}
