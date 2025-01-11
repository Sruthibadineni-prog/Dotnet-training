CREATE TABLE [dbo].[Trainer]
(
	TrainerId CHAR(4) PRIMARY KEY,
	TrainerName VARCHAR(30) NOT NULL,
	TrainerType CHAR(1) CHECK (TrainerType IN ('I','E')),
	TrainerPhone CHAR(10),
	TrainerEmail VARCHAR(30)
)
