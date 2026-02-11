-- SPDX-License-Identifier: Apache-2.0
-- Licensed to the Ed-Fi Alliance under one or more agreements.
-- The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
-- See the LICENSE and NOTICES files in the project root for more information.

-- Create the edfi_lineage schema if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'edfi_lineage')
EXEC sys.sp_executesql N'CREATE SCHEMA [edfi_lineage]'
GO

-- Dynamically create lineage tables for all tables in the edfi schema
DECLARE @TableName NVARCHAR(128);
DECLARE @SQL NVARCHAR(MAX);

DECLARE table_cursor CURSOR FOR
SELECT t.name
FROM sys.tables t
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
INNER JOIN sys.columns c ON t.object_id = c.object_id
WHERE s.name = 'edfi'
  -- Only include tables that have an `Id` column; tables without this column are subsidiaries and only their root tables need lineage tracking
  AND c.name = 'Id'
ORDER BY t.name;

OPEN table_cursor;

FETCH NEXT FROM table_cursor INTO @TableName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @SQL = N'
CREATE TABLE [edfi_lineage].' + QUOTENAME(@TableName) + N' (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ParentId] UNIQUEIDENTIFIER NOT NULL,
    [ApplicationName] NVARCHAR(200) NOT NULL,
    [Timestamp] BIGINT NOT NULL,
    CONSTRAINT [FK_lineage_' + @TableName + N'] FOREIGN KEY ([ParentId])
        REFERENCES [edfi].' + QUOTENAME(@TableName) + N' ([Id])
);';

    PRINT @SQL;
    EXEC sp_executesql @SQL;

    FETCH NEXT FROM table_cursor INTO @TableName;
END;

CLOSE table_cursor;
DEALLOCATE table_cursor;
GO


