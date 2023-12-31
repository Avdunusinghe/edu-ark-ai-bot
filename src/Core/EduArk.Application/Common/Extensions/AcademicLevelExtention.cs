using EduArk.Application.DTOs.AcademicLevelDTOs;
using EduArk.Application.DTOs.UserDTOs;
using EduArk.Domain.Entities.Tenant;

namespace EduArk.Application.Common.Extensions
{
    public static class AcademicLevelExtention
    {
        public static AcademicLevel ToEntity(this AcademicLevelDTO academicLevelDTO, AcademicLevel? academicLevel = null)
        {
            if (academicLevel == null)
            {
                academicLevel = new AcademicLevel();
            }

            academicLevel.Id = academicLevelDTO.Id;
            academicLevel.Name = academicLevelDTO.Name;
            academicLevel.LevelHeadId = academicLevelDTO.LevelHeadId;
            academicLevel.IsActive = true;

            return academicLevel;
        }

        public static AcademicLevelDetailsDTO ToAcademicLevelDetailsDto(this AcademicLevel academicLevel, AcademicLevelDetailsDTO? dto = null)
        {
            if (dto == null)
            {
                dto = new AcademicLevelDetailsDTO();
            }

            dto.Id = academicLevel.Id;
            dto.Name = academicLevel.Name;
            dto.LevelHeadId = academicLevel.LevelHeadId;
            dto.LevelHeadName = academicLevel.LevelHead.FirstName;
            dto.CreatedByName = academicLevel.CreatedByUser.FirstName;
            dto.UpdatedByName = academicLevel.UpdatedByUser.FirstName;
            dto.UpdatedDate = academicLevel.UpdateDate.Value.ToString("MMM d, yyyy");
            dto.CreatedDate = academicLevel.CreatedDate.ToString("MMM d, yyyy");

            return dto;
        }
    }
}
