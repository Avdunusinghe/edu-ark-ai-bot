using EduArk.Domain.Entities.Master;
using EduArk.Infrastructure.Master.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Master.Data.Configuration
{
    public class TenantConfiguration : IEntityTypeConfiguration<TenantCompany>
    {
        public void Configure(EntityTypeBuilder<TenantCompany> builder)
        {
            //Set Tenant Table Name
            builder.ToTable(EntityConstants.TENANT_ENTITY_TABLE_NAME);

            //Set Nullable Property Tenant Table 
            builder
                 .Property(x => x.Logo)
                 .IsRequired(false);

            //Set Nullable Property Tenant Table 
            builder
                .Property(x => x.SMTPServer)
                .IsRequired(false);

            //Set Nullable Property Tenant Table 
            builder
                .Property(x => x.SMTPUsername)
                .IsRequired(false);

            //Set Nullable Property Tenant Table 
            builder
                 .Property(x => x.SMTPPassword)
                 .IsRequired(false);

            //Set Nullable Property Tenant Table 
            builder
                 .Property(x => x.SMTPFrom)
                 .IsRequired(false);

            //Set Nullable Property Tenant Table 
            builder
                 .Property(x => x.SMTPPort)
                 .IsRequired(false);

            //Set Nullable Property Tenant Table 
            builder
                 .Property(x => x.IsSMTPUseSSL)
                 .IsRequired(false);


            //Set Nullable Property Tenant Table 
            builder
                .Property(x => x.CreatedDate)
                .IsRequired(false);

            //Set Nullable Property Tenant Table 
            builder
                .Property(x => x.SpecialNotes)
                .IsRequired(false);


            //Set Default Value Property Tenant Table 
            builder
                .Property(x => x.TenantId)
                .HasDefaultValue(Guid.NewGuid());

            //Set Default Value Property Tenant Table 
            builder
                .Property(x => x.APIKey)
                .HasDefaultValue(Guid.NewGuid());

            //Set Default Value Property Tenant Table 
            builder
                .Property(x => x.SecretKey)
                .HasDefaultValue(Guid.NewGuid());
        }
    }
}
