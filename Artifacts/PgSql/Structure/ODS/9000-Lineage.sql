-- SPDX-License-Identifier: Apache-2.0
-- Licensed to the Ed-Fi Alliance under one or more agreements.
-- The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
-- See the LICENSE and NOTICES files in the project root for more information.

-- Create the edfi_lineage schema if it doesn't exist
CREATE SCHEMA IF NOT EXISTS edfi_lineage;

-- Dynamically create lineage tables for all tables in the edfi schema
DO $$
DECLARE
    table_name TEXT;
    sql_stmt TEXT;
BEGIN
    FOR table_name IN
        SELECT t.tablename
        FROM pg_tables t
        INNER JOIN information_schema.columns c
            ON c.table_schema = t.schemaname
            AND c.table_name = t.tablename
        WHERE t.schemaname = 'edfi'
          -- Only include tables that have an `id` column; tables without this column are subsidiaries and only their root tables need lineage tracking
          AND c.column_name = 'id'
        ORDER BY t.tablename
    LOOP
        sql_stmt := format('
CREATE TABLE edfi_lineage.%I (
    Id SERIAL PRIMARY KEY,
    ParentId UUID NOT NULL,
    ApplicationName VARCHAR(200) NOT NULL,
    Timestamp BIGINT NOT NULL,
    CONSTRAINT FK_lineage_%s FOREIGN KEY (ParentId)
        REFERENCES edfi.%I (id)
);', table_name, table_name, table_name, table_name);

        RAISE NOTICE '%', sql_stmt;
        EXECUTE sql_stmt;
    END LOOP;
END $$;


