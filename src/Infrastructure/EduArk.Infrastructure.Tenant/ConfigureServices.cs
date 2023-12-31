using EduArk.Application.Common.Interfaces;
using EduArk.Domain.Repositories.Command.Base;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Domain.Repositories.Query.Base;
using EduArk.Domain.Repositories.Query.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Interceptors;
using EduArk.Infrastructure.Tenant.Repositories.Command;
using EduArk.Infrastructure.Tenant.Repositories.Command.Base;
using EduArk.Infrastructure.Tenant.Repositories.Query;
using EduArk.Infrastructure.Tenant.Repositories.Query.Base;
using EduArk.Infrastructure.Tenant.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddEduArkInfrastructureTenantServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<TenantDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("TenantConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ITenantDbContext>(provider => provider.GetRequiredService<TenantDbContext>());

            services.AddTransient<TenantDbContextInitialiser>();

            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));

            services.AddTransient<IAcademicLevelQueryRepository, AcademicLevelQueryRepository>();
            services.AddTransient<IAcademicLevelCommandRepository, AcademicLevelCommandRepository>();

            services.AddTransient<IUserQueryRepository, UserQueryRepository>();
            services.AddTransient<IUserCommandRepository, UserCommandRepository>();

            services.AddTransient<IRoleQueryRepository, RoleQueryRepository>();
            services.AddTransient<IRoleCommandRepository, RoleCommandRepository>();

            services.AddTransient<IUserRoleQueryRepository, UserRoleQueryRepository>();
            services.AddTransient<IUserRoleCommandRepository, UserRoleCommandRepository>();


            services.AddTransient<IAzureBlobStorageService, AzureBlobStorageService>();
            services.AddTransient<IEduArkMachineLearningAPIService, EduArkMachineLearningAPIService>();

            services.AddTransient<ILearningPlanQueryRepository, LearningPlanQueryRepository>();
            services.AddTransient<ILearningPlanCommandRepository, LearningPlanCommandRepository>();

            services.AddTransient<ILessonQueryRepository, LessonQueryRepository>();
            services.AddTransient<ILessonCommandRepository, LessonCommandRepository>();

            services.AddTransient<ILessonTypeAudioQueryRepository, LessonTypeAudioQueryRepository>();
            services.AddTransient<ILessonTypeAudioCommandRepository, LessonTypeAudioCommanRepository>();

            services.AddTransient<ILessonTypeTextQueryRepository, LessonTypeTextQueryRepository>();
            services.AddTransient<ILessonTypeTextCommandRepository, LessonTypeTextCommandRepository>();

            services.AddTransient<ILessonTypeVideoQueryRepository, LessonTypeVideoQueryRepository>();
            services.AddTransient<ILessonTypeVideoCommandRepository, LessonTypeVideoCommandRepository>();

            services.AddTransient<IAssessmentQueryRepository, AssessmentQueryRepository>();
            services.AddTransient<IAssessmentCommandRepository, AssessmentCommandRepository>();

            services.AddTransient<IEssayQuestionQueryRepository, EssayQuestionQueryRepository>();
            services.AddTransient<IEssayQuestionQueryRepository, EssayQuestionQueryRepository>();

            services.AddTransient<IMCQQuestionsQueryRepository, MCQQuestionsQueryRepository>();
            services.AddTransient<IMCQQuestionsCommandRepository, MCQQuestionsCommandRepository>();

            services.AddTransient<IStructuredQuestionQueryRepository, StructuredQuestionQueryRepository>();
            services.AddTransient<IStructuredQuestionCommandRepository, StructuredQuestionCommandRepository>();

            services.AddTransient<ISubjectQueryRepository, SubjectQueryRepository>();
            services.AddTransient<ISubjectCommandRepository, SubjectCommandRepository>();

            services.AddTransient<ISubjectStreamQueryRepository, SubjectStreamQueryRepository>();
            services.AddTransient<ISubjectStreamCommandRepository, SubjectStreamCommandRepository>();

            services.AddTransient<IClassNameQueryRepository, ClassNameQueryRepository>();
            services.AddTransient<IClassNameCommandRepository, ClassNameCommandRepository>();

            services.AddTransient<IClassQueryRepository, ClassQueryRepository>();
            services.AddTransient<IClassCommandRepository, ClassCommandRepository>();

            services.AddTransient<IAcademicYearQueryRepository, AcademicYearQueryRepository>();
            services.AddTransient<IAcademicYearCommandRepository, AcademicYearCommandRepository>();

            services.AddTransient<ISubjectAcademicLevelQueryRepository, SubjectAcademicLevelQueryRepository>();
            services.AddTransient<ISubjectAcademicLevelCommandRepository, SubjectAcademicLevelCommandRepository>();

            services.AddTransient<ISubjectTeacherQueryRepository, SubjectTeacherQueryRepository>();
            services.AddTransient<ISubjectTeacherCommandRepository, SubjectTeacherCommandRepository>();

            services.AddTransient<ISubjectTargetSettingQueryRepository, SubjectTargetSettingQueryRepository>();
            services.AddTransient<ISubjectTargetSettingCommandRepository, SubjectTargetSettingCommandRepository>();

            services.AddTransient<ISemesterQueryRepository, SemesterQueryRepository>();
            services.AddTransient<ISemesterCommandRepository, SemesterCommandRepository>();

            services.AddTransient<IStudentQueryRepository, StudentQueryRepository>();
            services.AddTransient<IStudentCommandRepository, StudentCommandRepository>();

            services.AddTransient<IStudentQueryRepository, StudentQueryRepository>();
            services.AddTransient<IStudentCommandRepository, StudentCommandRepository>();

            services.AddTransient<IStudentClassQueryRepository, StudentClassQueryRepository>();
            services.AddTransient<IStudentClassCommandRepository, StudentClassCommandRepository>();

            services.AddTransient<IExamQueryRepository, ExamQueryRepository>();
            services.AddTransient<IExamCommandRepository, ExamCommandRepository>();

            services.AddTransient<IExamMarkQueryRepository, ExamMarkQueryRepository>();
            services.AddTransient<IExamMarkCommandRepository, ExamMarkCommandRepository>();

            services.AddTransient<IExamTypeQueryRepository, ExamTypeQueryRepository>();


            return services;
        }
    }
}
