using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using ECommerce.SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Abstract
{
    public interface IUserService
    {
        Task<CustomResponseDto<UserDto>> CreateUserAsync(UserDto userDto);

        Task<CustomResponseDto<UserDto>> GetUserByNameAsync(string userName);
    }
}
