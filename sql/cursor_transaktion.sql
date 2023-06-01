CREATE OR REPLACE FUNCTION swap_lec_pr_hours() RETURNS VOID AS $$
DECLARE
    cur CURSOR FOR SELECT id, lecHour, prHour FROM DisciplineSpeciality FOR UPDATE;
    rec RECORD;
BEGIN
    OPEN cur;
    LOOP
        FETCH cur INTO rec;
        EXIT WHEN NOT FOUND;
        
        BEGIN
            UPDATE DisciplineSpeciality SET
                lecHour = rec.prHour,
                prHour = rec.lecHour
            WHERE CURRENT OF cur;
        EXCEPTION
            WHEN OTHERS THEN
                RAISE;
        END;
    END LOOP;
    CLOSE cur;
END;
$$ LANGUAGE plpgsql;

SELECT swap_lec_pr_hours()