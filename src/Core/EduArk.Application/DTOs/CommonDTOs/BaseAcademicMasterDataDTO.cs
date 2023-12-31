namespace EduArk.Application.DTOs.CommonDTOs
{
    public class BaseAcademicMasterDataDTO
    {
        public BaseAcademicMasterDataDTO()
        {
            AcademicYears = new List<DropDownDTO>();
            AcademicLevels = new List<DropDownDTO>();
            Semesters = new List<DropDownDTO>();
            ExamTypes = new List<DropDownDTO>();
        }

        public int CurrentAcademicYear { get; set; }
        public List<DropDownDTO> AcademicYears { get; set; }
        public List<DropDownDTO> AcademicLevels { get; set; }
        public List<DropDownDTO> Semesters { get; set; }
        public List<DropDownDTO> ExamTypes { get; set; }
    }
}
