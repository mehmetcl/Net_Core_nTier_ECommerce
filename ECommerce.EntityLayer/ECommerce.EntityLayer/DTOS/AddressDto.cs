using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EntityLayer.DTOS
{
    public class AddressDto:BaseDto
    {
        public string Country { get; set; }

        public string Province { get; set; }

        public string District { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public int UserId { get; set; }
    }
}
