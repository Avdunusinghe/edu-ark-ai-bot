using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class ClassSubjectTeacherConfiguration : IEntityTypeConfiguration<ClassSubjectTeacher>
    {
        public void Configure(EntityTypeBuilder<ClassSubjectTeacher> builder)
        {
            //Set ClassSubjectTeacher Table Name
            builder.ToTable(EntityConstants.CLASS_SUBJECT_TEACHER_ENTITY_TABLE_NAME);

            //Set ClassSubjectTeacher Table Primary Key
            builder.HasKey(x => x.Id);

            builder.HasOne<SubjectTeacher>(st => st.SubjectTeacher)
                   .WithMany(c => c.ClassSubjectTeachers)
                   .HasForeignKey(f => f.SubjectTeacherId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Nullable Relationship with ClassSubjectTeacher Table for Subject
            builder.HasOne<Subject>(sb => sb.Subject)
                   .WithMany(c => c.ClassSubjectTeachers)
                   .HasForeignKey(f => f.SubjectId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Nullable Relationship with ClassSubjectTeacher Table for Class
            builder.HasOne<Class>(c => c.Class)
                   .WithMany(cs => cs.ClassSubjectTeachers)
                   .HasForeignKey(f => new 
                   { 
                       f.ClassNameId, 
                       f.AcademicLevelId, 
                       f.AcademicYearId
                   })
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Nullable Relationship with ClassSubjectTeacher Table for Created User
            builder.HasOne<User>(u => u.CreatedByUser)
                   .WithMany(c => c.CreatedClassSubjectTeachers)
                   .HasForeignKey(f => f.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Nullable Relationship with ClassSubjectTeacher Table for Updated User
            builder.HasOne<User>(u => u.UpdatedByUser)
                   .WithMany(c => c.UpdatedClassSubjectTeachers)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
