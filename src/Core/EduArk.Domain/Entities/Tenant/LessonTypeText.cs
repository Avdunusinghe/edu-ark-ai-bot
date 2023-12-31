using Entities.Common.Enums;

namespace EduArk.Domain.Entities.Tenant
{
    public class LessonTypeText : BaseAuditableEntity
    {
        public int LessonId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public LessonType LessonType { get; set; }
        public string? TextContent { get; set; }

        public virtual Lesson Lesson { get; set; }

        //public virtual ICollection<Lesson>?Lessons { get; set; }
        //public virtual ICollection<LessonTypeText> Texts { get; set; }
    }
}
