-- создание таблицы пользователей
CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Login] nvarchar(50) not null,
	[Password] nvarchar(50) not null,
	[WorkerType] nvarchar(10) not null
)

--Добавление одного пользователя
INSERT INTO Users ([Id], [Login], [Password], [WorkerType]) VALUES (1, 'User1', 'qwerty', 'Manager')