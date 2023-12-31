using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class ExamMarkConfiguration : IEntityTypeConfiguration<ExamMark>
    {
        public void Configure(EntityTypeBuilder<ExamMark> builder)
        {
            builder.ToTable(EntityConstants.EXAM_MARK_ENTITY_TABLE_NAME);

            builder.HasKey(x => x.Id);

            //For Nullable Relationship with Exam Table for ExamMarks
            builder
                  .HasOne<Exam>(x => x.Exam)
                  .WithMany(r => r.ExamMarks)
                  .HasForeignKey(f => f.ExamId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired(true);

            //For Nullable Relationship with Exam Table for Student
            builder
                  .HasOne<Student>(x => x.Student)
                  .WithMany(r => r.ExamMarks)
                  .HasForeignKey(f => f.StudentId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired(true);

            //For Nullable Relationship with Exam Table for Student
            builder
                  .HasOne<Subject>(x => x.Subject)
                  .WithMany(r => r.ExamMarks)
                  .HasForeignKey(f => f.SubjectId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired(true);

            builder
                 .HasOne<AcademicLevel>(x => x.AcademicLevel)
                 .WithMany(r => r.ExamMarks)
                 .HasForeignKey(f => f.AcademicLevelId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired(true);

            builder.Property(x => x.Marks)
                    .HasPrecision(18, 2);

            builder.Property(x => x.Grade)
                   .IsRequired(false);

        }
    }
}
