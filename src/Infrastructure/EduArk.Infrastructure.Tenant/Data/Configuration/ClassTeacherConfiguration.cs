using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class ClassTeacherConfiguration : IEntityTypeConfiguration<ClassTeacher>
    {
        public void Configure(EntityTypeBuilder<ClassTeacher> builder)
        {
            //Set ClassTeacher Table Name
            builder.ToTable(EntityConstants.CLASS_TEACHER_ENTITY_TABLE_NAME);

            //Set ClassTeacher Table Primary Key
            builder.HasKey(x => new 
            { 
                x.ClassNameId, 
                x.AcademicLevelId, 
                x.AcademicYearId, 
                x.TeacherId 
            });

            //For  Relationship with ClassTeacher Table for Class
            builder.HasOne<Class>(c => c.Class)
                   .WithMany(ct => ct.ClassTeachers)
                   .HasForeignKey(f => new 
                   { 
                       f.ClassNameId, 
                       f.AcademicLevelId, 
                       f.AcademicYearId 
                   })
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For  Relationship with ClassTeacher Table for CreatedByUser
            builder.HasOne<User>(u => u.CreatedByUser)
                   .WithMany(ct => ct.CreatedClassTeachers)
                   .HasForeignKey(f => f.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For  Relationship with ClassTeacher Table for UpdatedByUser
            builder.HasOne<User>(u => u.UpdatedByUser)
                   .WithMany(ct => ct.UpdatedClassTeachers)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
