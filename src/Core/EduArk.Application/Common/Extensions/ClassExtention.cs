using EduArk.Application.DTOs.ClassDTOs;
using EduArk.Domain.Entities.Tenant;

namespace EduArk.Application.Common.Extensions
{
    public static class ClassExtention
    {
        public static Class ToEntity(this ClassDTO classDto, Class? entity = null)
        {
            if (entity is null) entity = new Class();

            entity.AcademicLevelId = classDto.AcademicLevelId;
            entity.AcademicYearId = classDto.AcademicYearId;
            entity.ClassCategory = classDto.ClassCategoryId;
            entity.ClassNameId = classDto.ClassNameId;
            entity.Name = classDto.Name;
            entity.LanguageStream = classDto.LanguageStreamId;
            entity.IsActive = true;

            return entity;
        }

        public static ClassDetailDTO ToClassDetailDto(this Class entity, ClassDetailDTO? classDetailDto = null) 
        {
            if(classDetailDto is null) classDetailDto = new ClassDetailDTO();

            var classTeacher = entity.ClassTeachers.FirstOrDefault(x => x.IsPrimary == true);

            classDetailDto.AcademicLevelId = entity.AcademicLevelId;
            classDetailDto.AcademicYearId = entity.AcademicYearId;
            classDetailDto.ClassNameId = entity.ClassNameId;
            classDetailDto.ClassTeacherName = 
                    classTeacher != null ? $"{ classTeacher.Teacher.FirstName} {classTeacher.Teacher.LastName}" : string.Empty;
            classDetailDto.Name = entity.Name;
            classDetailDto.TotalStudentCount = entity.StudentClasses.Count();

            return classDetailDto;
        }

    }
}
