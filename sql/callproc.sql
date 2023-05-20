CALL add_departmentmember('Дмитрий', 'Иванов', '+7 928 481 10 45', 'Иванович',  'ivanov2341@mail.ru');
CALL add_departmentmember('Иван', 'Бондарев','+7 966 444 65 43', 'Сергеевич', 'coolserg@ya.ru');
CALL add_departmentmember('Петр', 'Русаков','+7 962 140 35 81', 'Данилович', 'rusdan1@mail.ru');
CALL add_departmentmember('Сергей', 'Шастун','+7 912 262 32 56', 'Виленович', 'hasdhqwe@gmail.com');
CALL add_departmentmember('Александр', 'Русаков','+7 912 843 15 81', 'Александрович', 'aleska1992@mail.ru');
CALL add_departmentmember('Назир', 'Ахмедов','+7 946 123 22 22', 'Рашидович', '' );

CALL add_faculty('Компьютерных наук', 1);
CALL add_faculty('Физический', 2);
CALL add_faculty('Прикладной математики', 3);

CALL add_kafedra('ИКБ-2', 4, 1);
CALL add_kafedra('КОИС-1', 5, 2);
CALL add_kafedra('КТК-1', 6, 3);

CALL add_speciality('Информационные системы и технологии', '09.03.02', 1);
CALL add_speciality('Информационные системы и Вычислительная техника', '09.03.03', 1);
CALL add_speciality('Программная инженерия', '09.03.04', 1);

CALL add_discipline('Информатика', 1);
CALL add_discipline('Разработка ПО', 1);
CALL add_discipline('Базы Данных', 1);
CALL add_discipline('Физика', 2);

CALL add_discipline_speciality(1, 1, 1, 1, 80, 90);
CALL add_discipline_speciality(1, 2, 2, 1, 70, 75, 3);
CALL add_discipline_speciality(2, 1, 1, 2, 70, 75);
CALL add_discipline_speciality(2, 3, 1, 2, 43, 59);
CALL add_discipline_speciality(2, 2, 1, 1, 30, 0, 2);
CALL add_discipline_speciality(3, 1, 3, 1, 0, 65);
CALL add_discipline_speciality(3, 2, 3, 1, 100, 95);
CALL add_discipline_speciality(4, 3, 1, 2, 45, 50, 7);


select * from departmentmembers