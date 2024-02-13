CREATE DATABASE B1Tasks;

USE B1Tasks;

CREATE TABLE ImportTable (
	Id UNIQUEIDENTIFIER,
    Date DATETIME,
    LathinString VARCHAR(10),
    RussianString NVARCHAR(10),
    IntNumber INT,
    DecimalNumber FLOAT
);