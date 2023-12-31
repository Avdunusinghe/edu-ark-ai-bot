using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            //Set UserRole Table Name
            builder.ToTable(EntityConstants.USER_ROLE_ENTITY_TABLE_NAME);

            //Set UserRole Table Primary Key
            builder.HasKey(x => new
            {
                x.RoleId,
                x.UserId
            });

            //For Relationship with UserRole Table for Role
            builder
                  .HasOne<Role>(x => x.Role)
                  .WithMany(x => x.UserRoles)
                  .HasForeignKey(fk => fk.RoleId)
                  .OnDelete(DeleteBehavior.Restrict);

            //For Relationship with UserRole Table for Created User
            builder
                  .HasOne<User>(x => x.CreatedByUser)
                  .WithMany(x => x.CreatedUserRoles)
                  .HasForeignKey(fk => fk.CreatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict);

            //For Relationship with UserRole Table for Updated User
            builder
                  .HasOne<User>(f => f.UpdatedByUser)
                  .WithMany(f => f.UpdatedUserRoles)
                  .HasForeignKey(f => f.UpdatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict);

            //For Relationship with UserRole Table for User
            builder
                 .HasOne<User>(x => x.User)
                 .WithMany(x => x.UserRoles)
                 .HasForeignKey(fk => fk.UserId)
                 .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
