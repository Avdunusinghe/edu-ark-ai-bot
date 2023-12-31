using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Set User Table Name
            builder.ToTable(EntityConstants.USER_ENTITY_TABLE_NAME);

            //Set User Table Primary Key
            builder.HasKey(x => x.Id);

            // Set Nullable Property User Table Property UserConfirmationSecutiryCode
            builder.Property(x=>x.UserConfirmationSecutiryCode)
                   .IsRequired(false);

            // Set Nullable Property User Table Property PasswordResetSecurityToken
            builder.Property(x => x.PasswordResetSecurityToken)
                   .IsRequired(false);

            //For Nullable Relationship with User Table for Created User
            builder
                  .HasOne<User>(x => x.CreatedByUser)
                  .WithMany(r => r.CreatedUsers)
                  .HasForeignKey(f => f.CreatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired(false);

            //For Nullable Relationship with User Table for Updated User
            builder
                 .HasOne<User>(x => x.UpdatedByUser)
                 .WithMany(r => r.UpdatedUsers)
                 .HasForeignKey(f => f.UpdatedByUserId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired(false);

            //For  Relationship with User Table for Student
            builder.HasOne<Student>(s => s.Student)
                   .WithOne(u => u.User)
                   .HasForeignKey<Student>(f => f.Id);

          

        }
    }
}
