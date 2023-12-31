using System.ComponentModel;

namespace EduArk.Domain.Enums
{
    public enum TenantTypeStatus
    {
        [Description("Public")]
        Public = 1,

        [Description("Private")]
        Private = 2,
    }
}
