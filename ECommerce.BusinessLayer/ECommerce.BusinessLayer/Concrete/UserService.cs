using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.UnitOfWork;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using ECommerce.SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Concrete
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofWork;
        public UserService(IGenericDal<User> genericDal, IUnitOfWork unitofWork, IUserDal userDal, IMapper mapper) : base(genericDal, unitofWork)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        public Task<CustomResponseDto<UserDto>> CreateUserAsync(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<UserDto>> GetUserByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
