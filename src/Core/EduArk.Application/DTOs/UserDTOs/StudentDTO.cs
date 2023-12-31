using EduArk.Domain.Enums;

namespace EduArk.Application.DTOs.UserDTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string AdmissionNo { get; set; }
        public string EmegencyContactNo1 { get; set; }
        public string EmegencyContactNo2 { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }


       


    }
}
