using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class SemesterConfiguration : IEntityTypeConfiguration<Semester>
    {
        public void Configure(EntityTypeBuilder<Semester> builder)
        {
            //Set Semester Table Name
            builder.ToTable(EntityConstants.SEMESTER_ENTITY_TABLE_NAME);

            //Set Semester Table Primary Key
            builder.HasKey(x => x.Id);
        }
    }
}
