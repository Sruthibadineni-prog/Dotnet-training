CREATE TABLE [dbo].[Technology]
(
	TechId CHAR(4) PRIMARY KEY,
	TechTitle VARCHAR(20) NOT NULL,
	TechLevel CHAR(1) CHECK (TechLevel IN ('B','I','A')),
	Duration INT
)
