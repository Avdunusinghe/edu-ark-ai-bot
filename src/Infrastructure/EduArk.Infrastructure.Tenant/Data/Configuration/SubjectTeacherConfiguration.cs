using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class SubjectTeacherConfiguration : IEntityTypeConfiguration<SubjectTeacher>
    {
        public void Configure(EntityTypeBuilder<SubjectTeacher> builder)
        {
            //Set SubjectTeacher Table Name
            builder.ToTable(EntityConstants.SUBJECT_TEACHER_ENTITY_TABLE_NAME);

            //Set SubjectTeacher Table Primary Key
            builder.HasKey(x => x.Id);

            //Set Nullable Property SubjectTeacher Table EndDate
            builder.Property(X => X.EndDate)
                   .IsRequired(false);

            //For Relationship with SubjectTeacher Table for Subject
            builder.HasOne<Subject>(sb => sb.Subject)
                   .WithMany(st => st.SubjectTeachers)
                   .HasForeignKey(f => f.SubjectId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with SubjectTeacher Table for AcademicYear
            builder.HasOne<AcademicYear>(ay => ay.AcademicYear)
                   .WithMany(st => st.SubjectTeachers)
                   .HasForeignKey(f => f.AcademicYearId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with SubjectTeacher Table for AcademicLevel
            builder.HasOne<AcademicLevel>(al => al.AcademicLevel)
                   .WithMany(st => st.SubjectTeachers)
                   .HasForeignKey(f => f.AcademicLevelId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with SubjectTeacher Table for Teacher
            builder.HasOne<User>(u => u.Teacher)
                   .WithMany(st => st.SubjectTeachers)
                   .HasForeignKey(f => f.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with SubjectTeacher Table for Created User
            builder.HasOne<User>(u => u.CreatedByUser)
                   .WithMany(st => st.CreatedSubjectTeachers)
                   .HasForeignKey(f => f.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with SubjectTeacher Table for Updated User
            builder.HasOne<User>(u => u.UpdatedByUser)
                   .WithMany(st => st.UpdatedSubjectTeachers)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
