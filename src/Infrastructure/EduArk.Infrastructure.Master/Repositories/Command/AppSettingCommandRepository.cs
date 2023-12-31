using EduArk.Domain.Entities.Master;
using EduArk.Domain.Repositories.Command.Master;
using EduArk.Infrastructure.Master.Data;
using EduArk.Infrastructure.Master.Repositories.Command.Base;

namespace EduArk.Infrastructure.Master.Repositories.Command
{
    public class AppSettingCommandRepository : CommandRepository<AppSetting>, IAppSettingCommandRepository
    {
        public AppSettingCommandRepository(MasterDbContext context)
           : base(context)
        {

        }
    }
}
