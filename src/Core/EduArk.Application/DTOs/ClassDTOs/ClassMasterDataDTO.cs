using EduArk.Application.DTOs.CommonDTOs;

namespace EduArk.Application.DTOs.ClassDTOs
{
    public class ClassMasterDataDTO
    {
        public ClassMasterDataDTO()
        {

            AcademicYears = new List<DropDownDTO>();
            AcademicLevels = new List<DropDownDTO>();
            ClassNames = new List<DropDownDTO>();
            ClassCategories = new List<DropDownDTO>();
            LanguageStreams = new List<DropDownDTO>();
           
        }


        public int CurrentAcademicYear { get; set; }
        public List<DropDownDTO> AcademicYears { get; set; }
        public List<DropDownDTO> AcademicLevels { get; set; }
        public List<DropDownDTO> ClassNames { get; set; }
        public List<DropDownDTO> ClassCategories { get; set; }
        public List<DropDownDTO> LanguageStreams { get; set; }
        public List<DropDownDTO> AllTeachers { get; set; }
       
    }
}
