using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class AcademicLevelConfiguration : IEntityTypeConfiguration<AcademicLevel>
    {
        public void Configure(EntityTypeBuilder<AcademicLevel> builder)
        {
            //Set AcademicLevel Table Name
            builder.ToTable(EntityConstants.ACADEMIC_LEVEL_ENTITY_TABLE_NAME);

            //Set AcademicLevel Table Primary Key
            builder.HasKey(x => x.Id);

            //For Relationship with User Table for AcademicLevelHeads
            builder.HasOne<User>(x => x.LevelHead)
                   .WithMany(u => u.AcademicLevelHeads)
                   .HasForeignKey(f => f.LevelHeadId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with User Table for Created User
            builder.HasOne<User>(x => x.CreatedByUser)
                   .WithMany(u => u.CreatedAcademicLevels)
                   .HasForeignKey(f => f.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For Relationship with User Table for Updated User
            builder.HasOne<User>(x => x.UpdatedByUser)
                   .WithMany(u => u.UpdatedAcademicLevels)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
