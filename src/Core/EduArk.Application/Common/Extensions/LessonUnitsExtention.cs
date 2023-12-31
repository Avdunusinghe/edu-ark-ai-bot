using EduArk.Application.DTOs.TenantDTOs;
using EduArk.Domain.Entities.Tenant;

namespace EduArk.Application.Common.Extensions
{
    public static class LessonUnitsExtention
    {
        public static Lesson ToEntity(this LessonDTO lessonDTO, Lesson lesson = null)
        {
            if (lesson == null)
            {
                lesson = new Lesson();
            }

            lesson.LessonName = lessonDTO.LessonName;
            lesson.LessonDescription = lessonDTO.LessonDescription;
            lesson.LessonGrade = lessonDTO.LessonGrade;
            lesson.LessonSubject = lessonDTO.LessonSubject;
            lesson.LessonStatus = lessonDTO.LessonStatus;

            return lesson;
        }
    }
}
