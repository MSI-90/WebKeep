CREATE TABLE SavedLinks
( 
	Id int IDENTITY(1,1) NOT NULL,
	Category nvarchar(50) NULL,
	Description nvarchar(200) NULL,
	Link text NULL,
	Date nvarchar(12) NULL,
	PRIMARY KEY (Id)
)