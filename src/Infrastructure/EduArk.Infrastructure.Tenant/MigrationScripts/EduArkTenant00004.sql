BEGIN TRANSACTION;
GO

CREATE TABLE [AcademicLevel] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [LevelHeadId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_AcademicLevel] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AcademicLevel_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AcademicLevel_User_LevelHeadId] FOREIGN KEY ([LevelHeadId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AcademicLevel_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AcademicYear] (
    [Id] int NOT NULL,
    [IsCurrentYear] bit NOT NULL DEFAULT CAST(0 AS bit),
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_AcademicYear] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AcademicYear_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AcademicYear_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ClassName] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ClassName] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClassName_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]),
    CONSTRAINT [FK_ClassName_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id])
);
GO

CREATE TABLE [ExamType] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ExamType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Student] (
    [Id] int NOT NULL,
    [AdmissionNo] int NOT NULL,
    [EmegencyContactNo1] nvarchar(max) NOT NULL,
    [EmegencyContactNo2] nvarchar(max) NOT NULL,
    [Gender] int NOT NULL,
    [DateOfBirth] datetime2 NULL,
    [IsActive] bit NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NULL,
    CONSTRAINT [PK_Student] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Student_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Student_User_Id] FOREIGN KEY ([Id]) REFERENCES [User] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Student_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SubjectStream] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_SubjectStream] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Topic] (
    [Id] int NOT NULL IDENTITY,
    [LessonId] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [SequenceNo] int NOT NULL,
    [LearningExperience] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Topic] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Topic_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Topic_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Class] (
    [ClassNameId] int NOT NULL,
    [AcademicLevelId] int NOT NULL,
    [AcademicYearId] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [ClassCategory] int NOT NULL,
    [LanguageStream] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NOT NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Class] PRIMARY KEY ([ClassNameId], [AcademicLevelId], [AcademicYearId]),
    CONSTRAINT [FK_Class_AcademicLevel_AcademicLevelId] FOREIGN KEY ([AcademicLevelId]) REFERENCES [AcademicLevel] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Class_AcademicYear_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYear] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Class_ClassName_ClassNameId] FOREIGN KEY ([ClassNameId]) REFERENCES [ClassName] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Class_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Class_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Exam] (
    [Id] int NOT NULL IDENTITY,
    [Name] int NOT NULL,
    [AcademicYearId] int NOT NULL,
    [ExamTypeId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Exam] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Exam_ExamType_ExamTypeId] FOREIGN KEY ([ExamTypeId]) REFERENCES [ExamType] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Exam_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Exam_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Subject] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(450) NOT NULL,
    [SubjectCode] nvarchar(450) NOT NULL,
    [SubjectCategory] int NOT NULL,
    [IsParentBasketSubject] bit NOT NULL,
    [IsBuscketSubject] bit NOT NULL,
    [ParentBasketSubjectId] int NULL,
    [SubjectStreamId] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Subject] PRIMARY KEY ([Id]),
    CONSTRAINT [AK_Subject_Name] UNIQUE ([Name]),
    CONSTRAINT [AK_Subject_SubjectCode] UNIQUE ([SubjectCode]),
    CONSTRAINT [FK_Subject_SubjectStream_SubjectStreamId] FOREIGN KEY ([SubjectStreamId]) REFERENCES [SubjectStream] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Subject_Subject_ParentBasketSubjectId] FOREIGN KEY ([ParentBasketSubjectId]) REFERENCES [Subject] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Subject_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Subject_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Assessment] (
    [Id] int NOT NULL IDENTITY,
    [AssessmentName] nvarchar(max) NOT NULL,
    [AssessmentType] int NOT NULL,
    [TopicId] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Assessment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Assessment_Topic_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topic] ([Id]),
    CONSTRAINT [FK_Assessment_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Assessment_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ClassTeacher] (
    [ClassNameId] int NOT NULL,
    [AcademicLevelId] int NOT NULL,
    [AcademicYearId] int NOT NULL,
    [TeacherId] int NOT NULL,
    [IsPrimary] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdatedOn] datetime2 NOT NULL,
    [UpdatedByUserId] int NOT NULL,
    CONSTRAINT [PK_ClassTeacher] PRIMARY KEY ([ClassNameId], [AcademicLevelId], [AcademicYearId], [TeacherId]),
    CONSTRAINT [FK_ClassTeacher_Class_ClassNameId_AcademicLevelId_AcademicYearId] FOREIGN KEY ([ClassNameId], [AcademicLevelId], [AcademicYearId]) REFERENCES [Class] ([ClassNameId], [AcademicLevelId], [AcademicYearId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ClassTeacher_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ClassTeacher_User_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [User] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClassTeacher_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [StudentClass] (
    [StudentId] int NOT NULL,
    [ClassNameId] int NOT NULL,
    [AcademicYearId] int NOT NULL,
    [AcademicLevelId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_StudentClass] PRIMARY KEY ([StudentId], [ClassNameId], [AcademicLevelId], [AcademicYearId]),
    CONSTRAINT [FK_StudentClass_Class_ClassNameId_AcademicLevelId_AcademicYearId] FOREIGN KEY ([ClassNameId], [AcademicLevelId], [AcademicYearId]) REFERENCES [Class] ([ClassNameId], [AcademicLevelId], [AcademicYearId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_StudentClass_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SubjectAcademicLevel] (
    [SubjectId] int NOT NULL,
    [AcademicLevelId] int NOT NULL,
    CONSTRAINT [PK_SubjectAcademicLevel] PRIMARY KEY ([SubjectId], [AcademicLevelId]),
    CONSTRAINT [FK_SubjectAcademicLevel_AcademicLevel_AcademicLevelId] FOREIGN KEY ([AcademicLevelId]) REFERENCES [AcademicLevel] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SubjectAcademicLevel_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SubjectTeacher] (
    [Id] int NOT NULL IDENTITY,
    [AcademicLevelId] int NOT NULL,
    [AcademicYearId] int NOT NULL,
    [SubjectId] int NOT NULL,
    [TeacherId] int NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_SubjectTeacher] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubjectTeacher_AcademicLevel_AcademicLevelId] FOREIGN KEY ([AcademicLevelId]) REFERENCES [AcademicLevel] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SubjectTeacher_AcademicYear_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYear] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SubjectTeacher_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SubjectTeacher_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SubjectTeacher_User_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SubjectTeacher_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [EssayQuestion] (
    [Id] int NOT NULL IDENTITY,
    [AssessmentId] int NOT NULL,
    [SequenceNo] int NOT NULL,
    [Text] nvarchar(max) NOT NULL,
    [Marks] decimal(5,2) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_EssayQuestion] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EssayQuestion_Assessment_AssessmentId] FOREIGN KEY ([AssessmentId]) REFERENCES [Assessment] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EssayQuestion_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_EssayQuestion_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [MCQQuestions] (
    [Id] int NOT NULL IDENTITY,
    [AssessmentId] int NOT NULL,
    [SequenceNo] int NOT NULL,
    [Text] nvarchar(max) NOT NULL,
    [Marks] decimal(5,2) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_MCQQuestions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MCQQuestions_Assessment_AssessmentId] FOREIGN KEY ([AssessmentId]) REFERENCES [Assessment] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_MCQQuestions_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_MCQQuestions_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [StructuredQuestion] (
    [Id] int NOT NULL IDENTITY,
    [AssessmentId] nvarchar(max) NOT NULL,
    [SequenceNo] int NOT NULL,
    [Text] nvarchar(max) NOT NULL,
    [Marks] decimal(5,2) NOT NULL,
    [AssessmentId1] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_StructuredQuestion] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StructuredQuestion_Assessment_AssessmentId1] FOREIGN KEY ([AssessmentId1]) REFERENCES [Assessment] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_StructuredQuestion_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_StructuredQuestion_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [StudentClassSubject] (
    [StudentId] int NOT NULL,
    [ClassNameId] int NOT NULL,
    [AcademicYearId] int NOT NULL,
    [AcademicLevelId] int NOT NULL,
    [SubjectId] int NOT NULL,
    CONSTRAINT [PK_StudentClassSubject] PRIMARY KEY ([StudentId], [ClassNameId], [AcademicLevelId], [AcademicYearId], [SubjectId]),
    CONSTRAINT [FK_StudentClassSubject_StudentClass_StudentId_ClassNameId_AcademicLevelId_AcademicYearId] FOREIGN KEY ([StudentId], [ClassNameId], [AcademicLevelId], [AcademicYearId]) REFERENCES [StudentClass] ([StudentId], [ClassNameId], [AcademicLevelId], [AcademicYearId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_StudentClassSubject_SubjectAcademicLevel_SubjectId_AcademicLevelId] FOREIGN KEY ([SubjectId], [AcademicLevelId]) REFERENCES [SubjectAcademicLevel] ([SubjectId], [AcademicLevelId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ClassSubjectTeacher] (
    [Id] int NOT NULL IDENTITY,
    [ClassNameId] int NOT NULL,
    [AcademicLevelId] int NOT NULL,
    [AcademicYearId] int NOT NULL,
    [SubjectId] int NOT NULL,
    [SubjectTeacherId] int NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_ClassSubjectTeacher] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClassSubjectTeacher_Class_ClassNameId_AcademicLevelId_AcademicYearId] FOREIGN KEY ([ClassNameId], [AcademicLevelId], [AcademicYearId]) REFERENCES [Class] ([ClassNameId], [AcademicLevelId], [AcademicYearId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ClassSubjectTeacher_SubjectTeacher_SubjectTeacherId] FOREIGN KEY ([SubjectTeacherId]) REFERENCES [SubjectTeacher] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ClassSubjectTeacher_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ClassSubjectTeacher_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ClassSubjectTeacher_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [HeadOfDepartment] (
    [Id] int NOT NULL IDENTITY,
    [SubjectId] int NOT NULL,
    [AcademicLevelId] int NOT NULL,
    [AcademicYearId] int NOT NULL,
    [TeacherId] int NOT NULL,
    [HeadOfDepartmentId] int NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_HeadOfDepartment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_HeadOfDepartment_AcademicLevel_AcademicLevelId] FOREIGN KEY ([AcademicLevelId]) REFERENCES [AcademicLevel] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_HeadOfDepartment_AcademicYear_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYear] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_HeadOfDepartment_HeadOfDepartment_HeadOfDepartmentId] FOREIGN KEY ([HeadOfDepartmentId]) REFERENCES [HeadOfDepartment] ([Id]),
    CONSTRAINT [FK_HeadOfDepartment_SubjectTeacher_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [SubjectTeacher] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_HeadOfDepartment_Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_HeadOfDepartment_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_HeadOfDepartment_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_AcademicLevel_CreatedByUserId] ON [AcademicLevel] ([CreatedByUserId]);
GO

CREATE INDEX [IX_AcademicLevel_LevelHeadId] ON [AcademicLevel] ([LevelHeadId]);
GO

CREATE INDEX [IX_AcademicLevel_UpdatedByUserId] ON [AcademicLevel] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_AcademicYear_CreatedByUserId] ON [AcademicYear] ([CreatedByUserId]);
GO

CREATE INDEX [IX_AcademicYear_UpdatedByUserId] ON [AcademicYear] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_Assessment_CreatedByUserId] ON [Assessment] ([CreatedByUserId]);
GO

CREATE INDEX [IX_Assessment_TopicId] ON [Assessment] ([TopicId]);
GO

CREATE INDEX [IX_Assessment_UpdatedByUserId] ON [Assessment] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_Class_AcademicLevelId] ON [Class] ([AcademicLevelId]);
GO

CREATE INDEX [IX_Class_AcademicYearId] ON [Class] ([AcademicYearId]);
GO

CREATE INDEX [IX_Class_CreatedByUserId] ON [Class] ([CreatedByUserId]);
GO

CREATE INDEX [IX_Class_UpdatedByUserId] ON [Class] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_ClassName_CreatedByUserId] ON [ClassName] ([CreatedByUserId]);
GO

CREATE INDEX [IX_ClassName_UpdatedByUserId] ON [ClassName] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_ClassSubjectTeacher_ClassNameId_AcademicLevelId_AcademicYearId] ON [ClassSubjectTeacher] ([ClassNameId], [AcademicLevelId], [AcademicYearId]);
GO

CREATE INDEX [IX_ClassSubjectTeacher_CreatedByUserId] ON [ClassSubjectTeacher] ([CreatedByUserId]);
GO

CREATE INDEX [IX_ClassSubjectTeacher_SubjectId] ON [ClassSubjectTeacher] ([SubjectId]);
GO

CREATE INDEX [IX_ClassSubjectTeacher_SubjectTeacherId] ON [ClassSubjectTeacher] ([SubjectTeacherId]);
GO

CREATE INDEX [IX_ClassSubjectTeacher_UpdatedByUserId] ON [ClassSubjectTeacher] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_ClassTeacher_CreatedByUserId] ON [ClassTeacher] ([CreatedByUserId]);
GO

CREATE INDEX [IX_ClassTeacher_TeacherId] ON [ClassTeacher] ([TeacherId]);
GO

CREATE INDEX [IX_ClassTeacher_UpdatedByUserId] ON [ClassTeacher] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_EssayQuestion_AssessmentId] ON [EssayQuestion] ([AssessmentId]);
GO

CREATE INDEX [IX_EssayQuestion_CreatedByUserId] ON [EssayQuestion] ([CreatedByUserId]);
GO

CREATE INDEX [IX_EssayQuestion_UpdatedByUserId] ON [EssayQuestion] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_Exam_CreatedByUserId] ON [Exam] ([CreatedByUserId]);
GO

CREATE INDEX [IX_Exam_ExamTypeId] ON [Exam] ([ExamTypeId]);
GO

CREATE INDEX [IX_Exam_UpdatedByUserId] ON [Exam] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_HeadOfDepartment_AcademicLevelId] ON [HeadOfDepartment] ([AcademicLevelId]);
GO

CREATE INDEX [IX_HeadOfDepartment_AcademicYearId] ON [HeadOfDepartment] ([AcademicYearId]);
GO

CREATE INDEX [IX_HeadOfDepartment_CreatedByUserId] ON [HeadOfDepartment] ([CreatedByUserId]);
GO

CREATE INDEX [IX_HeadOfDepartment_HeadOfDepartmentId] ON [HeadOfDepartment] ([HeadOfDepartmentId]);
GO

CREATE INDEX [IX_HeadOfDepartment_SubjectId] ON [HeadOfDepartment] ([SubjectId]);
GO

CREATE INDEX [IX_HeadOfDepartment_TeacherId] ON [HeadOfDepartment] ([TeacherId]);
GO

CREATE INDEX [IX_HeadOfDepartment_UpdatedByUserId] ON [HeadOfDepartment] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_MCQQuestions_AssessmentId] ON [MCQQuestions] ([AssessmentId]);
GO

CREATE INDEX [IX_MCQQuestions_CreatedByUserId] ON [MCQQuestions] ([CreatedByUserId]);
GO

CREATE INDEX [IX_MCQQuestions_UpdatedByUserId] ON [MCQQuestions] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_StructuredQuestion_AssessmentId1] ON [StructuredQuestion] ([AssessmentId1]);
GO

CREATE INDEX [IX_StructuredQuestion_CreatedByUserId] ON [StructuredQuestion] ([CreatedByUserId]);
GO

CREATE INDEX [IX_StructuredQuestion_UpdatedByUserId] ON [StructuredQuestion] ([UpdatedByUserId]);
GO

CREATE UNIQUE INDEX [IX_Student_AdmissionNo] ON [Student] ([AdmissionNo]);
GO

CREATE INDEX [IX_Student_CreatedByUserId] ON [Student] ([CreatedByUserId]);
GO

CREATE INDEX [IX_Student_UpdatedByUserId] ON [Student] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_StudentClass_ClassNameId_AcademicLevelId_AcademicYearId] ON [StudentClass] ([ClassNameId], [AcademicLevelId], [AcademicYearId]);
GO

CREATE INDEX [IX_StudentClassSubject_SubjectId_AcademicLevelId] ON [StudentClassSubject] ([SubjectId], [AcademicLevelId]);
GO

CREATE INDEX [IX_Subject_CreatedByUserId] ON [Subject] ([CreatedByUserId]);
GO

CREATE INDEX [IX_Subject_ParentBasketSubjectId] ON [Subject] ([ParentBasketSubjectId]);
GO

CREATE INDEX [IX_Subject_SubjectStreamId] ON [Subject] ([SubjectStreamId]);
GO

CREATE INDEX [IX_Subject_UpdatedByUserId] ON [Subject] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_SubjectAcademicLevel_AcademicLevelId] ON [SubjectAcademicLevel] ([AcademicLevelId]);
GO

CREATE INDEX [IX_SubjectTeacher_AcademicLevelId] ON [SubjectTeacher] ([AcademicLevelId]);
GO

CREATE INDEX [IX_SubjectTeacher_AcademicYearId] ON [SubjectTeacher] ([AcademicYearId]);
GO

CREATE INDEX [IX_SubjectTeacher_CreatedByUserId] ON [SubjectTeacher] ([CreatedByUserId]);
GO

CREATE INDEX [IX_SubjectTeacher_SubjectId] ON [SubjectTeacher] ([SubjectId]);
GO

CREATE INDEX [IX_SubjectTeacher_TeacherId] ON [SubjectTeacher] ([TeacherId]);
GO

CREATE INDEX [IX_SubjectTeacher_UpdatedByUserId] ON [SubjectTeacher] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_Topic_CreatedByUserId] ON [Topic] ([CreatedByUserId]);
GO

CREATE INDEX [IX_Topic_UpdatedByUserId] ON [Topic] ([UpdatedByUserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230708183518_EduArkTenant00004', N'7.0.5');
GO

COMMIT;
GO

