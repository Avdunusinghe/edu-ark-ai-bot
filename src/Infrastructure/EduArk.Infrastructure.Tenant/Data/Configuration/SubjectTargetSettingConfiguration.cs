using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class SubjectTargetSettingConfiguration : IEntityTypeConfiguration<SubjectTargetSetting>
    {
        public void Configure(EntityTypeBuilder<SubjectTargetSetting> builder)
        {
            //Set SubjectTargetSetting Table Name
            builder.ToTable(EntityConstants.SUBJECT_TARGET_SETTING_ENTITY_TABLE_NAME);


            //Set SubjectTargetSetting Table Primary Key
            builder.HasKey(x => x.Id);

            //Set SubjectTargetSetting Table PredictedMark Property Constraints
            builder.Property(x => x.PredictedMark)
                   .HasPrecision(18, 2);

            //Set SubjectTargetSetting Table TeacherTargetScore  Property Constraints
            builder.Property(x => x.TeacherTargetScore)
                   .HasPrecision(18, 2)
                   .IsRequired(false);

            //For Relationship with SubjectTargetSetting Table for Subject
            builder.HasOne<Subject>(u => u.Subject)
                   .WithMany(cl => cl.SubjectTargetSettings)
                   .HasForeignKey(f => f.SubjectId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            // For Relationship with SubjectTargetSetting Table for AcademicYear
            builder.HasOne<AcademicYear>(u => u.AcademicYear)
                   .WithMany(cl => cl.SubjectTargetSettings)
                   .HasForeignKey(f => f.AcademicYearId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            // For Relationship with SubjectTargetSetting Table for AcademicLevel
            builder.HasOne<AcademicLevel>(u => u.AcademicLevel)
                   .WithMany(cl => cl.SubjectTargetSettings)
                   .HasForeignKey(f => f.AcademicLevelId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            // For Relationship with SubjectTargetSetting Table for Student
            builder.HasOne<Student>(u => u.Student)
                   .WithMany(cl => cl.SubjectTargetSettings)
                   .HasForeignKey(f => f.StudentId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
