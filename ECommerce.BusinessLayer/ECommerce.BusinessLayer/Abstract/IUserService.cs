using ECommerce.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Abstract
{
    public interface IUserService:IGenericService<User>
    {
        Task BlokeAsync(int id);

        Task<User> LoginAsync(string userNameOrEmail, string password);

        Task<bool> EmailCheckAsync(string email);

        Task<bool> UsernameCheckAsync(string username);
    }
}
