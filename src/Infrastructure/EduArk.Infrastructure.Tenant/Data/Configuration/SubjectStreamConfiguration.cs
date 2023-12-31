using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class SubjectStreamConfiguration : IEntityTypeConfiguration<SubjectStream>
    {
        public void Configure(EntityTypeBuilder<SubjectStream> builder)
        {
            //Set SubjectStream Table Name
            builder.ToTable(EntityConstants.SUBJECT_STREAM_ENTITY_TABLE_NAME);

            //Set SubjectStream Table Primary Key
            builder.HasKey(x => x.Id);
        }
    }
}
