namespace EduArk.Domain.Entities.Tenant;

public class User : BaseAuditableEntity
{
    public User()
    {
        CreatedUsers = new HashSet<User>();
        UpdatedUsers = new HashSet<User>();

        CreatedUserRoles = new HashSet<UserRole>();
        UserRoles = new HashSet<UserRole>();

        CreatedUserLearningPlans = new HashSet<LearningPlan>();
        UpdatedUserLearningPlans = new HashSet<LearningPlan>();
        UpdatedUserRoles =  new HashSet<UserRole>();

        CreatedAssessments = new HashSet<Assessment>();
        UpdatedAssessments = new HashSet<Assessment>();

        CreatedMCQQuestions = new HashSet<MCQQuestions>();
        UpdatedMCQQuestions = new HashSet<MCQQuestions>();

        CreatedStructuredQuestions = new HashSet<StructuredQuestion>();
        UpdatedStructuredQuestions = new HashSet<StructuredQuestion>();

        CreatedEssayQuestions = new HashSet<EssayQuestion>();
        UpdatedEssayQuestions = new HashSet<EssayQuestion>();

        CreatedTopics = new HashSet<Topic>();
        UpdatedTopics = new HashSet<Topic>();

        CreatedStudents = new HashSet<Student>();
        UpdatedStudents = new HashSet<Student>();

        CreatedExams = new HashSet<Exam>();
        UpdatedExams = new HashSet<Exam>();

        AcademicLevelHeads = new HashSet<AcademicLevel>();
        CreatedAcademicLevels = new HashSet<AcademicLevel>();
        UpdatedAcademicLevels = new HashSet<AcademicLevel>();

        CreatedAcademicYears = new HashSet<AcademicYear>();
        UpdatedAcademicYears = new HashSet<AcademicYear>();

        CreatedSubjects = new HashSet<Subject>();
        UpdatedSubjects = new HashSet<Subject>();

        CreatedHeadOfDepartments = new HashSet<HeadOfDepartment>();
        UpdatedHeadOfDepartments = new HashSet<HeadOfDepartment>();

        SubjectTeachers = new HashSet<SubjectTeacher>();
        CreatedSubjectTeachers = new HashSet<SubjectTeacher>();
        UpdatedSubjectTeachers = new HashSet<SubjectTeacher>();

        CreatedClassSubjectTeachers = new HashSet<ClassSubjectTeacher>();
        UpdatedClassSubjectTeachers = new HashSet<ClassSubjectTeacher>();

        CreatedClassTeachers = new HashSet<ClassTeacher>();
        UpdatedClassTeachers = new HashSet<ClassTeacher>();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsUserConfirmed { get; set; }
    public string? UserConfirmationSecutiryCode { get; set; }
    public string? PasswordResetSecurityToken { get; set; }
    public string? ProfileImageUrl { get; set; }


    public virtual Student Student { get; set; }


    public virtual ICollection<User> CreatedUsers { get; set; }
    public virtual ICollection<User> UpdatedUsers { get; set; }


    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<UserRole> CreatedUserRoles { get; set; }
    public virtual ICollection<UserRole> UpdatedUserRoles { get; set; }


    //Lessons, LearningPlan, LessonTypeAudio, LessonTypeText, and LessonTypeVideo
    public virtual ICollection<Lesson> CreatedUserLessons { get; set; }
    public virtual ICollection<Lesson> UpdatedUserLessons { get; set; }

    public virtual ICollection<LearningPlan> CreatedUserLearningPlans { get; set; }
    public virtual ICollection<LearningPlan> UpdatedUserLearningPlans { get; set; }

    public virtual ICollection<LessonTypeText> CreatedUserLessonTypeTexts { get; set; }
    public virtual ICollection<LessonTypeText> UpdatedUserLessonTypeTexts { get; set; }

    public virtual ICollection<LessonTypeAudio> CreatedUserLessonTypeAudios { get; set; }
    public virtual ICollection<LessonTypeAudio> UpdatedUserLessonTypeAudios { get; set; }

    public virtual ICollection<LessonTypeVideo> CreatedUserLessonVideos { get; set; }
    public virtual ICollection<LessonTypeVideo> UpdatedUserLessonVideos { get; set; }

    //Assessment , MCQqusetions, Structured Questions , Essay Questions , topics
    public virtual ICollection<Assessment> CreatedAssessments { get; set; }
    public virtual ICollection<Assessment> UpdatedAssessments { get; set; }

    public virtual ICollection<MCQQuestions> CreatedMCQQuestions { get; set; }
    public virtual ICollection<MCQQuestions> UpdatedMCQQuestions { get; set; }

    public virtual ICollection<StructuredQuestion> CreatedStructuredQuestions { get; set; }
    public virtual ICollection<StructuredQuestion> UpdatedStructuredQuestions { get; set; }

    public virtual ICollection<EssayQuestion> CreatedEssayQuestions { get; set; }
    public virtual ICollection<EssayQuestion> UpdatedEssayQuestions { get; set; }

    public virtual ICollection<Topic> CreatedTopics { get; set; }
    public virtual ICollection<Topic> UpdatedTopics { get; set; }

    public virtual ICollection<Student> CreatedStudents { get; set; }
    public virtual ICollection<Student> UpdatedStudents { get; set; }

    public virtual ICollection<Exam> CreatedExams { get; set; }
    public virtual ICollection<Exam> UpdatedExams { get; set; }

    public virtual ICollection<AcademicLevel> AcademicLevelHeads { get; set; }
    public virtual ICollection<AcademicLevel> CreatedAcademicLevels { get; set; }
    public virtual ICollection<AcademicLevel> UpdatedAcademicLevels { get; set; }

    public virtual ICollection<AcademicYear> CreatedAcademicYears { get; set; }
    public virtual ICollection<AcademicYear> UpdatedAcademicYears { get; set; }

    public virtual ICollection<Class> CreatedClasses { get; set; }
    public virtual ICollection<Class> UpdatedClasses { get; set; }

    public virtual ICollection<Subject> CreatedSubjects { get; set; }
    public virtual ICollection<Subject> UpdatedSubjects { get; set; }

    public virtual ICollection<HeadOfDepartment> CreatedHeadOfDepartments { get; set; }
    public virtual ICollection<HeadOfDepartment> UpdatedHeadOfDepartments { get; set; }

    public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
    public virtual ICollection<SubjectTeacher> CreatedSubjectTeachers { get; set; }
    public virtual ICollection<SubjectTeacher> UpdatedSubjectTeachers { get; set; }

    public virtual ICollection<ClassSubjectTeacher> CreatedClassSubjectTeachers { get; set; }
    public virtual ICollection<ClassSubjectTeacher> UpdatedClassSubjectTeachers { get; set; }

    public virtual ICollection<ClassTeacher> CreatedClassTeachers { get; set; }
    public virtual ICollection<ClassTeacher> UpdatedClassTeachers { get; set; }
}
