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
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).UseIdentityColumn(1);
            //builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            //builder.Property(x => x.Surname).IsRequired();
            //builder.Property(x => x.Email).IsRequired();
            //builder.Property(x => x.Password);
            //builder.Property(x => x.RoleId).HasDefaultValue(1);
        }
    }
}
