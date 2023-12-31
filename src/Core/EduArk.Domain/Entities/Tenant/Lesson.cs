using Entities.Common.Enums;

namespace EduArk.Domain.Entities.Tenant
{
    public class Lesson : BaseAuditableEntity
    {
        public Lesson() 
        { 
            CreatedLessons =  new HashSet<Lesson>();
            UpdatedLessons = new HashSet<Lesson>();

            LessonTypes = new HashSet<Lesson>();

            LessonTypeAudios = new HashSet<LessonTypeAudio>();
            LessonTypeTexts = new HashSet<LessonTypeText>();
            LessonTypeVideos = new HashSet<LessonTypeVideo>();

        }

        public int Id { get; set; }
        public string? LessonName { get; set; }
        public string? LessonDescription { get; set; }
        public string? LessonGrade { get; set; }
        public string? LessonSubject { get; set; }
        public string? LessonStatus { get; set; }
        public LessonType? LessonType { get; set; }
        public string FileUploadUrl { get; set; }

        public virtual ICollection<Lesson> CreatedLessons { get; set; } 
        public virtual ICollection<Lesson> UpdatedLessons { get; set; }


        public virtual ICollection <Lesson>? LessonTypes { get; set; }  

        public virtual ICollection <LessonTypeAudio>? LessonTypeAudios { get; set; }
        public virtual ICollection <LessonTypeText>? LessonTypeTexts { get; set; } 
        public virtual ICollection <LessonTypeVideo>? LessonTypeVideos { get; set; }
    }
}
