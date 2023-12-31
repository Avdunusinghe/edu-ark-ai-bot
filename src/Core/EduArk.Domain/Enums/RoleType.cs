using System.ComponentModel;

namespace EduArk.Domain.Enums
{
    public enum RoleType
    {
       
        [Description("Admin")]
        Admin = 1,

        [Description("Principle")]
        Principle = 2,

        [Description("Level Head")]
        LevelHead = 3,

        [Description("Head Of Deaprtment")]
        HOD = 4,

        [Description("Teacher")]
        Teacher = 5,

        [Description("Student")]
        Student = 6,

        [Description("Supper Admin")]
        SupperAdmin = 8,

    }
}

