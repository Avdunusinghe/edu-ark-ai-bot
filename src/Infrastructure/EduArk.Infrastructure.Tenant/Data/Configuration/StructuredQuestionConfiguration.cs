using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class StructuredQuestionConfiguration : IEntityTypeConfiguration<StructuredQuestion>
    {
        public void Configure(EntityTypeBuilder<StructuredQuestion> builder)
        {


            //Set User Table Name
            builder.ToTable(EntityConstants.STRUCTURED_QUESTION_ENTITY_TABLE_NAME);

            //Set User Table Primary Key
            builder.HasKey(x => x.Id);

            //For Nullable Relationship with User Table for Created User
            builder
                  .HasOne<User>(x => x.CreatedByUser)
                  .WithMany(r => r.CreatedStructuredQuestions)
                  .HasForeignKey(f => f.CreatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired(true);

            //For Nullable Relationship with User Table for Updated User
            builder
                 .HasOne<User>(x => x.UpdatedByUser)
                 .WithMany(r => r.UpdatedStructuredQuestions)
                 .HasForeignKey(f => f.UpdatedByUserId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired(true);


            builder
               .Property(x => x.Marks)
               .HasPrecision(5, 2);

        }
    }
}
