using EduArk.Domain.Enums;
using Entities.Common.Enums;

namespace EduArk.Domain.Entities.Tenant
{
    public class Assessment : BaseAuditableEntity
    {
        public Assessment()
        {
            MCQQuestions = new HashSet<MCQQuestions>();
            StructuredQuestion = new HashSet<StructuredQuestion>();
            EssayQuestion = new HashSet<EssayQuestion>();
        }

        public string AssessmentName { get; set; }
        //public int SubjectId { get; set; }
        //public int LessonId { get; set; }
        public AssessmentType AssessmentType { get; set; }

      

        //public virtual Lesson Lesson { get; set; }

    

        public virtual ICollection<MCQQuestions> MCQQuestions { get; set; }
        public virtual ICollection<StructuredQuestion> StructuredQuestion { get; set; }
        public virtual ICollection<EssayQuestion> EssayQuestion { get; set; }
    }
}
