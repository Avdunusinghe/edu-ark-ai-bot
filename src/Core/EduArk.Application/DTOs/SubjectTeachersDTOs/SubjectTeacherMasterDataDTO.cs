using EduArk.Application.DTOs.CommonDTOs;

namespace EduArk.Application.DTOs.SubjectTeachersDTOs
{
    public class SubjectTeacherMasterDataDTO
    {
        public SubjectTeacherMasterDataDTO()
        {
            AcademicYears = new List<DropDownDTO>();
            AcademicLevels = new List<DropDownDTO>();
        }

        public int CurrentAcademicYear { get; set; }
        public List<DropDownDTO> AcademicYears { get; set; }
        public List<DropDownDTO> AcademicLevels { get; set; }
    }
}
