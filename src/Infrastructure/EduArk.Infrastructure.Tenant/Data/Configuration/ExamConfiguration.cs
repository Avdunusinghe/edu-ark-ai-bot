using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            //Set Exam Table Name
            builder.ToTable(EntityConstants.EXAM_ENTITY_TABLE_NAME);

            //Set Exam Table Primary Key
            builder.HasKey(x => x.Id);

            builder
                 .HasOne<AcademicYear>(x => x.AcademicYear)
                 .WithMany(r => r.Exams)
                 .HasForeignKey(f => f.AcademicYearId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired(true);

            builder
                .HasOne<ExamType>(x => x.ExamType)
                .WithMany(r => r.Exams)
                .HasForeignKey(f => f.ExamTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);


            builder
                .HasOne<Semester>(x => x.Semester)
                .WithMany(r => r.Exams)
                .HasForeignKey(f => f.SemesterId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            //For Nullable Relationship with Exam Table for Created User
            builder
                  .HasOne<User>(x => x.CreatedByUser)
                  .WithMany(r => r.CreatedExams)
                  .HasForeignKey(f => f.CreatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired(false);

            //For Nullable Relationship with Exam Table for Updated User
            builder
                 .HasOne<User>(x => x.UpdatedByUser)
                 .WithMany(r => r.UpdatedExams)
                 .HasForeignKey(f => f.UpdatedByUserId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired(false);
        }
    }
}
