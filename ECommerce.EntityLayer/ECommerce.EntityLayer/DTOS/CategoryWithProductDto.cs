using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EntityLayer.DTOS
{
    public class CategoryWithProductDto:CategoryDto
    {
        
        public ICollection<ProductDto> Products { get; set; }
    }
}
