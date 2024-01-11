
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EntityLayer.Concrete
{
    public class AboutImage 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public byte[] ImageData { get; set; }
        public int AboutId { get; set; }
        public About About { get; set; }
     

   

    }
}
