CREATE OR REPLACE FUNCTION validate_name()
  RETURNS TRIGGER AS $$
BEGIN
  DECLARE
    ffirstname TEXT;
    fsecondname TEXT;
    fmiddlename TEXT;
  BEGIN 
    ffirstname := REPLACE(NEW.firstname, ' ', '');
    fsecondname := REPLACE(NEW.secondname, ' ', '');
    fmiddlename := REPLACE(NEW.middlename, ' ', '');

    IF fmiddlename ~ '^[а-яА-ЯЁё]+$' OR fmiddlename = '' THEN
      NEW.middlename := INITCAP(fmiddlename);
    ELSE    
      RAISE EXCEPTION 'Invalid syntax name!';
    END IF;

    IF ffirstname ~ '^[а-яА-ЯЁё]+$' AND fsecondname ~ '^[а-яА-ЯЁё]+$' THEN
      
      NEW.firstname := INITCAP(ffirstname);
      NEW.secondname := INITCAP(fsecondname);    
    ELSE    
      RAISE EXCEPTION 'Invalid syntax name!';
    END IF;
    
    RETURN NEW;
  END;
END;
$$ LANGUAGE plpgsql;


CREATE TRIGGER add_member_trigger
BEFORE INSERT ON departmentmembers
FOR EACH ROW
EXECUTE FUNCTION validate_name();



CREATE OR REPLACE FUNCTION validate_Faculty()
  RETURNS TRIGGER AS $$
BEGIN
  DECLARE
    fname TEXT;
  BEGIN 
    fname := NEW.name;
    IF fname = '' THEN
      RAISE EXCEPTION 'Поле не должно быть пустым!';
    END IF;
    NEW.name = regexp_replace(NEW.name, '^\w+', initcap(split_part(fname, ' ', 1)));
    RETURN NEW;
  END;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER add_faculty_trigger
BEFORE INSERT ON Faculty
FOR EACH ROW
EXECUTE FUNCTION validate_Faculty();



CREATE OR REPLACE FUNCTION validate_Kafedra()
  RETURNS TRIGGER AS $$
DECLARE
  fname TEXT;
BEGIN
  fname := NEW.name;
  IF fname = '' THEN
    RAISE EXCEPTION 'Поле не должно быть пустым!';
  END IF;
  IF fname ~ '^[а-яА-ЯЁё\s-]+$' THEN
      NEW.name = regexp_replace(NEW.name, '^\w+', initcap(split_part(fname, ' ', 1)));
    ELSE    
      RAISE EXCEPTION 'Недопустимые символы!';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER add_kafedra_trigger
BEFORE INSERT ON Kafedra
FOR EACH ROW
EXECUTE FUNCTION validate_Kafedra();



CREATE OR REPLACE FUNCTION validate_discipline()
  RETURNS TRIGGER AS $$
DECLARE
  fname TEXT;
BEGIN
  fname := NEW.name;
  IF fname = '' THEN
    RAISE EXCEPTION 'Поле не должно быть пустым!';
  END IF;
  IF fname ~ '^[а-яА-ЯЁё\s-]+$' THEN
      NEW.name = regexp_replace(NEW.name, '^\w+', initcap(split_part(fname, ' ', 1)));
    ELSE    
      RAISE EXCEPTION 'Недопустимые символы!';
    END IF;    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER add_discipline_trigger
BEFORE INSERT ON Discipline
FOR EACH ROW
EXECUTE FUNCTION validate_discipline();



CREATE OR REPLACE FUNCTION validate_spec()
  RETURNS TRIGGER AS $$
DECLARE
  fname TEXT;
BEGIN
  fname := NEW.name;
  IF fname = '' THEN
    RAISE EXCEPTION 'Поле не должно быть пустым!';
  END IF;
  IF fname ~ '^[а-яА-ЯЁё\s-]+$' THEN
      NEW.name = regexp_replace(NEW.name, '^\w+', initcap(split_part(fname, ' ', 1)));
    ELSE    
      RAISE EXCEPTION 'Недопустимые символы!';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER add_spec_trigger
BEFORE INSERT ON Speciality
FOR EACH ROW
EXECUTE FUNCTION validate_spec();



CREATE OR REPLACE FUNCTION validate_uchet()
  RETURNS TRIGGER AS $$
BEGIN
  NEW.lecHour := ABS(NEW.lecHour);
  NEW.prHour := ABS(NEW.prHour);
  NEW.labs := ABS(NEW.labs);
  NEW.semestr := ABS(NEW.semestr);
  IF NEW.semestr = 0 THEN
    RAISE EXCEPTION 'Семестр не должен быть нулевым!';
  END IF;
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER add_uchet_trigger
BEFORE INSERT ON DisciplineSpeciality
FOR EACH ROW
EXECUTE FUNCTION validate_uchet();