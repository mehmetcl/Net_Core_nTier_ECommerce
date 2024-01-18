using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EntityLayer.Concrete
{
    public class Order : BaseEntity
    {
       


        public int Price { get; set; }
        public int Piece { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }


        public int AddressId { get; set; }

        public Address Address { get; set; }


        public OrderStatus Case { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }

    public enum OrderStatus
    {
        PENDING,
        PROCESSING,
        SHIPPED,
        DELIVERED,
        CANCELLED
    }
}
