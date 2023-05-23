CREATE RULE insert_spec_rule AS
    ON INSERT TO Spec
    DO INSTEAD (
        INSERT INTO Speciality (name, code, facultyID)
        VALUES (NEW."Специальность", NEW."Код", (SELECT id FROM Faculty WHERE name = NEW."Факультет"))
    );