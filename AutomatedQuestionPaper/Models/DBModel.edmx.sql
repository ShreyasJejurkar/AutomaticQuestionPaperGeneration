
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/09/2018 19:47:08
-- Generated from EDMX file: C:\Users\Shreyas.SHREYAS\source\repos\AutomatedQuestionPaper\AutomatedQuestionPaper\Models\DBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [QuestionPaper];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Answer__Question__239E4DCF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answer] DROP CONSTRAINT [FK__Answer__Question__239E4DCF];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admin];
GO
IF OBJECT_ID(N'[dbo].[Answer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Answer];
GO
IF OBJECT_ID(N'[dbo].[Chapters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chapters];
GO
IF OBJECT_ID(N'[dbo].[Courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Courses];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[ExamPapers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExamPapers];
GO
IF OBJECT_ID(N'[dbo].[Question]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Question];
GO
IF OBJECT_ID(N'[dbo].[QuestionType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionType];
GO
IF OBJECT_ID(N'[dbo].[Semester]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Semester];
GO
IF OBJECT_ID(N'[dbo].[Staff]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Staff];
GO
IF OBJECT_ID(N'[dbo].[StaffCourse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffCourse];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL
);
GO

-- Creating table 'Answers'
CREATE TABLE [dbo].[Answers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuestionId] int  NULL,
    [Name] nvarchar(max)  NULL,
    [IsCorrect] bit  NULL,
    [DisplayOrder] int  NOT NULL
);
GO

-- Creating table 'Chapters'
CREATE TABLE [dbo].[Chapters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CourseId] int  NULL,
    [ChapterNo] int  NULL,
    [ChapterName] nvarchar(100)  NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [Courseid] int IDENTITY(1,1) NOT NULL,
    [DepartmentId] int  NULL,
    [CourseName] varchar(50)  NULL,
    [Description] varchar(50)  NULL,
    [Year] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DepartmentName] varchar(50)  NULL
);
GO

-- Creating table 'ExamPapers'
CREATE TABLE [dbo].[ExamPapers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StaffId] int  NULL,
    [PaperName] nvarchar(50)  NULL,
    [PaperValue] varbinary(max)  NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ChapterId] int  NULL,
    [QuestionTypeId] int  NULL,
    [Name] nvarchar(256)  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'QuestionTypes'
CREATE TABLE [dbo].[QuestionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'Semesters'
CREATE TABLE [dbo].[Semesters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SemesterName] varchar(50)  NULL
);
GO

-- Creating table 'Staffs'
CREATE TABLE [dbo].[Staffs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Surname] nvarchar(50)  NULL,
    [Address] nvarchar(50)  NULL,
    [Phone] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL
);
GO

-- Creating table 'StaffCourses'
CREATE TABLE [dbo].[StaffCourses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SemesterId] int  NULL,
    [StaffId] int  NULL,
    [CourseId] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Id] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [PK_Answers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Chapters'
ALTER TABLE [dbo].[Chapters]
ADD CONSTRAINT [PK_Chapters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Courseid] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([Courseid] ASC);
GO

-- Creating primary key on [Id] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExamPapers'
ALTER TABLE [dbo].[ExamPapers]
ADD CONSTRAINT [PK_ExamPapers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTypes'
ALTER TABLE [dbo].[QuestionTypes]
ADD CONSTRAINT [PK_QuestionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Semesters'
ALTER TABLE [dbo].[Semesters]
ADD CONSTRAINT [PK_Semesters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [PK_Staffs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StaffCourses'
ALTER TABLE [dbo].[StaffCourses]
ADD CONSTRAINT [PK_StaffCourses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [QuestionId] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK__Answer__Question__239E4DCF]
    FOREIGN KEY ([QuestionId])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Answer__Question__239E4DCF'
CREATE INDEX [IX_FK__Answer__Question__239E4DCF]
ON [dbo].[Answers]
    ([QuestionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------