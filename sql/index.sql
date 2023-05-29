CREATE INDEX name_idx_disc ON Discipline USING gin (name gin_trgm_ops);

CREATE INDEX name_idx_faculty ON faculty USING gin (name gin_trgm_ops);

