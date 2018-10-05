CREATE TABLE [dbo].[Admin] (
    [id]       INT           IDENTITY (1, 1) NOT NULL,
    [Username] NVARCHAR (50) NULL,
    [Password] NVARCHAR (50) NULL,
    CONSTRAINT [PK_adm] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[Departments] (
    [Id]      INT          IDENTITY (1, 1) NOT NULL,
    [DepartmentName] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Staff] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50) NULL,
    [Surname]  NVARCHAR (50) NULL,
    [Address]  NVARCHAR (50) NULL,
    [Phone]    NVARCHAR (50) NULL,
    [Email]    NVARCHAR (50) NULL,
    [Password] NVARCHAR (50) NULL,
    CONSTRAINT [PK_yetkilibilgi] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Question] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [ChapterId]      INT            NULL,
    [QuestionTypeId] INT            NULL,
    [Name]           NVARCHAR (256) NULL,
    [IsActive]       BIT            CONSTRAINT [DF__Question__IsActi__1CF15040] DEFAULT ((1)) NULL,
    CONSTRAINT [PK__Question__3214EC070750936A] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Answer] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [QuestionId]   INT            NULL,
    [Name]         NVARCHAR (MAX) NULL,
    [IsCorrect]    BIT            DEFAULT ((0)) NULL,
    [DisplayOrder] INT            DEFAULT ((100)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__Answer__Question__239E4DCF] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question] ([Id])
);

CREATE TABLE [dbo].[Chapters] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CourseId]    INT            NULL,
    [ChapterNo]   INT            NULL,
    [ChapterName] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Chapters] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Courses] (
    [Courseid]     INT          IDENTITY (1, 1) NOT NULL,
    [DepartmentId] INT          NULL,
    [CourseName]       VARCHAR (50) NULL,
    [Description]  VARCHAR (50) NULL,
    CONSTRAINT [PK_courses] PRIMARY KEY CLUSTERED ([Courseid] ASC)
);

CREATE TABLE [dbo].[ExamPapers] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [StaffId] INT             NULL,
    [PaperName]  NVARCHAR (50)   NULL,
    [PaperValue] VARBINARY (MAX) NULL,
    CONSTRAINT [PK_ExamPapers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[QuestionType] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (256) NULL,
    [IsActive] BIT            DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Semester] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [SemesterName] VARCHAR (50) NULL,
    CONSTRAINT [PK_semster] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[StaffCourse] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [SemesterId] INT NULL,
    [StaffId] INT NULL,
    [CourseId]   INT NULL,
    CONSTRAINT [PK_PersonelsCourses] PRIMARY KEY CLUSTERED ([Id] ASC)
);






