using EduArk.Domain.Enums;

namespace EduArk.Application.DTOs.SubjectDTOs
{
    public class SubjectDTO
    {
        public SubjectDTO()
        {
            SubjectAcademicLevels = new List<int>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SubjectCode { get; set; }
        public SubjectCategory SubjectCategory { get; set; }
        public SubjectType SubjectType { get; set; }
        public int? ParentBasketSubjectId { get; set; }
        public int SubjectStreamId { get; set; }
        public List<int> SubjectAcademicLevels { get; set; }
    }

}
