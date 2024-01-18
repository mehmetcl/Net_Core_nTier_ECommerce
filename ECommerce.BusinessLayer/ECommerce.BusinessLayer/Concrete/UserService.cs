using AutoMapper;
using AutoMapper.Internal.Mappers;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.UnitOfWork;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using ECommerce.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Concrete
{
    public class UserService(IUserDal userDal, IMapper mapper, IUnitOfWork unitofWork, UserManager<User> userManager) : IUserService
    {
        private readonly IUserDal _userDal = userDal;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitofWork = unitofWork;
        private readonly UserManager<User> _userManager = userManager;

        public async Task<CustomResponseDto<UserDto>> CreateUserAsync(UserDto createUserDto)
        {
            var user = new User {
            Email = createUserDto.Email,  
            UserName=createUserDto.Username,
            };
            var result = await _userManager.CreateAsync(user,createUserDto.Password);  

            if (!result.Succeeded) {
                var errors = result.Errors.Select(x => x.Description.ToList());


                return CustomResponseDto<UserDto>.Fail(400,"",true);
            }


            var userDto = _mapper.Map<UserDto>(user);
            return CustomResponseDto<UserDto>.Success(200, userDto);
        }

        public async Task<CustomResponseDto<UserDto>> GetUserByNameAsync(string userName)
        {
           var user = await _userManager.FindByNameAsync(userName); 
            if (user == null) {
                return CustomResponseDto<UserDto>.Fail(404, "UserName not Found", true);
            
            }
            var userDto = _mapper.Map<UserDto>(user);
            return CustomResponseDto<UserDto>.Success(200, userDto);

        }
    }
}
