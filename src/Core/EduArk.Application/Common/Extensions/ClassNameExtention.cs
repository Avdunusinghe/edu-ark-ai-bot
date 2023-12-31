using EduArk.Application.DTOs.ClassNameDTOs;
using EduArk.Domain.Entities.Tenant;

namespace EduArk.Application.Common.Extensions
{
    public static class ClassNameExtention
    {
        public static ClassName ToEntity(this ClassNameDTO classNameDto, ClassName? className = null)
        {
            if (className is null) className = new ClassName();

            className.Name = classNameDto.Name;
            className.Description = classNameDto.Description;
            className.IsActive = true;

            return className;
        }

        public static ClassNameDTO ToClassNameDto(this ClassName className, ClassNameDTO? classNameDto = null)
        {
            if(classNameDto is null) classNameDto = new ClassNameDTO();

            classNameDto.Id = className.Id;
            classNameDto.Name = className.Name;
            classNameDto.Description = className.Description;
            classNameDto.CreatedByName = className.CreatedByUser.FirstName;
            classNameDto.UpdatedByName = className.UpdatedByUser.FirstName;
            classNameDto.CreatedDate = className.CreatedDate.ToString("MMM d, yyyy");
            classNameDto.UpdatedDate = className.UpdateDate.Value.ToString("MMM d, yyyy");

            return classNameDto;
        }
    }
}
