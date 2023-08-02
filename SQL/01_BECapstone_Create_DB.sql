USE [master]

IF db_id('BE-Capstone') IS NULl
  CREATE DATABASE [BE-Capstone]
GO

USE [BE-Capstone]
GO


DROP TABLE IF EXISTS [ExerciseReaction];
DROP TABLE IF EXISTS [Reaction];
DROP TABLE IF EXISTS [Comment];
DROP TABLE IF EXISTS [Exercise];
DROP TABLE IF EXISTS [RegimenAssignment];
DROP TABLE IF EXISTS [RegimenExercise];
DROP TABLE IF EXISTS [Regimen];
DROP TABLE IF EXISTS [Note];
DROP TABLE IF EXISTS [Message];
DROP TABLE IF EXISTS [Assignment];
DROP TABLE IF EXISTS [UserProfile];
DROP TABLE IF EXISTS [UserType];
GO


CREATE TABLE [UserType] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Type] nvarchar(20) NOT NULL
)

CREATE TABLE [Reaction] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Name] nvarchar(50) NOT NULL,
  [IconCode] nvarchar(255) NOT NULL
)

CREATE TABLE [Exercise] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Name] nvarchar(50) NOT NULL,
  [Type] nvarchar(50) NOT NULL,
  [Muscle]nvarchar(50) NOT NULL,
  [Instructions] text NOT NULL,
  [VideoLocation] text NOT NULL
)

CREATE TABLE [UserProfile] (
  [Id] integer PRIMARY KEY IDENTITY,
  [FirstName] nvarchar(50) NOT NULL,
  [LastName] nvarchar(50) NOT NULL,
  [Email] nvarchar(555) NOT NULL,
  [Password] nvarchar(555) NOT NULL,
  [CreateDateTime] datetime NOT NULL,
  [ImageLocation] nvarchar(255),
  [UserTypeId] integer NOT NULL

  CONSTRAINT [FK_User_UserType] FOREIGN KEY ([UserTypeId]) REFERENCES [UserType] ([Id]),
)

CREATE TABLE [PatientAssignment] (
  [Id] integer PRIMARY KEY IDENTITY,
  [PatientProfileId] integer NOT NULL,
  [ProviderProfileId] integer NOT NULL,
  [BeginDate] date NOT NULL,
  [EndDate] date

  CONSTRAINT [FK_PatientAssignment_UserProfile_Patient] FOREIGN KEY ([PatientProfileId]) REFERENCES [UserProfile] ([Id]),
  CONSTRAINT [FK_PatientAssignment_UserProfile_Provider] FOREIGN KEY ([ProviderProfileId]) REFERENCES [UserProfile] ([Id])
)

CREATE TABLE [Note] (
  [Id] integer PRIMARY KEY IDENTITY,
  [PatientProfileId] integer NOT NULL,
  [ProviderProfileId] integer NOT NULL,
  [Content] text NOT NULL,
  [CreateDateTime] datetime NOT NULL

  CONSTRAINT [FK_Note_UserProfile_Patient] FOREIGN KEY ([PatientProfileId]) REFERENCES [UserProfile] ([Id]),
  CONSTRAINT [FK_Note_UserProfile_Provider] FOREIGN KEY ([ProviderProfileId]) REFERENCES [UserProfile] ([Id])
)

CREATE TABLE [Regimen] (
  [Id] integer PRIMARY KEY IDENTITY,
  [ProviderProfileId] integer NOT NULL,
  [Title] nvarchar(255) NOT NULL,
  [Description] text,
  [CreateDateTime] datetime NOT NULL

  CONSTRAINT [FK_Regimen_UserProfile_Provider] FOREIGN KEY ([ProviderProfileId]) REFERENCES [UserProfile] ([Id])
)

CREATE TABLE [Message] (
  [Id] integer PRIMARY KEY IDENTITY,
  [ToId] integer NOT NULL,
  [FromId] integer NOT NULL,
  [Content] text NOT NULL,
  [CreateDateTime] datetime NOT NULL

  CONSTRAINT [FK_Message_UserProfile_To] FOREIGN KEY ([ToId]) REFERENCES [UserProfile] ([Id]),
  CONSTRAINT [FK_Message_UserProfile_From] FOREIGN KEY ([FromId]) REFERENCES [UserProfile] ([Id])
)

--Functions as a bridge table because there is a many to many relationship between regimen and exercise,
--thus the bridge table is needed to allow the provider to associate many exercises to a single regimen and 
--assign a signle exercise to many different regimens
CREATE TABLE [RegimenExercise] (
  [Id] integer PRIMARY KEY IDENTITY,
  [RegimenId] integer NOT NULL,
  [ExerciseId] integer NOT NULL

  CONSTRAINT [FK_RegimenExercise_Regimen_Regimen] FOREIGN KEY ([RegimenId]) REFERENCES [Regimen] ([Id]),
  CONSTRAINT [FK_RegimenExercise_Exercise_Exercise] FOREIGN KEY ([ExerciseId]) REFERENCES [Exercise] ([Id])
)


--Functions as a bridge table because there is a many to many relationship between regimens and users,
--thus the bridge table is needed to allow the provider to associate many regimens to a single user and 
--assign a single regimen to many different users
CREATE TABLE [RegimenAssignment] (
  [Id] integer PRIMARY KEY IDENTITY,
  [RegimenId] integer NOT NULL,
  [PatientProfileId] integer NOT NULL,
  [AssignmentDate] date NOT NULL

  CONSTRAINT [FK_RegimenAssignment_Regimen_Regimen] FOREIGN KEY ([RegimenId]) REFERENCES [Regimen] ([Id]),
  CONSTRAINT [FK_RegimenAssignment_UserProfile_Patient] FOREIGN KEY ([PatientProfileId]) REFERENCES [UserProfile] ([Id])
)


--utilizes regimenexercises bridge table to more easily reference the user, regimen, and exercises 
--this is meant to allow a user to react to one exercise without affecting other users exercises or regimen plans
CREATE TABLE [ExerciseReaction] (
  [Id] integer PRIMARY KEY IDENTITY,
  [RegimenExerciseId] integer NOT NULL,
  [ReactionId] integer NOT NULL

  CONSTRAINT [FK_ExerciseReaction_RegimenExercise] FOREIGN KEY ([RegimenExerciseId]) REFERENCES [RegimenExercise] ([Id]),
  CONSTRAINT [FK_ExerciseReaction_Reaction] FOREIGN KEY ([ReactionId]) REFERENCES [Reaction] ([Id])
)

--Only need to reference RegimenexerciseId for comments: 
--This is because it will link both the regimen and exercise allowing me to
--reference both the exercise and  regimen associated
CREATE TABLE [Comment] (
  [Id] integer PRIMARY KEY IDENTITY,
  [RegimenExerciseId] integer NOT NULL,
  [Content] text NOT NULL,
  [CreateDateTime] datetime NOT NULL

  CONSTRAINT [FK_Comment_RegimenExercise] FOREIGN KEY ([RegimenExerciseId]) REFERENCES [RegimenExercise] ([Id])
)
GO