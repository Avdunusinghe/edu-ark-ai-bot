using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class LearningPlanConfiguration : IEntityTypeConfiguration<LearningPlan>
    {
        public void Configure(EntityTypeBuilder<LearningPlan> builder)
        {
            //Set Learning Plan Table Name
            builder.ToTable(EntityConstants.LEARNING_PLAN);

            //Set Learning Plan Table Primary Key
            builder.HasKey(x => x.Id);

            //For Nullable Relationship with Lesson Plan Table for Created User
            builder
                  .HasOne<User>(x => x.CreatedByUser)
                  .WithMany(r => r.CreatedUserLearningPlans)
                  .HasForeignKey(f => f.CreatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired(true);

            // For Nullable Relationship with Lesson Plan Table for Updated User
            builder
                   .HasOne<User>(x => x.UpdatedByUser)
                   .WithMany(r => r.UpdatedUserLearningPlans)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
