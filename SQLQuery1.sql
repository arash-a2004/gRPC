CREATE DATABASE ProductTest;

USE ProductTest;

CREATE TABLE [Product](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[productName] [NVARCHAR](50) NULL,
	[productCode] [NVARCHAR](50) NULL,
	[price] [NVARCHAR](MAX) NULL
);

SELECT * from [Product];

INSERT INTO Product (productName, productCode , price) VALUES ('cellphone','1', 12.2); 

--delete
DELETE FROM [Product] WHERE Id=6;

drop table [Product];

