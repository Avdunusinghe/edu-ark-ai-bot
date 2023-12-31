using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            //Set Lesson Table Name
            builder.ToTable(EntityConstants.LESSON);

            //Set Lesson Table Primary Key
            builder.HasKey(x => x.Id);

            //For Nullable Relationship with Lesson Table for Created User
            builder
                  .HasOne<User>(x => x.CreatedByUser)
                  .WithMany(r => r.CreatedUserLessons)
                  .HasForeignKey(f => f.CreatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired(true);

            // For Nullable Relationship with Lesson Table for Updated User
            builder
                   .HasOne<User>(x => x.UpdatedByUser)
                   .WithMany(r => r.UpdatedUserLessons)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
