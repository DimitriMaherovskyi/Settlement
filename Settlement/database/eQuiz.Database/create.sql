CREATE DATABASE [Settlement];
GO

USE [Settlement];

CREATE TABLE [tblUsers]
(
[UserId] INT NOT NULL IDENTITY (1, 1),
[FirstName] NVARCHAR(30) NOT NULL,
[LastName] NVARCHAR(30) NOT NULL,
[Institute] NVARCHAR(30) NOT NULL,
[UserName] NVARCHAR(20) NOT NULL,
[PasswordHash] NVARCHAR(50) NOT NULL,
[Email] NVARCHAR(40) NOT NULL,
[CreatedDate] DATETIME NOT NULL,
[LastLoginDate] DATETIME NULL,
[RoleId] INT NULL,
[Quote] INT NULL
CONSTRAINT [PK_tblUsers_ID] PRIMARY KEY ([UserId])
);

CREATE TABLE [tblRoles]
(
[RoleId] INT NOT NULL IDENTITY (1, 1),
[RoleName] nvarchar(30) NOT NULL
CONSTRAINT [PK_tblRoles_ID] PRIMARY KEY ([RoleId])
);

CREATE TABLE [tblHostel]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[Number] INT NOT NULL,
[Address] NVARCHAR(50) NULL,
[MonthPaymentSum] INT NOT NULL
CONSTRAINT [PK_tblHostel_ID] PRIMARY KEY ([Id])
);

CREATE TABLE [tblRoomType]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[Type] NVARCHAR(50) NOT NULL
CONSTRAINT [PK_tblRoomType_ID] PRIMARY KEY ([Id])
);

CREATE TABLE [tblRoom]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[HostelId] INT NOT NULL,
[RoomTypeId] INT NOT NULL,
[Number] INT NOT NULL,
[AmountPlaces] INT NOT NULL,
[RoomFloor] INT NOT NULL
CONSTRAINT [PK_tblRoom_ID] PRIMARY KEY ([Id])
);

CREATE TABLE [tblStudent]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[Firstname] NVARCHAR(50) NOT NULL,
[Fathername] NVARCHAR(50) NOT NULL,
[Surname] NVARCHAR(50) NOT NULL,
[GenderType] BIT NOT NULL,
[Insitute] NVARCHAR(50) NOT NULL,
[StudyGroup] NVARCHAR(50) NOT NULL,
[Status] BIT NOT NULL
CONSTRAINT [PK_tblStudent_ID] PRIMARY KEY ([Id])
);

CREATE TABLE [tblStudentRoom]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[StudentId] INT NOT NULL,
[RoomId] INT NOT NULL,
[DateIn] DATETIME2 NOT NULL,
[DateOut] DATETIME2 NOT NULL
CONSTRAINT [PK_tblStudentRoom_ID] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [tblResidence]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[Name] NVARCHAR(50) NOT NULL,
[Distance] INT NOT NULL
CONSTRAINT [PK_tblResidence_ID] PRIMARY KEY ([Id])
);

CREATE TABLE [tblStudentResidence]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[StudentId] INT NOT NULL,
[ResidenceId] INT NOT NULL
CONSTRAINT [PK_tblStudentResidence_ID] PRIMARY KEY ([Id])
);

CREATE TABLE [tblViolation]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[Name] NVARCHAR(50) NOT NULL,
[Penalty] INT NOT NULL
CONSTRAINT [PK_tblViolation_ID] PRIMARY KEY ([Id])
);

CREATE TABLE [tblStudentViolation]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[ViolationId] INT NOT NULL,
[StudentId] INT NOT NULL,
[Time] DATETIME NULL
CONSTRAINT [PK_tblStudentViolation_ID] PRIMARY KEY ([Id])
);

CREATE TABLE [tblPayment]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[StudentId] INT NOT NULL,
[HostelId] INT NOT NULL,
[Amount] INT NOT NULL,
[PaymentDate] DATETIME NOT NULL
CONSTRAINT [PK_tblPayment_ID] PRIMARY KEY ([Id])
);

CREATE TABLE [tblBenefit]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[Name] NVARCHAR(50) NOT NULL,
[Value] INT NOT NULL
CONSTRAINT [PK_tblBenefit_ID] PRIMARY KEY ([Id])
);

CREATE TABLE [tblStudentBenefit]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[BenefitId] INT NOT NULL,
[StudentId] INT NOT NULL,
CONSTRAINT [PK_tblStudentBenefit_ID] PRIMARY KEY ([Id])
);

CREATE TABLE [tblSettleRequest]
(
[Id] INT NOT NULL IDENTITY (1, 1),
[StudentId] INT NOT NULL,
[Status] BIT NOT NULL
CONSTRAINT [PK_tblSettleRequest_ID] PRIMARY KEY ([Id])
);

ALTER TABLE [tblSettleRequest] ADD CONSTRAINT [FK_tblSettleRequest_tblStudent] FOREIGN KEY ([StudentId]) REFERENCES [tblStudent](Id);

ALTER TABLE [tblPayment] ADD CONSTRAINT [FK_tblPayment_tblHostel] FOREIGN KEY ([HostelId]) REFERENCES [tblHostel]([Id]);

ALTER TABLE [tblUsers] ADD CONSTRAINT [FK_tblUsers_tblRoles] FOREIGN KEY ([RoleId]) REFERENCES [tblRoles]([RoleId]);

ALTER TABLE [tblStudentBenefit] ADD CONSTRAINT [FK_tblStudentBenefit_tblBenefit] FOREIGN KEY ([BenefitId]) REFERENCES [tblBenefit]([Id]);

ALTER TABLE [tblStudentBenefit] ADD CONSTRAINT [FK_tblStudentBenefit_tblStudent] FOREIGN KEY ([StudentId]) REFERENCES [tblStudent]([Id]);

ALTER TABLE [tblRoom] ADD CONSTRAINT [FK_tblRoom_tblRoomType] FOREIGN KEY ([RoomTypeId]) REFERENCES [tblRoomType]([Id]);

ALTER TABLE [tblRoom] ADD CONSTRAINT [FK_tblRoom_tblHostel] FOREIGN KEY ([HostelId]) REFERENCES [tblHostel]([Id]);

ALTER TABLE [tblStudentRoom] ADD CONSTRAINT [FK_tblStudentRoom_tblStudent] FOREIGN KEY ([StudentId]) REFERENCES [tblStudent]([Id]);

ALTER TABLE [tblStudentRoom] ADD CONSTRAINT [FK_tblStudentRoom_tblRoom] FOREIGN KEY ([RoomId]) REFERENCES [tblRoom]([Id]); 

ALTER TABLE [tblStudentViolation] ADD CONSTRAINT [FK_tblStudentViolation_tblStudent] FOREIGN KEY ([StudentId]) REFERENCES [tblStudent]([Id]);

ALTER TABLE [tblStudentViolation] ADD CONSTRAINT [FK_tblStudentViolation_tblViolation] FOREIGN KEY ([ViolationId]) REFERENCES [tblViolation]([Id]);

ALTER TABLE [tblStudentResidence] ADD CONSTRAINT [FK_tblStudentResidence_tblStudent] FOREIGN KEY ([StudentId]) REFERENCES [tblStudent]([Id]);

ALTER TABLE [tblStudentResidence] ADD CONSTRAINT [FK_tblStudentResidence_tblResidence] FOREIGN KEY ([ResidenceId]) REFERENCES [tblResidence]([Id]);

ALTER TABLE [tblPayment] ADD CONSTRAINT [FK_tblPayment_tblStudent] FOREIGN KEY ([StudentId]) REFERENCES [tblStudent]([Id]);

GO