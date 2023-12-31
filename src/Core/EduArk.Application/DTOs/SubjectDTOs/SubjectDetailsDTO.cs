using EduArk.Domain.Enums;

namespace EduArk.Application.DTOs.SubjectDTOs
{
    public class SubjectDetailsDTO
    {
        public SubjectDetailsDTO()
        {
            SubjectAcademicLevels = new List<int>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SubjectCode { get; set; }
        public SubjectCategory SubjectCategory { get; set; }
        public int CategorysId { get; set; }
        public string SubjectCategoryName { get; set; }
        public SubjectType SubjectType { get; set; }
        public int? ParentBasketSubjectId { get; set; }
        public string ParentBasketSubjectName { get; set; }
        public int SubjectStreamId { get; set; }
        public string SubjectStreamName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedByName { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedByName { get; set; }
       
       
        public List<int> SubjectAcademicLevels { get; set; }
    }
}
