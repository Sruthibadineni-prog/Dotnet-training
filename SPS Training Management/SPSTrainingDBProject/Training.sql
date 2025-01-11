CREATE TABLE [dbo].[Training]
(
	TrainingId CHAR(4) PRIMARY KEY,
	TechId CHAR(4) REFERENCES Technology(TechId),
	TrainerId CHAR(4) REFERENCES Trainer(TrainerId),
	StartDate DATETIME,
	EndDate DATETIME
)
