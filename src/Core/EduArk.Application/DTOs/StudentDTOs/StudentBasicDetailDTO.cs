namespace EduArk.Application.DTOs.StudentDTOs
{
    public class StudentBasicDetailDTO 
    {
        public StudentBasicDetailDTO()
        {
            StudentAcademicBehavior = new StudentAcademicBehaviorDTO();
        }
        public string FullName { get; set; }
        public string ProfileUrl { get; set; }
        public string AdmissionNo { get; set; }
        public string Email { get; set; }
        public string EmegencyContactNo1 { get; set; }
        public string EmegencyContactNo2 { get; set; }

        public StudentAcademicBehaviorDTO StudentAcademicBehavior { get; set; }


    }
}
