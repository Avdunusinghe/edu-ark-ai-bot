using EduArk.Application.DTOs.CommonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduArk.Application.DTOs.SubjectDTOs
{
    public class SubjectMasterDataDTO
    {
        public SubjectMasterDataDTO()
        {
            SubjectTypes = new List<DropDownDTO>();
            ParentBasketSubjects = new List<DropDownDTO>();
            SubjectCategories = new List<DropDownDTO>();
            SubjectStreams = new List<DropDownDTO>();
            AcademicLevels = new List<DropDownDTO>();
        }
        public List<DropDownDTO>  SubjectTypes { get; set; }
        public List<DropDownDTO> ParentBasketSubjects { get; set; }
        public List<DropDownDTO> SubjectCategories { get; set; }
        public List<DropDownDTO> SubjectStreams { get; set; }
        public List<DropDownDTO> AcademicLevels { get; set; }
    }
}
