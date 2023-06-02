do $$ 
declare id_f int; 
begin CALL add_departmentmember('{firstName}', '{secondName}', '{phone}', '{lastName}', '{email}');
SELECT id INTO id_f FROM departmentmembers WHERE phonenumber = format_phone_number('{phone}');
CALL add_faculty('{faculty}', id_f); 
end; 
$$; 
commit;