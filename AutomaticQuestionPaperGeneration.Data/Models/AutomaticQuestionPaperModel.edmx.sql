
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/16/2019 22:00:58
-- Generated from EDMX file: C:\Users\Shreyas\source\repos\AutomaticQuestionPaperGeneration\AutomaticQuestionPaperGeneration.Data\Models\AutomaticQuestionPaperModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AutomaticQuestionPaper];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Semesters'
CREATE TABLE [dbo].[Semesters] (
    [SemesterId] int IDENTITY(1,1) NOT NULL,
    [SemesterName] nvarchar(max)  NOT NULL,
    [SemesterStartDate] datetime  NOT NULL,
    [SemesterEndDate] datetime  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [DepartmentId] int IDENTITY(1,1) NOT NULL,
    [DepartmentName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Staffs'
CREATE TABLE [dbo].[Staffs] (
    [StaffId] int IDENTITY(1,1) NOT NULL,
    [StaffFullName] nvarchar(max)  NOT NULL,
    [StaffAddress] nvarchar(max)  NOT NULL,
    [StaffGender] nvarchar(max)  NOT NULL,
    [StaffPhoneNumber] nvarchar(max)  NOT NULL,
    [StaffEmailAddress] nvarchar(max)  NOT NULL,
    [StaffPassword] nvarchar(max)  NOT NULL,
    [DepartmentId] int  NOT NULL,
    [Subject_SubjectId] int  NOT NULL
);
GO

-- Creating table 'Subjects'
CREATE TABLE [dbo].[Subjects] (
    [SubjectId] int IDENTITY(1,1) NOT NULL,
    [SubjectShortName] nvarchar(max)  NOT NULL,
    [SubjectDescription] nvarchar(max)  NOT NULL,
    [SubjectYear] nvarchar(max)  NOT NULL,
    [DepartmentId] int  NOT NULL
);
GO

-- Creating table 'Chapters'
CREATE TABLE [dbo].[Chapters] (
    [ChapterId] int IDENTITY(1,1) NOT NULL,
    [ChapterName] nvarchar(max)  NOT NULL,
    [ChapterUnit] int  NOT NULL,
    [ChapterNo] int  NOT NULL,
    [SubjectId] int  NOT NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [QuestionId] int IDENTITY(1,1) NOT NULL,
    [QuestionText] nvarchar(max)  NOT NULL,
    [QuestionDifficultyLevel] int  NOT NULL,
    [ChapterId] int  NOT NULL
);
GO

-- Creating table 'ExamPapers1'
CREATE TABLE [dbo].[ExamPapers1] (
    [ExamPaperId] int IDENTITY(1,1) NOT NULL,
    [SubjectId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [SemesterId] in table 'Semesters'
ALTER TABLE [dbo].[Semesters]
ADD CONSTRAINT [PK_Semesters]
    PRIMARY KEY CLUSTERED ([SemesterId] ASC);
GO

-- Creating primary key on [DepartmentId] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([DepartmentId] ASC);
GO

-- Creating primary key on [StaffId] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [PK_Staffs]
    PRIMARY KEY CLUSTERED ([StaffId] ASC);
GO

-- Creating primary key on [SubjectId] in table 'Subjects'
ALTER TABLE [dbo].[Subjects]
ADD CONSTRAINT [PK_Subjects]
    PRIMARY KEY CLUSTERED ([SubjectId] ASC);
GO

-- Creating primary key on [ChapterId] in table 'Chapters'
ALTER TABLE [dbo].[Chapters]
ADD CONSTRAINT [PK_Chapters]
    PRIMARY KEY CLUSTERED ([ChapterId] ASC);
GO

-- Creating primary key on [QuestionId] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([QuestionId] ASC);
GO

-- Creating primary key on [ExamPaperId] in table 'ExamPapers1'
ALTER TABLE [dbo].[ExamPapers1]
ADD CONSTRAINT [PK_ExamPapers1]
    PRIMARY KEY CLUSTERED ([ExamPaperId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DepartmentId] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [FK_DepartmentStaff]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[Departments]
        ([DepartmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentStaff'
CREATE INDEX [IX_FK_DepartmentStaff]
ON [dbo].[Staffs]
    ([DepartmentId]);
GO

-- Creating foreign key on [Subject_SubjectId] in table 'Staffs'
ALTER TABLE [dbo].[Staffs]
ADD CONSTRAINT [FK_StaffSubject]
    FOREIGN KEY ([Subject_SubjectId])
    REFERENCES [dbo].[Subjects]
        ([SubjectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffSubject'
CREATE INDEX [IX_FK_StaffSubject]
ON [dbo].[Staffs]
    ([Subject_SubjectId]);
GO

-- Creating foreign key on [SubjectId] in table 'Chapters'
ALTER TABLE [dbo].[Chapters]
ADD CONSTRAINT [FK_SubjectChapter]
    FOREIGN KEY ([SubjectId])
    REFERENCES [dbo].[Subjects]
        ([SubjectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubjectChapter'
CREATE INDEX [IX_FK_SubjectChapter]
ON [dbo].[Chapters]
    ([SubjectId]);
GO

-- Creating foreign key on [ChapterId] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_ChapterQuestion]
    FOREIGN KEY ([ChapterId])
    REFERENCES [dbo].[Chapters]
        ([ChapterId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChapterQuestion'
CREATE INDEX [IX_FK_ChapterQuestion]
ON [dbo].[Questions]
    ([ChapterId]);
GO

-- Creating foreign key on [SubjectId] in table 'ExamPapers1'
ALTER TABLE [dbo].[ExamPapers1]
ADD CONSTRAINT [FK_SubjectExamPaper]
    FOREIGN KEY ([SubjectId])
    REFERENCES [dbo].[Subjects]
        ([SubjectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubjectExamPaper'
CREATE INDEX [IX_FK_SubjectExamPaper]
ON [dbo].[ExamPapers1]
    ([SubjectId]);
GO

-- Creating foreign key on [DepartmentId] in table 'Subjects'
ALTER TABLE [dbo].[Subjects]
ADD CONSTRAINT [FK_DepartmentSubject]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[Departments]
        ([DepartmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentSubject'
CREATE INDEX [IX_FK_DepartmentSubject]
ON [dbo].[Subjects]
    ([DepartmentId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------