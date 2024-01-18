using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EntityLayer.Concrete
{
    public class Address :BaseEntity
    {
    

        public string Country { get; set; }

        public string Province { get; set; }

        public string District { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public User User { get; set; }
      

     

        public ICollection<Order>? Orders { get; set; }
    }
}
