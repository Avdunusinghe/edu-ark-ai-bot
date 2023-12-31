using System.ComponentModel;

namespace EduArk.Domain.Enums
{
    public enum SubjectType
    {
        [Description("Normal Subject")]
        NormalSubject = 1,
        [Description("Parent Basket Subject")]
        ParentBasketSubject = 2,
        [Description("Basket Subject")]
        BasketSubject = 3,
    }
}
