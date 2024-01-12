using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EntityLayer.Concrete
{
    public class User : BaseEntity
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }


        public ICollection<Basket>? Baskets { get; set; }

        public ICollection<Address>? Addresses { get; set; }


        public List<Order>? Orders { get; set; }

    }
}
