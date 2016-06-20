USE [Settlement];
GO

SET IDENTITY_INSERT [tblHostel] ON;
INSERT INTO [tblHostel]([Id], [Number])
VALUES (1, 1),
		(2, 2),
		(3, 3);
SET IDENTITY_INSERT [tblHostel] OFF;

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

INSERT INTO [tblResidence] ([Id], [Name], [Distance])
VALUES (1, 'Rudno', 25),
		(2, 'Rivne', 200),
		(3, 'Frankove', 140),
		(4, 'Rudno', 25),
		(5, 'Ostrig', 250),
		(6, 'Rivne', 200),
		(7, 'Vynnyky', 15),
		(8, 'Slavske', 160),
		(9, 'Slavske', 160),
		(10, 'Truskavets', 100),
		(11, 'Lutzk', 150),
		(12, 'Odessa', 700)

SET IDENTITY_INSERT [tblViolation] ON;
INSERT INTO [tblViolation] ([Id], [StudentId], [Name], [Penalty])
VALUES (1, 1, 'Smoking weed', 10),
		(2, 1, 'Drunk', 5),
		(3, 7, 'Fight', 5)
SET IDENTITY_INSERT [tblViolation] OFF;

SET IDENTITY_INSERT [tblBenefit] ON;
INSERT INTO [tblBenefit] ([Id], [Name])
VALUES (1, 'Mountain place'),
		(2, 'Invalid 3 group')
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
VALUES (1, 1, 1, '2016-03-03', '2016-04-04'),
		(2, 2, 2, '2016-03-03', '2016-04-04'),
		(3, 3, 3, '2016-03-03', '2016-04-04'),
		(4, 4, 3, '2016-03-03', '2016-04-04'),
		(5, 5, 5, '2016-03-03', '2016-04-04'),
		(6, 6, 6, '2016-03-03', '2016-04-04'),
		(7, 7, 7, '2016-03-03', '2016-04-04'),
		(8, 8, 7, '2016-03-03', '2016-04-04'),
		(9, 9, 9, '2016-03-03', '2016-04-04'),
		(10, 10, 10, '2016-03-03', '2016-04-04'),
		(11, 11, 11, '2016-03-03', '2016-04-04'),
		(12, 12, 11, '2016-03-03', '2016-04-04');
SET IDENTITY_INSERT [tblStudentRoom] OFF;