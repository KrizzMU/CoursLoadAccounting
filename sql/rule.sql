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

CREATE RULE update_spec_rule AS
    ON UPDATE TO Spec
    DO INSTEAD (
        UPDATE Speciality
        SET name = CheckNameSpec(NEW."Специальность"), code = CheckCodeSpec(NEW."Код"), facultyID = (SELECT id FROM Faculty WHERE name = NEW."Факультет")
        WHERE id = (SELECT id FROM Speciality WHERE name = OLD."Специальность" AND code = OLD."Код" AND facultyID = (SELECT id FROM Faculty WHERE name = OLD."Факультет"))
    );


CREATE RULE delete_spec_rule AS
    ON DELETE TO Spec
    DO INSTEAD (
        DELETE FROM Speciality
        WHERE code = OLD."Код"
    );