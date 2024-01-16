using ECommerce.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Abstract
{
    public interface IUserDal:IGenericDal<User>
    {
        void Block(string id);

        Task<User> LoginAsync(string userNameOrEmail, string password);

        Task<bool> EmailCheckAsync(string email);

        Task<bool> UsernameCheckAsync(string username);
    }
}
