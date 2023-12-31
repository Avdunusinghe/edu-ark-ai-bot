using EduArk.Domain.Entities.Tenant;
using Microsoft.EntityFrameworkCore;

namespace EduArk.Application.Common.Interfaces
{
    public interface ITenantDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<UserRole> UserRoles { get; }

        DbSet<AcademicYear> AcademicYears { get; }
        DbSet<Assessment> Assessments { get; }
        DbSet<EssayQuestion> EssayQuestions { get; }
        DbSet<LearningPlan> LearningPlans { get; }
        DbSet<LessonTypeAudio> LessonTypeAudios { get; }
        DbSet<LessonTypeText> LessonTypeTexts { get; }
        DbSet<LessonTypeVideo> LessonTypeVideos { get; }
        DbSet<MCQQuestions> MCQQuestionss { get; }
        DbSet<StructuredQuestion> StructuredQuestions { get; }
        DbSet<Topic> Topics { get; }
        DbSet<Class> Classes { get; }
        DbSet<Subject> Subjecs { get; }
        DbSet<ClassName> ClassNames { get; }
        DbSet<SubjectTargetSetting> SubjectTargetSettings { get; }
        DbSet<Semester> Semesters { get; }
        DbSet<Exam> Exams { get; }
        DbSet<ExamType> ExamTypes { get; }
        DbSet<ExamMark> ExamMarkss { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);
        Task RetryOnExceptionAsync(Func<Task> func);
    }
}
