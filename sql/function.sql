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
  WHERE name = fname;
  IF id_f IS NULL THEN
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


CREATE OR REPLACE FUNCTION format_phone_number(
    fphone CHAR(50)
)
  RETURNS CHAR(50) AS $$
BEGIN
  -- Получение введенного номера телефона
  DECLARE
    phone_number TEXT;
  BEGIN
    -- Удаление всех символов, кроме цифр
    phone_number := regexp_replace(fphone, '[^0-9]', '', 'g');

    -- Проверка и исправление длины номера телефона
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
      -- Если номер телефона неправильной длины, выбрасываем исключение
      RAISE EXCEPTION 'Телефон введен неверно!';
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
  -- Получение введенного адреса электронной почты
  DECLARE
    email_address TEXT := femail;
  BEGIN
    -- Проверка и исправление формата адреса электронной почты
    IF femail = '' THEN
        RETURN femail;
	END IF;
    IF email_address ~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$' THEN
      -- Адрес электронной почты в правильном формате
      RETURN femail;
    ELSE
      -- Если адрес электронной почты неправильного формата, выбрасываем исключение
      RAISE EXCEPTION 'Почта введенна неверно!';
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


CREATE OR REPLACE FUNCTION CheckUchet(
    fDiscipline INT,
    fSpeciality INT
)
AS $$
DECLARE
  id_f INT;
BEGIN
  SELECT 
  id INTO DisciplineSpeciality
  FROM Speciality
  WHERE DisciplineID = fDiscipline AND SpecialityID = fSpeciality;
  IF id_f IS NOT NULL THEN
    RAISE EXCEPTION 'У данной специальности уже есть такая дисциплина!';
  END IF;
END;
$$ LANGUAGE plpgsql;