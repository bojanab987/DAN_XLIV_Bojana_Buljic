IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Pizzeria')
CREATE DATABASE Pizzeria;
GO
USE Pizzeria
--dropping tables
IF OBJECT_ID('vwOrders') IS NOT NULL
DROP VIEW vwOrders;

IF OBJECT_ID('tblOrders') IS NOT NULL 
DROP TABLE tblOrders;

IF OBJECT_ID('tblMenu') IS NOT NULL 
DROP TABLE tblMenu;

--create tables
CREATE TABLE tblMenu(
	ItemId INT PRIMARY KEY IDENTITY(1,1),
	PizzaName VARCHAR(30) NOT NULL,
	Price INT NOT NULL
	);
CREATE TABLE tblOrders(
	OrderId INT PRIMARY KEY IDENTITY(1,1),
	Username CHAR(13) NOT NULL,
	ItemId INT FOREIGN KEY REFERENCES  tblMenu(ItemId) ON DELETE SET NULL,
	Amount INT NOT NULL,
	OrderDateTime DATE not null,
	OrderStatus VARCHAR(20) not null
);
GO
--create view
CREATE VIEW vwOrders
as
select o.OrderId, o.Username, o.ItemId, o.Amount, o.OrderDateTime, o.Amount * m.Price AS TotalPrice
from tblOrders o
inner join tblMenu m
on o.ItemId = m.ItemId;

GO
--Insert menu values into table
INSERT INTO tblMenu(PizzaName, Price)
VALUES ('Capricosa', 820), ('Venezia',950),('FrutiDiMari',1000),('Vegetariana',620),('QuatroStagione',990),('Margarita',600),('Proscuito',950);