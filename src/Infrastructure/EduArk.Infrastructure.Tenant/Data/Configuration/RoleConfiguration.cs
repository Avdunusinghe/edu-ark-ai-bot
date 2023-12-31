using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //Set Role Table Name
            builder.ToTable(EntityConstants.ROLE_ENTITY_TABLE_NAME);


            //Set User Table Primary Key
            builder.HasKey(x => x.Id);
        }
    }
}
