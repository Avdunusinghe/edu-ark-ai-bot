using EduArk.Application.Common.Interfaces;
using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Reflection;

namespace EduArk.Infrastructure.Tenant.Data
{
    public class TenantDbContext : DbContext, ITenantDbContext
    {
        private IDbContextTransaction dbContextTransaction;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
       
        public TenantDbContext()
        {
            
        }
        public TenantDbContext
            (DbContextOptions<TenantDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
        {
            
            this._auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
            
            
        }

      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => Debug.WriteLine(message))
           .EnableSensitiveDataLogging();
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
            base.OnConfiguring(optionsBuilder);

           
               optionsBuilder.UseSqlServer(@"Server=DESKTOP-D32ORJ1\SQLEXPRESS;TrustServerCertificate=True; Database=EduArk;User Id=sa;Password=1qaz2wsx@;");
                base.OnConfiguring(optionsBuilder);
           


           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }


        public DbSet<User> Users => Set<User>();

        public DbSet<Role> Roles => Set<Role>();

        public DbSet<UserRole> UserRoles => Set<UserRole>();

        public DbSet<Assessment> Assessments => Set<Assessment>();

        public DbSet<EssayQuestion> EssayQuestions => Set<EssayQuestion>();

        public DbSet<LearningPlan> LearningPlans => Set<LearningPlan>();

        public DbSet<LessonTypeAudio> LessonTypeAudios => Set<LessonTypeAudio>();

        public DbSet<LessonTypeText> LessonTypeTexts => Set<LessonTypeText>();

        public DbSet<LessonTypeVideo> LessonTypeVideos => Set<LessonTypeVideo>();

        public DbSet<MCQQuestions> MCQQuestionss => Set<MCQQuestions>();

        public DbSet<StructuredQuestion> StructuredQuestions => Set<StructuredQuestion>();

        public DbSet<Topic> Topics => Set<Topic>();

        public DbSet<Class> Classes => Set<Class>();

        public DbSet<Subject> Subjecs => Set<Subject>();

        public DbSet<ClassName> ClassNames => Set<ClassName>();

        public DbSet<SubjectTargetSetting> SubjectTargetSettings => Set<SubjectTargetSetting>();

        public DbSet<Semester> Semesters => Set<Semester>();

        public DbSet<Exam> Exams => Set<Exam>();

        public DbSet<ExamType> ExamTypes => Set<ExamType>();

        public DbSet<ExamMark> ExamMarkss => Set<ExamMark>();

        public DbSet<AcademicYear> AcademicYears => Set<AcademicYear>();

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            dbContextTransaction ??= await Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await SaveChangesAsync(cancellationToken);
                dbContextTransaction?.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                if (dbContextTransaction != null)
                {
                    DisposeTransaction();
                }
            }
        }

        public async Task RetryOnExceptionAsync(Func<Task> func)
        {
            await Database.CreateExecutionStrategy().ExecuteAsync(func);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await dbContextTransaction?.RollbackAsync(cancellationToken);
            }
            finally
            {
                DisposeTransaction();
            }
        }

        private void DisposeTransaction()
        {
            try
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
        }

      
    }
}
