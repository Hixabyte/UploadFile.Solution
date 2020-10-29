﻿CREATE TABLE [Employee] (
	[Id]		INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_Employee PRIMARY KEY,
	[Title]		NVARCHAR(max) NOT NULL,
	[Forename]	NVARCHAR(max) NOT NULL,
	[Surname]	NVARCHAR(max) NOT NULL,
	[Gender]	NVARCHAR(max) NOT NULL,
	[BirthDate]	DATE NOT NULL,
	[Email]		NVARCHAR(max) NOT NULL,
	[Role]		NVARCHAR(max) NOT NULL,
	[Salary]	DECIMAL(13,2) NOT NULL,
)