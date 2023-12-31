using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class HeadOfDepatmentConfiguration : IEntityTypeConfiguration<HeadOfDepartment>
    {
        public void Configure(EntityTypeBuilder<HeadOfDepartment> builder)
        {
            //Set HeadOfDepartment Table Name
            builder.ToTable(EntityConstants.HEAD_OF_DEPARTMENT_ENTITY_TABLE_NAME);

            //Set HeadOfDepartment Table Primary Key
            builder.HasKey(x => x.Id);

            //For Relationship with HeadOfDepartment Table for Subject
            builder.HasOne<Subject>(sb => sb.Subject)
                   .WithMany(hod => hod.HeadOfDepartments)
                   .HasForeignKey(fk => fk.SubjectId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with HeadOfDepartment Table for AcademicLevel
            builder.HasOne<AcademicLevel>(al => al.AcademicLevel)
                   .WithMany(hod => hod.HeadOfDepartments)
                   .HasForeignKey(fk => fk.AcademicLevelId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with HeadOfDepartment Table for AcademicYear
            builder.HasOne<AcademicYear>(ay => ay.AcademicYear)
                   .WithMany(hod => hod.HeadOfDepartments)
                   .HasForeignKey(fk => fk.AcademicYearId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with HeadOfDepartment Table for SubjectTeacher
            builder.HasOne<SubjectTeacher>(st => st.SubjectTeacher)
                   .WithMany(hod => hod.HeadOfDepartments)
                   .HasForeignKey(fk => fk.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with HeadOfDepartment Table for CreatedByUser
            builder.HasOne<User>(u => u.CreatedByUser)
                   .WithMany(ct => ct.CreatedHeadOfDepartments)
                   .HasForeignKey(f => f.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with HeadOfDepartment Table for UpdatedByUser
            builder.HasOne<User>(u => u.UpdatedByUser)
                   .WithMany(ct => ct.UpdatedHeadOfDepartments)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
