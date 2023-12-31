using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class ExamTypeConfiguration : IEntityTypeConfiguration<ExamType>
    {
        public void Configure(EntityTypeBuilder<ExamType> builder)
        {
            //Set ExamType Table Name
            builder.ToTable(EntityConstants.EXAM_TYPE_ENTITY_TABLE_NAME);

            //Set ExamType Table Primary Key
            builder.HasKey(x => x.Id);

        }
    }
}
