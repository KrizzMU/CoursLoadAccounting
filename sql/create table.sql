CREATE TABLE departmentmembers
(
    id SERIAL NOT NULL PRIMARY KEY,
    firstname VARCHAR(30) NOT NULL,
    secondname VARCHAR(30) NOT NULL,
    middlename VARCHAR(30),
	email VARCHAR(100),
	phonenumber VARCHAR(16) NOT NULL
);

CREATE TABLE Faculty (
	id serial NOT NULL PRIMARY KEY,
	name VARCHAR(100) NOT NULL UNIQUE,
	departamentID integer NOT NULL REFERENCES departmentmembers(id) ON DELETE CASCADE
);

CREATE TABLE Kafedra (
	id serial NOT NULL PRIMARY KEY,
	name VARCHAR(100) NOT NULL UNIQUE,
	departamentID integer NOT NULL REFERENCES departmentmembers(id) ON DELETE CASCADE,
	facultyID integer NOT NULL REFERENCES Faculty(ID) ON DELETE CASCADE
);

CREATE TABLE Speciality (
	id serial NOT NULL PRIMARY KEY,
	name VARCHAR(100) NOT NULL UNIQUE,
	code VARCHAR(8) NOT NULL UNIQUE,
	facultyID integer NOT NULL REFERENCES Faculty(ID) ON DELETE CASCADE
);

CREATE TABLE Discipline (
	id serial NOT NULL PRIMARY KEY,
	name VARCHAR(100) NOT NULL UNIQUE,
	kafedraID integer NOT NULL REFERENCES Kafedra(id) ON DELETE CASCADE
);

CREATE TABLE DisciplineSpeciality (
	id serial NOT NULL PRIMARY KEY,
	DisciplineID integer NOT NULL REFERENCES Discipline(id) ON DELETE CASCADE,
	SpecialityID integer NOT NULL REFERENCES Speciality(id) ON DELETE CASCADE,
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


