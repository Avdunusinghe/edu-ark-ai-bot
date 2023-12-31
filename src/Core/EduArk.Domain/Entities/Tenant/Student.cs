using EduArk.Domain.Enums;

namespace EduArk.Domain.Entities.Tenant
{
    public class Student : BaseAuditableEntity
    {
        public Student()
        {
            StudentClasses = new HashSet<StudentClass>();
            SubjectTargetSettings = new HashSet<SubjectTargetSetting>();
            ExamMarks = new HashSet<ExamMark>();
        }
        public string AdmissionNo { get; set; }
        public string EmegencyContactNo1 { get; set; }
        public string EmegencyContactNo2 { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public bool PersonalMotivation { get; set; }
        public bool StudyEnvironment { get; set; }
        public bool TeacherInstructorQuality { get; set; }
        public bool PriorKnowledgeOfTheSubject { get; set; }
        public bool ClassAttendance { get; set; }
        public bool TimeManagementSkills { get; set; }
        public int ConfidentAcademicPerformance { get; set; }
        public int StudyHours { get; set; }
        

        public virtual User User { get; set; }

        public virtual ICollection<StudentClass> StudentClasses { get; set; }
        public virtual ICollection<SubjectTargetSetting> SubjectTargetSettings { get; set; }
        public virtual ICollection<ExamMark> ExamMarks { get; set; }
    }
}
