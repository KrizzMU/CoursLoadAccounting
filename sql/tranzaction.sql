BEGIN;
SAVEPOINT sp1;

add_departmentmember();
add_faculty();

ROLLBACK TO SAVEPOINT sp1;
COMMIT;