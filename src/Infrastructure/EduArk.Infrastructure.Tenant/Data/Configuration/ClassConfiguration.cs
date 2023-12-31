using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            //Set Class Table Name
            builder.ToTable(EntityConstants.CLASS_ENTITY_TABLE_NAME);

            //Set Class Table Primary Keys (Composite)
            builder.HasKey(x => new 
            { 
                x.ClassNameId, 
                x.AcademicLevelId, 
                x.AcademicYearId 
            });

            //For Relationship with Class Table for Class Name
            builder.HasOne<ClassName>(cn => cn.ClassName)
                   .WithMany(cl => cl.Classes)
                   .HasForeignKey(f => f.ClassNameId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with Class Table for Academic Level
            builder.HasOne<AcademicLevel>(al => al.AcademicLevel)
                   .WithMany(cl => cl.Classes)
                   .HasForeignKey(f => f.AcademicLevelId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with Class Table for Academic Year
            builder.HasOne<AcademicYear>(ay => ay.AcademicYear)
                   .WithMany(cl => cl.Classes)
                   .HasForeignKey(f => f.AcademicYearId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with Class Table for Created User
            builder.HasOne<User>(u => u.CreatedByUser)
                   .WithMany(cl => cl.CreatedClasses)
                   .HasForeignKey(f => f.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with Class Table for Updated User
            builder.HasOne<User>(u => u.UpdatedByUser)
                   .WithMany(cl => cl.UpdatedClasses)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
