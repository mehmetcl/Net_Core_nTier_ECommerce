using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EntityLayer.DTOS
{
    public class BasketDto
    {
        public int Id { get; set; }

        public int Piece { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }
    }
}
