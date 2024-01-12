using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.UnitOfWork;
using ECommerce.EntityLayer.Concrete;
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

        public async Task BlokeAsync(int id)
        {
            _userDal.Block(id);
            _unitofWork.CommitAsync();

        }

        public async Task<bool> EmailCheckAsync(string email)
        {

            return await _userDal.EmailCheckAsync(email);
        }

        public async Task<User> LoginAsync(string userNameOrEmail, string password)
        {
            var user = await _userDal.LoginAsync(userNameOrEmail, password);
            if (user == null)
            {
                throw new NotFoundException($"{typeof(User).Name} {userNameOrEmail} not found");
            }
            return user;
        }

        public async Task<bool> UsernameCheckAsync(string username)
        {
            return await _userDal.UsernameCheckAsync(username);
        }
    }
}
