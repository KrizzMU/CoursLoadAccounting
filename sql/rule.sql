CREATE VIEW Spec
AS
SELECT
s.name as "Специальность",
s.code as "Код",
f.name as "Факультет"
FROM Speciality s
JOIN Faculty f ON s.facultyID = f.id

CREATE RULE insert_spec_rule AS
    ON INSERT TO Spec
    DO INSTEAD (  
        INSERT INTO Speciality (name, code, facultyID)
        VALUES (CheckNameSpec(NEW."Специальность"), CheckCodeSpec(NEW."Код"), (SELECT id FROM Faculty WHERE name = NEW."Факультет"))
    );

    INSERT INTO Spec ("Специальность", "Код", "Факультет")
VALUES ('Новая специальность', '090310', 'Компьютерных наук');