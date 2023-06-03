set enable_seqscan = on;

CREATE INDEX name_idx_disc ON Discipline USING gin (name gin_trgm_ops);

CREATE INDEX idx_departmentmembers_id ON departmentmembers (id);

CREATE INDEX name_idx_faculty ON Faculty USING gist (name gist_trgm_ops);