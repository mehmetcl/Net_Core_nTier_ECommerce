using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EntityLayer.Concrete
{
    public class Address 
    {

        public int Id { get; set; }
        public string Name { get; set; }    

        public string City { get; set; }    
        public string Town { get; set; }   

        public string Description { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
