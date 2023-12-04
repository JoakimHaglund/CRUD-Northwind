select * from Customers;

if not exists (select  CompanyName from Customers where CompanyName = "Alfreds Futterkiste") 

insert into Customers(CustomerID, CompanyName) values ('lol','lol lolson')

SELECT CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax FROM Customers

INSERT INTO Customers (CustomerID, CompanyName) VALUES('uCHNK', 'Bat Soup Inc')
INSERT INTO Customers (CustomerID, CompanyName) VALUES('fuck', 'this shit')
INSERT INTO Customers (CustomerID, CompanyName, City) VALUES('fucks', 'this shits', 'hell')


SELECT *
FROM INFORMATION_SCHEMA.COLUMNS
WHERE table_name = 'Customers'
