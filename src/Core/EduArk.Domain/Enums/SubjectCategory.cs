using System.ComponentModel;

namespace EduArk.Domain.Enums
{
    public enum SubjectCategory
    {
        [Description("Primary School Subject")]
        PrimarySchoolSubject = 1,
        [Description("Junior School Subject")]
        JuniorSchoolSubject = 2,
        [Description("High School Subject")]
        HighSchoolSubject = 3
    }
}
