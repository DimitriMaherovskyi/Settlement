USE [Settlement];
GO

SET IDENTITY_INSERT [tblHostel] ON;
INSERT INTO [tblHostel]([Id], [Number], [MonthPaymentSum])
VALUES (1, 1, 100),
		(2, 2, 100),
		(3, 3, 150);
SET IDENTITY_INSERT [tblHostel] OFF;

SET IDENTITY_INSERT [tblRoles] ON;
INSERT INTO [tblRoles]([RoleId], [RoleName])
VALUES (1, 'Admin'),
		(2, 'Warden'),
		(3, 'Dean');
SET IDENTITY_INSERT [tblRoles] OFF;

SET IDENTITY_INSERT [tblUsers] ON;
INSERT INTO [tblUsers]([UserId], [FirstName], [LastName], [Institute], [UserName], [PasswordHash], [Email], [CreatedDate], [LastLoginDate], [RoleId], [Quote])
VALUES (1, 'Andriy', 'Shevchenko', 'andrewShevchenko', 'IKTA', '1bbd886460827015e5d605ed44252251', 'andrewShev@nulp.ua', GETDATE(),NULL, 1, 100), --Password: 11111111
		(2, 'Kateryna', 'Ivanchuk', 'kateIvanchuk', 'IKNI', '62a8489af34a1bb0f97d40485425c622', 'kateIvanchuk@nulp.ua', GETDATE(),NULL, 2, 70),   --Password: kate1408
		(3, 'Bogdan', 'Telnyah', 'bogdanTelnyah', 'IARX', '715822acce9cdbb622a62c9e305cd6b0', 'bogdanTelnyah@nulp.ua', GETDATE(),NULL, 3, 100);   --Password: !hotdog2376
SET IDENTITY_INSERT [tblUsers] OFF;

SET IDENTITY_INSERT [tblRoomType] ON;
INSERT INTO tblRoomType([Id], [Type])
VALUES (1, 'Living'),
		(2, 'Storage');
SET IDENTITY_INSERT [tblRoomType] OFF;

SET IDENTITY_INSERT [tblRoom] ON;
INSERT INTO tblRoom([Id], [HostelId], [RoomTypeId], [Number], [RoomFloor], [AmountPlaces])
VALUES (1, 1, 1, 101, 1, 4),
		(2, 1, 1, 102, 1, 4),
		(3, 1, 1, 103, 1, 4),
		(4, 1, 2, 104, 1, 0),
		(5, 2, 1, 101, 1, 4),
		(6, 2, 1, 102, 1, 4),
		(7, 2, 1, 103, 1, 4),
		(8, 2, 2, 104, 1, 0),
		(9, 3, 1, 101, 1, 4),
		(10, 3, 1, 102, 1, 4),
		(11, 3, 1, 103, 1, 4),
		(12, 3, 2, 104, 1, 0);
SET IDENTITY_INSERT [tblRoom] OFF;

SET IDENTITY_INSERT [tblStudent] ON;
INSERT INTO tblStudent([Id], [Firstname], [Fathername], [Surname], [GenderType], [Insitute], [StudyGroup], [Status])
VALUES (1, 'Alex', 'Alexeyevich', 'Flesh', 1, 'IKNI', 'KN-17', 1),
		(2, 'Oleg', 'Vasylovich', 'Yuriev', 1, 'IKNI', 'KN-27', 1),
		(3, 'Oxana', 'Alexeyevna', 'Kurie', 2, 'IKNI', 'KN-37', 1),
		(4, 'Alex', 'Alexeyevich', 'Bulhakov', 1, 'IKNI', 'KN-47', 1),
		(5, 'Petr', 'Dmytrovich', 'Chadskiy', 1, 'INEM', 'OA-11', 1),
		(6, 'Phil', 'Georgeivich', 'Collins', 1, 'INEM', 'OA-21', 1),
		(7, 'Axel', 'Romanovich', 'Rose', 1, 'INEM', 'OA-31', 1),
		(8, 'Joan', 'Alexeyevich', 'Rolling', 2, 'INEM', 'OA-41', 1),
		(9, 'John', 'Alexeyevich', 'Snow', 1, 'IPPT', 'KT-12', 1),
		(10, 'Sophia', 'Valereevna', 'Bach', 2, 'IPPT', 'KT-22', 1),
		(11, 'Oleg', 'Alexeyevich', 'Slash', 1, 'IPPT', 'KT-32', 1),
		(12, 'Vanko', 'Georgeivich', 'Pirelli', 1, 'IPPT', 'KT-42', 1);
SET IDENTITY_INSERT [tblStudent] OFF;

SET IDENTITY_INSERT [tblResidence] ON;
INSERT INTO [tblResidence] ([Id], [Name], [Distance])
VALUES (1, 'Rudno', 25),
		(2, 'Rivne', 200),
		(3, 'Frankove', 140),
		(4, 'Ostrig', 250),
		(5, 'Vynnyky', 15),
		(6, 'Slavske', 160),
		(7, 'Truskavets', 100),
		(8, 'Lutzk', 150),
		(9, 'Odessa', 700)
SET IDENTITY_INSERT [tblResidence] OFF;

SET IDENTITY_INSERT [tblStudentResidence] ON;
INSERT INTO [tblStudentResidence] ([Id], [StudentId], [ResidenceId])
VALUES (1, 1, 1),
		(2, 2, 2),
		(3, 3, 3),
		(4, 4, 1),
		(5, 5, 4),
		(6, 6, 2),
		(7, 7, 5),
		(8, 8, 6),
		(9, 9, 6),
		(10, 10, 7),
		(11, 11, 8),
		(12, 12, 9)
SET IDENTITY_INSERT [tblStudentResidence] OFF;

SET IDENTITY_INSERT [tblViolation] ON;
INSERT INTO [tblViolation] ([Id], [Name], [Penalty])
VALUES (1, 'Smoking weed', 10),
		(2, 'Drunk', 5),
		(3, 'Fight', 5)
SET IDENTITY_INSERT [tblViolation] OFF;

SET IDENTITY_INSERT [tblStudentViolation] ON;
INSERT INTO [tblStudentViolation] ([Id], [StudentId], [ViolationId], [Time])
VALUES (1, 1, 1, '2016-06-06'),
		(2, 1, 2, '2016-07-06'),
		(3, 7, 3, '2016-08-06')
SET IDENTITY_INSERT [tblStudentViolation] OFF;

SET IDENTITY_INSERT [tblBenefit] ON;
INSERT INTO [tblBenefit] ([Id], [Name], [Value])
VALUES (1, 'Mountain place', 100),
		(2, 'Invalid 3 group', 250)
SET IDENTITY_INSERT [tblBenefit] OFF;

SET IDENTITY_INSERT [tblStudentBenefit] ON;
INSERT INTO [tblStudentBenefit] ([Id], [StudentId], [BenefitId])
VALUES (1, 8, 1),
		(2, 9, 1),
		(3, 10, 1),
		(4, 3, 2)
SET IDENTITY_INSERT [tblStudentBenefit] OFF;

SET IDENTITY_INSERT [tblStudentRoom] ON;
INSERT INTO tblStudentRoom([Id], [StudentId], [RoomId], [DateIn], [DateOut])
VALUES (1, 1, 1, '2016-03-03', '2016-07-07'),
		(2, 2, 2, '2016-03-03', '2016-07-07'),
		(3, 3, 3, '2016-03-03', '2016-04-04'),
		(4, 4, 3, '2016-03-03', '2016-07-07'),
		(5, 5, 5, '2016-03-03', '2016-07-07'),
		(6, 6, 6, '2016-03-03', '2016-07-07'),
		(7, 7, 7, '2016-03-03', '2016-07-07'),
		(8, 8, 7, '2016-03-03', '2016-04-04')
SET IDENTITY_INSERT [tblStudentRoom] OFF;