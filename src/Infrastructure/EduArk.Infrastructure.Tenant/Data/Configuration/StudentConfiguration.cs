using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //Set Student Table Name
            builder.ToTable(EntityConstants.STUDENT_ENTITY_TABLE_NAME);

            //Set Student Table Primary Key
            builder.HasKey(t => t.Id);

            //Set Student Table AdmissionNo IsUnique
            builder.HasIndex(x => x.AdmissionNo)
                   .IsUnique();

            //Set Student Table Property StudyHours
            builder.Property(p => p.StudyHours)
                   
                   .HasDefaultValue(0);

            //Set Student Table  Property ImportantFactorsAcademicPerformance
            builder.Property(p => p.PersonalMotivation)
                    .HasDefaultValue(false);

            builder.Property(p => p.StudyEnvironment)
                   .HasDefaultValue(false);

            builder.Property(p => p.TeacherInstructorQuality)
                   .HasDefaultValue(false);

            builder.Property(p => p.PriorKnowledgeOfTheSubject)
                   .HasDefaultValue(false);

            builder.Property(p => p.TimeManagementSkills)
                   .HasDefaultValue(false);

            builder.Property(p => p.ClassAttendance)
                   .HasDefaultValue(false);

            //Set Student Table Property ImportantFactorsAcademicPerformance
            builder.Property(p => p.ConfidentAcademicPerformance)
                  
                   .HasDefaultValue(0);

            // For Nullable Relationship with User Table for Created User
            builder
                  .HasOne<User>(x => x.CreatedByUser)
                  .WithMany(r => r.CreatedStudents)
                  .HasForeignKey(f => f.CreatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired(false);

            //For Nullable Relationship with User Table for Updated User
            builder
                 .HasOne<User>(x => x.UpdatedByUser)
                 .WithMany(r => r.UpdatedStudents)
                 .HasForeignKey(f => f.UpdatedByUserId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired(false);


        }
    }
}
