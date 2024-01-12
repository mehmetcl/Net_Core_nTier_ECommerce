using ECommerce.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1); //otomatik artma
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.ToTable("Categories"); //tablo ismi eğer bu verilmezse appdbContext classında tanımlanan isimle oluşturur.
            
        }
    }
}
