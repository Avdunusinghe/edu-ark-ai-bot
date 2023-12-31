using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class ClassNameConfiguration : IEntityTypeConfiguration<ClassName>
    {
        public void Configure(EntityTypeBuilder<ClassName> builder)
        {
            //Set ClassName Table Name
            builder.ToTable(EntityConstants.CLASS_NAME_ENTITY_TABLE_NAME);

            //Set ClassName Table Primary Key
            builder.HasKey(x => x.Id);
        }
    }
}
