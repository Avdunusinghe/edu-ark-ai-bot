namespace EduArk.Domain.Entities.Tenant
{
    public class StudentClassSubject
    {
        public int StudentId { get; set; }
        public int ClassNameId { get; set; }
        public int AcademicYearId { get; set; }
        public int AcademicLevelId { get; set; }
        public int SubjectId { get; set; }

        public virtual StudentClass StudentClass { get; set; }
        public virtual SubjectAcademicLevel SubjectAcademicLevel { get; set; }
    }
}
