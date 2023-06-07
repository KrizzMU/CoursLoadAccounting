CREATE VIEW uchet_fulltable
AS
select 
s.name as "Специальность",
d.name as "Дисциплина",
k.name as "Кафедра",
f.name as "Факультет",
CASE
	WHEN ds.lecHour = 0 OR ds.lecHour IS NULL
	then 'Нет'
	ElSE CAST(ds.lecHour AS CHAR(4))
END as "ЛК час",
CASE
	WHEN ds.prHour = 0 OR ds.prHour IS NULL
	then 'Нет'
	ElSE CAST(ds.prHour AS CHAR(4))
END as "ПР час",
CASE
	WHEN ds.labs = 0 OR ds.labs IS NULL
	then 'Нет'
	ElSE CAST(ds.labs AS CHAR(4))
END as "ЛР",
CAST(ds.semestr AS CHAR(4)) as "Семестр",
CASE
	WHEN ds.sessia = 1 then 'Зачет'
	WHEN ds.sessia = 2 then 'Экзамен'
END as "Сессия"
from DisciplineSpeciality ds
JOIN Discipline d ON d.id = ds.DisciplineID
JOIN Speciality s ON s.id = ds.SpecialityID
JOIN Kafedra k ON k.id = d.kafedraID
JOIN Faculty f ON f.id = s.facultyID

drop view uchet_fulltable

select 
*
from uchet_fulltable

--------

CREATE VIEW OrgStructur
AS
SELECT
k.name as "Кафедра",
CONCAT(d.secondname, ' ', d.firstname, ' ', d.middlename) as "ФИО Завкафедрой",
CASE
	WHEN d.email = ''
	THEN 'Нет почты'
	ELSE d.email
END as "Почта Завкафедрой",
d.phonenumber as "Тел Завкафедрой",
f.name as "Факультет",
	(SELECT 
	CONCAT(dt.secondname, ' ', dt.firstname, ' ', dt.middlename) as "ФИО Декана"
	FROM departmentmembers dt WHERE f.departamentID = dt.id)
FROM Kafedra k
JOIN departmentmembers d ON k.departamentID = d.id
JOIN Faculty f ON f.id = k.facultyID

-------------

CREATE VIEW Facultets
AS
SELECT
f.name as "Факультет",
CONCAT(d.secondname, ' ', d.firstname, ' ', d.middlename) as "ФИО Декана",
CASE
	WHEN d.email = ''
	THEN 'Нет почты'
	ELSE d.email
END as "Почта Декана",
d.phonenumber as "Тел Декана"
FROM Faculty f 
JOIN departmentmembers d ON f.departamentID = d.id

-------------

CREATE VIEW Members
AS
SELECT
CONCAT(secondname, ' ', firstname, ' ', middlename) as "ФИО",
CASE
	WHEN d.email = ''
	THEN 'Нет почты'
	ELSE d.email
END as "Почта",
phonenumber as "Телефон"
FROM departmentmembers d

------------

CREATE VIEW Discip
AS
SELECT
d.name as "Дисциплина",
k.name as "Кафедра"
FROM Discipline d
JOIN Kafedra k ON d.kafedraID = k.id


---------

CREATE VIEW Spec
AS
SELECT
s.name as "Специальность",
s.code as "Код",
f.name as "Факультет"
FROM Speciality s
JOIN Faculty f ON s.facultyID = f.id


------------------------

CREATE VIEW SubQueryFrom
AS
SELECT 
dm.firstname, 
dm.secondname, 
f.name AS faculty_name,
dm.email
FROM (
    SELECT *
    FROM departmentmembers
    WHERE email LIKE '%mail.ru'
) AS dm
JOIN Faculty AS f ON f.departamentID = dm.id

------------------------

CREATE VIEW SubQueryWhere
AS
SELECT 
firstname,
middlename,
email,
phonenumber
FROM departmentmembers
WHERE id = (
    SELECT departamentID
    FROM Faculty
    WHERE id = 1   
);

---------------------------

CREATE VIEW SubQueryCorrelated
AS
SELECT *
FROM departmentmembers dm
WHERE EXISTS (
    SELECT 1
    FROM Faculty f
    WHERE f.departamentID = dm.id    
) OR EXISTS (
	SELECT 1
    FROM kafedra k
    WHERE k.departamentID = dm.id  
);

-------------------------

CREATE VIEW CorrelatedDiscipline AS
SELECT d.name
FROM discipline d
WHERE EXISTS (
    SELECT 1
    FROM disciplineSpeciality ds
    WHERE ds.disciplineID = d.id
    AND ds.lecHour > 0
    AND ds.prHour > 0
);

-------------------------

CREATE VIEW CorrelatedMember AS
SELECT dm.firstname, dm.secondname
FROM departmentmembers dm
WHERE (
    SELECT SUM(ds.prHour)
    FROM disciplineSpeciality ds
    JOIN discipline d ON ds.DisciplineID = d.id
    JOIN kafedra k ON d.kafedraID = k.id
    WHERE k.departamentID = dm.id
) < (
    SELECT SUM(ds.lecHour)
    FROM disciplineSpeciality ds
    JOIN discipline d ON ds.DisciplineID = d.id
    JOIN kafedra k ON d.kafedraID = k.id
    WHERE k.departamentID = dm.id
);

-------------------------

CREATE VIEW QueryHaving
AS
select 
s.name as "Специальность",
COUNT(d.name) as "Количество дисциплин",
ROUND(AVG(ds.lecHour),2) as "Среднее время лекций",
ROUND(AVG(ds.prHour),2) as "Среднее время практик"
FROM DisciplineSpeciality ds
JOIN Discipline d ON d.id = ds.DisciplineID
JOIN Speciality s ON s.id = ds.SpecialityID
GROUP BY s.name
HAVING AVG(ds.lecHour) >= (SELECT ROUND(AVG(lecHour), 2) FROM DisciplineSpeciality)

------------------------------

CREATE VIEW QueryAny
AS
SELECT name, code
FROM Speciality
WHERE id = ANY (
    SELECT SpecialityID
    FROM DisciplineSpeciality
    WHERE lecHour > 80 OR prHour > 80
);

-----------------------------


CREATE VIEW QueryAll
AS
SELECT
s.name as "Специальность",
ds.lecHour,
CASE
	WHEN ds.sessia = 1 then 'Зачет'
	WHEN ds.sessia = 2 then 'Экзамен'
END as "Сессия"
FROM DisciplineSpeciality ds
JOIN Discipline d ON d.id = ds.DisciplineID
JOIN Speciality s ON s.id = ds.SpecialityID
WHERE ds.lecHour > ALL(
	SELECT AVG(lecHour) FROM DisciplineSpeciality 
)

	
	

