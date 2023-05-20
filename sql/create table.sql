CREATE TABLE departmentmembers
(
    id SERIAL NOT NULL PRIMARY KEY,
    firstname VARCHAR(15) NOT NULL,
    secondname VARCHAR(15) NOT NULL,
    middlename VARCHAR(15),
	email VARCHAR(30),
	phonenumber VARCHAR(16) NOT NULL
);

CREATE TABLE Faculty (
	id serial NOT NULL PRIMARY KEY,
	name VARCHAR(50) NOT NULL UNIQUE,
	departamentID integer NOT NULL REFERENCES departmentmembers(id)
);

CREATE TABLE Kafedra (
	id serial NOT NULL PRIMARY KEY,
	name VARCHAR(50) NOT NULL UNIQUE,
	departamentID integer NOT NULL REFERENCES departmentmembers(id),
	facultyID integer NOT NULL REFERENCES Faculty(ID)
);

CREATE TABLE Speciality (
	id serial NOT NULL PRIMARY KEY,
	name VARCHAR(50) NOT NULL UNIQUE,
	code VARCHAR(8) NOT NULL UNIQUE,
	facultyID integer NOT NULL REFERENCES Faculty(ID)
);

CREATE TABLE Discipline (
	id serial NOT NULL PRIMARY KEY,
	name VARCHAR(50) NOT NULL UNIQUE,
	kafedraID integer NOT NULL REFERENCES Kafedra(id)
);

CREATE TABLE DisciplineSpeciality (
	id serial NOT NULL PRIMARY KEY,
	DisciplineID integer NOT NULL REFERENCES Discipline(id),
	SpecialityID integer NOT NULL REFERENCES Speciality(id),
	lecHour integer,
	prHour integer,
	labs integer,
	semestr integer NOT NULL,
	sessia integer NOT NULL
);

drop table departmentmembers cascade;
drop table Faculty cascade;
drop table Kafedra cascade;
drop table Speciality cascade;
drop table Discipline cascade;
drop table DisciplineSpeciality cascade;


