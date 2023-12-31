using System.ComponentModel;

namespace EduArk.Domain.Enums
{
    public enum UserActiveStatus
    {
        [Description("_All_")]
        All = 0,

        [Description("Active")]
        Active = 1,

        [Description("In Active")]
        Inactive = 2,
    }
}
