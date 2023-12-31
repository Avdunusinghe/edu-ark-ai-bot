namespace EduArk.Domain.Entities.Tenant
{
    public class ClassTeacher
    {
        public int ClassNameId { get; set; }
        public int AcademicLevelId { get; set; }
        public int AcademicYearId { get; set; }
        public int TeacherId { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedByUserId { get; set; }

        public virtual Class Class { get; set; }
        public virtual User Teacher { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User UpdatedByUser { get; set; }
    }
}
