CREATE OR REPLACE PROCEDURE add_facultymember(
	IN ffirstName VARCHAR(255),
	IN fsecondName VARCHAR(255),
	IN fphone VARCHAR(255),
	IN ffaculty VARCHAR(255),
	IN flastName VARCHAR(255)  DEFAULT NULL,
	IN femail VARCHAR(255)  DEFAULT NULL
)
AS $$ 
DECLARE id_f INT; 
BEGIN
	BEGIN
	BEGIN
	CALL add_departmentmember(ffirstName, fsecondName, fphone, flastName, femail);	
	SELECT id INTO id_f FROM departmentmembers WHERE phonenumber = format_phone_number(fphone);	
	CALL add_faculty(ffaculty, id_f); 	
	EXCEPTION
	WHEN OTHERS THEN
	ROLLBACK;
	RAISE;
	END;
	COMMIT;
	END;
END
$$ LANGUAGE plpgsql;