CREATE OR REPLACE FUNCTION CheckFaculty(
    fname VARCHAR(50)
)
RETURNS BOOLEAN AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
  id INTO id_f 
  FROM faculty 
  WHERE name ILIKE fname;
  IF id_f IS NULL THEN
    RETURN FALSE;
  END IF;
  RETURN TRUE;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION CheckFaculty(
    facultyID INT,
    fname VARCHAR(50)
)
RETURNS BOOLEAN AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
    id INTO id_f 
  FROM faculty 
  WHERE name = fname;
  
  IF id_f IS NULL OR id_f = facultyID THEN
    RETURN FALSE;
  END IF;
  
  RETURN TRUE;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION CheckMembersPhone(
    fphone VARCHAR(16)
)
RETURNS BOOLEAN AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
  id INTO id_f 
  FROM departmentmembers
  WHERE phonenumber = fphone;
  IF id_f IS NULL THEN
    RETURN FALSE;
  END IF;
  RETURN TRUE;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION CheckMembersPhone(
    memberID INT,
    fphone VARCHAR(16)
)
RETURNS BOOLEAN AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
    id INTO id_f 
  FROM departmentmembers
  WHERE phonenumber = fphone;
  
  IF id_f IS NULL OR id_f = memberID THEN
    RETURN FALSE;
  END IF;
  
  RETURN TRUE;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION CheckMembersmail(
    fmail VARCHAR(16)
)
RETURNS BOOLEAN AS $$
DECLARE
  id_f INT;
BEGIN
  IF fmail = '' THEN
  	RETURN FALSE;
  END IF;
  
  SELECT 
  id INTO id_f 
  FROM departmentmembers
  WHERE email = fmail;
  
  IF id_f IS NULL THEN
    RETURN FALSE;
  END IF;
  	RETURN TRUE;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION CheckMembersmail(
    memberID INT,
    fmail VARCHAR(16)
)
RETURNS BOOLEAN AS $$
DECLARE
  id_f INT;
BEGIN
  IF fmail = '' THEN
    RETURN FALSE;
  END IF;
  
  SELECT 
    id INTO id_f 
  FROM departmentmembers
  WHERE email = fmail;
  
  IF id_f IS NULL OR id_f = memberID THEN
    RETURN FALSE;
  END IF;
  
  RETURN TRUE;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION format_phone_number(
    fphone CHAR(50)
)
  RETURNS CHAR(50) AS $$
BEGIN
  DECLARE
    phone_number TEXT;
  BEGIN
    phone_number := regexp_replace(fphone, '[^0-9]', '', 'g');

    IF length(phone_number) = 10 THEN
      phone_number := '+7 ' || substr(phone_number, 1, 3) || ' ' ||
                      substr(phone_number, 4, 3) || ' ' ||
                      substr(phone_number, 7, 2) || ' ' ||
                      substr(phone_number, 9, 2);
    ELSIF length(phone_number) = 11 THEN
      phone_number := '+7 ' || substr(phone_number, 2, 3) || ' ' ||
                      substr(phone_number, 5, 3) || ' ' ||
                      substr(phone_number, 8, 2) || ' ' ||
                      substr(phone_number, 10, 2);
    ELSE

      RAISE EXCEPTION 'Invalid phone number';
    END IF;

    RETURN phone_number;
  END;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION validate_email(
    femail VARCHAR(30)
)
  RETURNS VARCHAR(30) AS $$
BEGIN

  DECLARE
    email_address TEXT := femail;
  BEGIN

    IF femail = '' THEN
        RETURN femail;
	END IF;
    IF email_address ~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$' THEN

      RETURN femail;
    ELSE
      RAISE EXCEPTION 'Invalid email address';
    END IF;
  END;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION CheckKafedra(
    fname TEXT
)
RETURNS TEXT AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
  id INTO id_f 
  FROM Kafedra
  WHERE name ILIKE fname;
  IF id_f IS NOT NULL THEN
    RAISE EXCEPTION 'Данная кафедра уже есть!';
  END IF;
  return fname;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION CheckKafedra(
    kafedraID INT,
    fname TEXT
)
RETURNS TEXT AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
    id INTO id_f 
  FROM Kafedra
  WHERE name ILIKE fname;
  
  IF id_f IS NOT NULL AND id_f <> kafedraID THEN
    RAISE EXCEPTION 'Данная кафедра уже есть!';
  END IF;
  
  RETURN fname;
END;
$$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION CheckDiscipline(
    fname TEXT
)
RETURNS TEXT AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
  id INTO id_f 
  FROM Discipline
  WHERE name ILIKE fname;
  IF id_f IS NOT NULL THEN
    RAISE EXCEPTION 'Данная дисциплина уже есть!';
  END IF;
  return fname;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION CheckDiscipline(
    disciplineID INT,
    fname TEXT
)
RETURNS TEXT AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
    id INTO id_f 
  FROM Discipline
  WHERE name ILIKE fname;
  
  IF id_f IS NOT NULL AND id_f <> disciplineID THEN
    RAISE EXCEPTION 'Данная дисциплина уже есть!';
  END IF;
  
  RETURN fname;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION CheckNameSpec(
    fname TEXT
)
RETURNS TEXT AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
  id INTO id_f 
  FROM Speciality
  WHERE name ILIKE fname;
  IF id_f IS NOT NULL THEN
    RAISE EXCEPTION 'Данная специальность уже существует!';
  END IF;
  return fname;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION CheckNameSpec(
    specID INT,
    fname TEXT
)
RETURNS TEXT AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
    id INTO id_f 
  FROM Speciality
  WHERE name ILIKE fname;
  
  IF id_f IS NOT NULL AND id_f <> specID THEN
    RAISE EXCEPTION 'Данная специальность уже существует!';
  END IF;
  
  RETURN fname;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION CheckCodeSpec(
    fcode TEXT
)
RETURNS TEXT AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
  id INTO id_f 
  FROM Speciality
  WHERE code ILIKE fcode;
  IF id_f IS NOT NULL THEN
    RAISE EXCEPTION 'Данный код уже существует!';
  END IF;

  fcode := regexp_replace(fcode, '[^0-9]', '', 'g');

  IF length(fcode) = 6 THEN
    fcode := substr(fcode, 1, 2) || '.' || substr(fcode, 3, 2) || '.' || substr(fcode, 5, 2);
  ELSE
    RAISE EXCEPTION 'Недопустимые значения в коде!';
  END IF;

  return fcode;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION CheckCodeSpec(
    specID INT,
    fcode TEXT
)
RETURNS TEXT AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
    id INTO id_f 
  FROM Speciality
  WHERE code ILIKE fcode;
  
  IF id_f IS NOT NULL AND id_f <> specID THEN
    RAISE EXCEPTION 'Данный код уже существует!';
  END IF;

  fcode := regexp_replace(fcode, '[^0-9]', '', 'g');

  IF length(fcode) = 6 THEN
    fcode := substr(fcode, 1, 2) || '.' || substr(fcode, 3, 2) || '.' || substr(fcode, 5, 2);
  ELSE
    RAISE EXCEPTION 'Недопустимые значения в коде!';
  END IF;

  RETURN fcode;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION CheckUchet(
    fDiscipline INT,
    fSpeciality INT
)
RETURNS VOID
AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
  id INTO id_f
  FROM DisciplineSpeciality
  WHERE DisciplineID = fDiscipline AND SpecialityID = fSpeciality;
  IF id_f IS NOT NULL THEN
    RAISE EXCEPTION 'У данной специальности уже есть такая дисциплина!';
  END IF;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION CheckUchet(
    uchetID INT,
    fDiscipline INT,
    fSpeciality INT
)
RETURNS VOID
AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
  id INTO id_f
  FROM DisciplineSpeciality
  WHERE DisciplineID = fDiscipline AND SpecialityID = fSpeciality;
  IF id_f IS NOT NULL AND id_f <> uchetID THEN
    RAISE EXCEPTION 'У данной специальности уже есть такая дисциплина!';
  END IF;
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_department_members_with_email()
RETURNS TABLE (firstname VARCHAR(30), secondname VARCHAR(30), email VARCHAR(100))
AS $$
BEGIN
    RETURN QUERY
    SELECT firstname, secondname, email
    FROM departmentmembers
    WHERE email IS NOT NULL AND email != '';
END;
$$ LANGUAGE SQL;