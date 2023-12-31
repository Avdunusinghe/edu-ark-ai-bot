using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class LessonTypeTextConfiguration : IEntityTypeConfiguration<LessonTypeText>
    {
        public void Configure(EntityTypeBuilder<LessonTypeText> builder)
        {
            //Set Lesson Type Text Table Name
            builder.ToTable(EntityConstants.LESSON_TYPE_TEXT);

            //Set Lesson Type Text Table Primary Key
            builder.HasKey(x => x.Id);

            //For Nullable Relationship with LessonTypeText Table for Created User
            builder
                  .HasOne<User>(x => x.CreatedByUser)
                  .WithMany(r => r.CreatedUserLessonTypeTexts)
                  .HasForeignKey(f => f.CreatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired(true);

            // For Nullable Relationship with LessonTypeText Table for Updated User
            builder
                   .HasOne<User>(x => x.UpdatedByUser)
                   .WithMany(r => r.UpdatedUserLessonTypeTexts)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
