CREATE TABLE [dbo].[Trainee]
(
	TrainingId CHAR(4) REFERENCES Training(TrainingId),
	TraineeId INT REFERENCES Employee(EmpId),
	TraineeStatus CHAR(1) CHECK (TraineeStatus IN ('C','N')),
	Remarks VARCHAR(30),
	PRIMARY KEY(TrainingId, TraineeId)
)
