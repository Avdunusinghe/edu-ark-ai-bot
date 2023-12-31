using EduArk.Domain.Entities.Master;
using EduArk.Infrastructure.Master.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Master.Data.Configuration
{
    public class AppSettingConfiguration : IEntityTypeConfiguration<AppSetting>
    {
        public void Configure(EntityTypeBuilder<AppSetting> builder)
        {
            //Set AppSetting Table Name

            builder.ToTable(EntityConstants.APP_SETTING_ENTITY_TABLE_NAME);

            //Set AppSetting Table Primary Key

            builder.HasKey(x => x.Key);
        }
    }
}
