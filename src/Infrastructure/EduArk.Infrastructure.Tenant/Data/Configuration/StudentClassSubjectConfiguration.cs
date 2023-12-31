using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class StudentClassSubjectConfiguration : IEntityTypeConfiguration<StudentClassSubject>
    {
        public void Configure(EntityTypeBuilder<StudentClassSubject> builder)
        {
            //Set StudentClassSubject Table Name
            builder.ToTable(EntityConstants.STUDENT_CLASS_SUBJECT_ENTITY_TABLE_NAME);

            //Set StudentClassSubject Table Primary Keys
            builder.HasKey(x => new 
            { 
                x.StudentId, 
                x.ClassNameId, 
                x.AcademicLevelId,
                x.AcademicYearId, 
                x.SubjectId 
            });

            //For Relationship with StudentClassSubject Table for StudentClass
            builder.HasOne<StudentClass>(x => x.StudentClass)
                .WithMany(sc => sc.StudentClassSubjects)
                .HasForeignKey(f => new 
                { 
                    f.StudentId, 
                    f.ClassNameId, 
                    f.AcademicLevelId, 
                    f.AcademicYearId 
                })
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            //For Relationship with StudentClassSubject Table for SubjectAcademicLevel
            builder.HasOne<SubjectAcademicLevel>(x => x.SubjectAcademicLevel)
                .WithMany(sc => sc.StudentClassSubjects)
                .HasForeignKey(f => new 
                { 
                    f.SubjectId, 
                    f.AcademicLevelId 
                })
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);
        }
    }
}
