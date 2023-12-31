using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class StudentClassConfiguration : IEntityTypeConfiguration<StudentClass>
    {
        public void Configure(EntityTypeBuilder<StudentClass> builder)
        {
            //Set StudentClass Table Name
            builder.ToTable(EntityConstants.STUDENT_CLASS_LEVEL_ENTITY_TABLE_NAME);

            //Set StudentClass Table Primary Key
            builder.HasKey(x => new 
            { 
                x.StudentId, 
                x.ClassNameId, 
                x.AcademicLevelId, 
                x.AcademicYearId 
            });

            //For Nullable Relationship with StudentClass Table for Student
            builder.HasOne<Student>(s => s.Student)
                .WithMany(sc => sc.StudentClasses)
                .HasForeignKey(fk => fk.StudentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            //For Nullable Relationship with StudentClass Table for Class
            builder.HasOne<Class>(cl => cl.Class)
                .WithMany(sc => sc.StudentClasses)
                .HasForeignKey(fk => new 
                { 
                    fk.ClassNameId, 
                    fk.AcademicLevelId, 
                    fk.AcademicYearId 
                })
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);
        }
    }
}
