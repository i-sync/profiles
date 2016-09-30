use profile;

Create Table `Profile`
(
	ID int primary key auto_increment,
    `Name` varchar(64),
    NickName varchar(64),
    Avatar varchar(128),
    Email varchar(64),
    Phone varchar(32),
    Address varchar(128),
    Intro varchar(4096),
    AddDate datetime,
    UpdateDate datetime,
    `Password` varchar(256)
)DEFAULT CHARACTER SET=utf8;


Create Table Skill
(
	ID int primary key auto_increment,
    PID int ,
    Title varchar(1024),
    Content varchar(4096)
)DEFAULT CHARACTER SET=utf8;

Create Table Experience
(
	ID int primary key auto_increment,
	PID int,
	Title varchar(256),
	Company varchar(256),
	Link varchar(128),
	Period varchar(256),
	Location varchar(256),
	Position varchar(256),
	Intro varchar(4096)
)DEFAULT CHARACTER SET=utf8;

Create Table Project 
(
	ID int primary key auto_increment,
    PID int,
    Title varchar(512),
    Image varchar(128),
    Link varchar(128),
    Tags varchar(256),
    Intro varchar(4096)
)DEFAULT CHARACTER SET=utf8;

Create Table Education
(
	ID int primary key auto_increment,
    PID int,
    Title varchar(256),
    Period varchar(256),
    Professional varchar(128),
    Link varchar(128),
    Intro varchar(4096)
)DEFAULT CHARACTER SET=utf8;

Create Table Living 
(
	ID int primary key auto_increment,
    PID int,
    Title varchar(256),
    Content varchar(4096)
)DEFAULT CHARACTER SET=utf8;

Create Table Link
(
	ID int primary key auto_increment,
    PID int,
    Title varchar(256),
    Icon varchar(64),
    Link varchar(128),
    Logo varchar(128) 
)DEFAULT CHARACTER SET=utf8;

