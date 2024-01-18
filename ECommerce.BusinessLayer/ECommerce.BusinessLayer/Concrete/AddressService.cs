using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.UnitOfWork;
using ECommerce.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Concrete
{
    public class AddressService : GenericService<Address>, IAddressService
    {
        private readonly IAddressDal _addressDal;
        private readonly IMapper _mapper;
        public AddressService(IGenericDal<Address> genericDal, IUnitOfWork unitofWork, IAddressDal addressDal, IMapper mapper) : base(genericDal, unitofWork)
        {
            _addressDal = addressDal;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Address>> GetAddressAsync(string userId)
        {
            var addresses = await _addressDal.GetAddress(userId).ToListAsync();
            if (addresses.Count <= 0)
            {
                throw new NotFoundException($"No User Id ({userId}) found from address");
            }
            return addresses;
        }
    }
}
