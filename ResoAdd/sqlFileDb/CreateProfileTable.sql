create table if not exists Profile
(
	ProfileId serial primary key,
	UserId int,
	ProfileName varchar(50),
	FirstName varchar(50),
	LastName varchar(50),
	ProfileImg varchar(100)
)