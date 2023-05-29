CREATE OR REPLACE PROCEDURE add_departmentmember(
    IN p_firstname TEXT,
    IN p_secondname TEXT,
    IN p_phonenumber TEXT,
    IN p_middlename TEXT DEFAULT NULL,
    IN p_email TEXT DEFAULT NULL
	
)
AS $$
BEGIN
    p_phonenumber := format_phone_number(p_phonenumber);
    IF CheckMembersPhone(p_phonenumber) THEN
        RAISE EXCEPTION 'Данный номер уже занят!';
    END IF;
    p_email := validate_email(p_email);
    INSERT INTO departmentmembers (firstname, secondname, middlename, email, phonenumber)
    VALUES (p_firstname, p_secondname, p_middlename, p_email, p_phonenumber);
END
$$ LANGUAGE plpgsql;



CREATE OR REPLACE PROCEDURE update_departmentmember(
    IN p_id INT,
    IN p_firstname TEXT,
    IN p_secondname TEXT,
    IN p_phonenumber TEXT,
    IN p_middlename TEXT DEFAULT NULL,
    IN p_email TEXT DEFAULT NULL
)
AS $$
BEGIN
    p_phonenumber := format_phone_number(p_phonenumber);

    IF CheckMembersPhone(p_id, p_phonenumber) THEN
        RAISE EXCEPTION 'Данный номер уже занят!';
    END IF;

    UPDATE departmentmembers
    SET firstname = p_firstname,
        secondname = p_secondname,
        middlename = p_middlename,
        email = p_email,
        phonenumber = p_phonenumber
    WHERE id = p_id;
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE delete_departmentmember(
    IN p_id INT
)
LANGUAGE SQL
AS $$
    DELETE FROM departmentmembers
    WHERE id = p_id;
$$;

----------------

CREATE OR REPLACE PROCEDURE add_faculty(
    IN p_name TEXT,
    IN p_department_id INTEGER
)
AS $$
BEGIN  
    p_name = regexp_replace(p_name, '^\w+', initcap(split_part(p_name, ' ', 1)));
    
    IF CheckFaculty(p_name) THEN
        RAISE EXCEPTION 'Данный факультет уже существует!';
    END IF;

    INSERT INTO Faculty (name, departamentID)
    VALUES (p_name, p_department_id);
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE update_faculty(
    IN p_id INT,
    IN p_name TEXT,
    IN p_department_id INTEGER
)
AS $$
BEGIN 
    p_name = regexp_replace(p_name, '^\w+', initcap(split_part(p_name, ' ', 1)));
    IF CheckFaculty(p_id, p_name) THEN
        RAISE EXCEPTION 'Данный факультет уже существует!';
    END IF;
    UPDATE Faculty
    SET name = p_name,
        departamentID = p_department_id
    WHERE id = p_id;
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE delete_faculty(
    IN p_id INT
)
LANGUAGE SQL
AS $$
    DELETE FROM Faculty
    WHERE id = p_id;
$$;

--------------------


CREATE OR REPLACE PROCEDURE add_kafedra(
    IN p_name TEXT,
    IN p_department_id INTEGER,
    IN p_faculty_id INTEGER
)
AS $$
BEGIN
    p_name := CheckKafedra(p_name);
    INSERT INTO Kafedra (name, departamentID, facultyID)
    VALUES (p_name, p_department_id, p_faculty_id);
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE update_kafedra(
    IN p_id INT,
    IN p_name TEXT,
    IN p_department_id INTEGER,
    IN p_faculty_id INTEGER
)
AS $$
BEGIN
    p_name := CheckKafedra(p_id, p_name);
    UPDATE Kafedra
    SET name = p_name,
        departamentID = p_department_id,
        facultyID = p_faculty_id
    WHERE id = p_id;
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE delete_kafedra(
    IN p_id INT
)
LANGUAGE SQL
AS $$
    DELETE FROM Kafedra
    WHERE id = p_id;
$$;

-------------

CREATE OR REPLACE PROCEDURE add_speciality(
    IN p_name TEXT,
    IN p_code VARCHAR(8),
    IN p_faculty_id INTEGER
)
AS $$
BEGIN
    p_name := CheckNameSpec(p_name);
    p_code := CheckCodeSpec(p_code);
    INSERT INTO Speciality (name, code, facultyID)
    VALUES (p_name, p_code, p_faculty_id);
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE update_speciality(
    IN p_id INT,
    IN p_name TEXT,
    IN p_code TEXT,
    IN p_faculty_id INTEGER
)
AS $$
BEGIN
    p_name := CheckNameSpec(p_id, p_name);
    p_code := CheckCodeSpec(p_id, p_code);
    UPDATE Speciality
    SET name = p_name,
        code = p_code,
        facultyID = p_faculty_id
    WHERE id = p_id;
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE delete_speciality(
    IN p_id INT
)
LANGUAGE SQL
AS $$
    DELETE FROM Speciality
    WHERE id = p_id;
$$;

-------------


CREATE OR REPLACE PROCEDURE add_discipline(
    IN p_name TEXT,
    IN p_kafedra_id INTEGER
)
AS $$
BEGIN
    p_name := CheckDiscipline(p_name);
    INSERT INTO Discipline (name, kafedraID)
    VALUES (p_name, p_kafedra_id);
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE update_discipline(
    IN p_id INT,
    IN p_name TEXT,
    IN p_kafedra_id INTEGER
)
AS $$
BEGIN
    p_name := CheckDiscipline(p_id, p_name);
    UPDATE Discipline
    SET name = p_name,
        kafedraID = p_kafedra_id
    WHERE id = p_id;
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE delete_discipline(
    IN p_id INT
)
LANGUAGE SQL
AS $$
    DELETE FROM Discipline
    WHERE id = p_id;
$$;

-------------------

CREATE OR REPLACE PROCEDURE add_discipline_speciality(
    IN p_discipline_id INTEGER,
    IN p_speciality_id INTEGER,
    IN p_semestr INTEGER,
    IN p_sessia INTEGER,
    IN p_lechour INTEGER DEFAULT NULL,
    IN p_prhours INTEGER DEFAULT NULL,
    IN p_labs INTEGER DEFAULT NULL
)
AS $$
BEGIN
    PERFORM CheckUchet(p_discipline_id, p_speciality_id);
    INSERT INTO DisciplineSpeciality (DisciplineID, SpecialityID, lechour, prhour, labs, semestr, sessia)
    VALUES (p_discipline_id, p_speciality_id, p_lechour, p_prhours, p_labs, p_semestr, p_sessia);
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE update_discipline_speciality(
	IN p_id INT,
    IN p_discipline_id INTEGER,
    IN p_speciality_id INTEGER,
    IN p_semestr INTEGER,
    IN p_sessia INTEGER,
	IN p_lechour INTEGER DEFAULT NULL,
    IN p_prhours INTEGER DEFAULT NULL,
    IN p_labs INTEGER DEFAULT NULL
)
AS $$
BEGIN
    PERFORM CheckUchet(p_id, p_discipline_id, p_speciality_id);
    UPDATE DisciplineSpeciality
    SET DisciplineID = p_discipline_id,
        SpecialityID = p_speciality_id,
        lechour = p_lechour, 
        prhour = p_prhours, 
        labs = p_labs,
        semestr = p_semestr,
        sessia = p_sessia
    WHERE id = p_id;
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE delete_discipline_speciality(
    IN p_id INT
)
LANGUAGE SQL
AS $$
    DELETE FROM DisciplineSpeciality
    WHERE id = p_id;
$$;

