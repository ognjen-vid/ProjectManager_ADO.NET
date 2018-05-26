USE ProjectManagerDB;

DROP TABLE Active;
DROP TABLE Employee;
DROP TABLE Project;
DROP TABLE Firm;

CREATE TABLE Firm(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	name NVARCHAR(50),
);

CREATE TABLE Project(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	name NVARCHAR(50),
	firm_id INT,
	FOREIGN KEY (firm_id)
	REFERENCES Firm(id) ON DELETE CASCADE
);

CREATE TABLE Employee(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	name NVARCHAR(50),
	lastname NVARCHAR(50),
	firm_id INT,
	FOREIGN KEY (firm_id)
	REFERENCES Firm(id) ON DELETE CASCADE
);

CREATE TABLE Active(
	employee_id  INT,
	project_id  INT,
	PRIMARY KEY(employee_id, project_id),
); 

INSERT INTO Firm (name) VALUES 
	('VegaIT'), 
	('Google');

INSERT INTO Project (name, firm_id) VALUES
	('VegaProject1', 1),
	('VegaProject2', 1),
	('GoogleProject2', 2);

INSERT INTO Employee (name, lastName, firm_id) VALUES
	('VPera','Peric', 1),
	('VMika','Mikic', 1),
	('VSima','Simic', 1),
	('VDjoka','VDjokic', 1),
	('VRUda','VRUdic', 1),
	('VDjaja','Vdjidji', 1);

INSERT INTO Active (employee_id, project_id) VALUES
	(1, 1),
	(2, 1),
	(3, 1),
	(4, 1),
	(5, 1),
	(6, 1),
	(1, 2),
	(2, 2),
	(3, 2),
	(4, 2),
	(5, 2);