using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class AcademicYearConfiguration : IEntityTypeConfiguration<AcademicYear>
    {
        public void Configure(EntityTypeBuilder<AcademicYear> builder)
        {
            //Set AcademicYear Table Name
            builder.ToTable(EntityConstants.ACADEMIC_YEAR_ENTITY_TABLE_NAME);

            //Set AcademicYear Table Primary Key
            builder.HasKey(x => x.Id);

            //Set AcademicYear Table Primary Key ValueGeneratedNever
            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            //Set Default Value AcademicYear Table Attribute IsCurrentYear 
            builder.Property(x => x.IsCurrentYear)
                   .HasDefaultValue(false);

            //For Nullable Relationship with Exam Table for Created User
            builder.HasOne<User>(x => x.CreatedByUser)
                   .WithMany(u => u.CreatedAcademicYears)
                   .HasForeignKey(f => f.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Nullable Relationship with Exam Table for Updated User
            builder.HasOne<User>(x => x.UpdatedByUser)
                   .WithMany(u => u.UpdatedAcademicYears)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
