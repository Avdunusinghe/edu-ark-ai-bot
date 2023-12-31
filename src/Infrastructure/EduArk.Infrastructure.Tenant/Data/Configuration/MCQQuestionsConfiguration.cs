using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class MCQQuestionsConfiguration : IEntityTypeConfiguration<MCQQuestions>
    {
        public void Configure(EntityTypeBuilder<MCQQuestions> builder)
        {


            //Set User Table Name
            builder.ToTable(EntityConstants.MCQ_QUESTIONS_ENTITY_TABLE_NAME);

            //Set User Table Primary Key
            builder.HasKey(x => x.Id);

            //For Nullable Relationship with User Table for Created User
            builder
                  .HasOne<User>(x => x.CreatedByUser)
                  .WithMany(r => r.CreatedMCQQuestions)
                  .HasForeignKey(f => f.CreatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired(true);

            //For Nullable Relationship with User Table for Updated User
            builder
                 .HasOne<User>(x => x.UpdatedByUser)
                 .WithMany(r => r.UpdatedMCQQuestions)
                 .HasForeignKey(f => f.UpdatedByUserId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired(true);

            builder
                .HasOne<Assessment>(x => x.Assessment)
                .WithMany(r => r.MCQQuestions)
                .HasForeignKey(f => f.AssessmentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            builder
               .Property(x => x.Marks)
               .HasPrecision(5, 2);



        }
    }
}
