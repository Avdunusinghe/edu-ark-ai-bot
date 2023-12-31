using EduArk.Domain.Enums;

namespace EduArk.Domain.Entities.Tenant
{
    public class SubjectTargetSetting : BaseEntity
    {
        public int SubjectId { get; set; }
        public int AcademicYearId { get; set; }
        public int AcademicLevelId { get; set; }
        public int SemesterId { get; set; }
        public int StudentId { get; set; }
        public decimal PredictedMark { get; set; }
        public decimal? TeacherTargetScore { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual AcademicYear AcademicYear { get; set; }
        public virtual AcademicLevel AcademicLevel { get; set; }
        public virtual Student Student { get; set; }

    }
}
