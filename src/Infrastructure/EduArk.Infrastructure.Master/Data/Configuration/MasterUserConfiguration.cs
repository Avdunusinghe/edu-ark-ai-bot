using EduArk.Domain.Entities.Master;
using EduArk.Infrastructure.Master.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Master.Data.Configuration
{
    public class MasterUserConfiguration : IEntityTypeConfiguration<MasterUser>
    {
        public void Configure(EntityTypeBuilder<MasterUser> builder)
        {
            //Set MasterUser Table Name
            builder.ToTable(EntityConstants.MASTER_USER_ENTITY_TABLE_NAME);

            //Set MasterUser Table Primary Key
            builder.HasKey(x => x.Id);


        }
    }
}
