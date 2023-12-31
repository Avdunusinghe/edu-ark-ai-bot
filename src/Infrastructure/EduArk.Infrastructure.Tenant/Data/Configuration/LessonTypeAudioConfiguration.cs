using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class LessonTypeAudioConfiguration : IEntityTypeConfiguration<LessonTypeAudio>
    {
        public void Configure(EntityTypeBuilder<LessonTypeAudio> builder)
        {
            //Set Lesson Type Audio Table Name
            builder.ToTable(EntityConstants.LESSON_TYPE_AUDIO);

            //Set Lesson Type Audio Table Primary Key
            builder.HasKey(x => x.Id);

            //For Nullable Relationship with LessonTypeAudio Table for Created User
            builder
                  .HasOne<User>(x => x.CreatedByUser)
                  .WithMany(r => r.CreatedUserLessonTypeAudios)
                  .HasForeignKey(f => f.CreatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired(true);

            // For Nullable Relationship with LessonTypeAudio Table for Updated User
            builder
                   .HasOne<User>(x => x.UpdatedByUser)
                   .WithMany(r => r.UpdatedUserLessonTypeAudios)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
