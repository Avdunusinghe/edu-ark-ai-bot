using EduArk.Domain.Entities.Master;
using EduArk.Domain.Repositories.Command.Base;

namespace EduArk.Domain.Repositories.Command.Master
{
    public interface IAppSettingCommandRepository : ICommandRepository<AppSetting>
    {
    }
}
