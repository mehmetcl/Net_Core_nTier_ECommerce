using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.EntityFrameWork
{
    public class EFAddressDal : GenericRepository<Address>, IAddressDal
    {
        
        public EFAddressDal(ECommerceContext context) : base(context)
        {
        }

        public IQueryable<Address> GetAddress(int userId)
        {
          return _context.Addresses.Where(a => a.UserId == userId); 
        }
    }
}
