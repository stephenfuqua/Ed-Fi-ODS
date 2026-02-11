# Lineage Tracking

## Goal

Report application and timestamp for every modification, and enrich GET responses with that information under a new `_lineage` dictionary:

```json
{
  "_lineage": [
    {
      "applicationName": "Example SIS",
      "timestamp": 1711761837
    },
    {
      "applicationName": "Example SIS",
      "timestamp": 1723054173
    },
    {
      "applicationName": "Example HR System",
      "timestamp": 1723054999
    }
  ]
}
```

## Approach

This will require several steps:

1. Create supportive table structures for recording this information.
2. Modify the write pipeline to lookup the `applicationName` for a given bearer token.
3. Modify the write pipeline to write a new `_lineage` entry whenever performing a save operation.
4. Modify the read pipeline to retrieve this information and enrich the response.

### Unresolved

1. Should we capture cascading key updates? Is it enough to know who changed the parent record?
2. Can we update the aggregate data before writing it?

## Plan 1: Create Tables to Support Lineage

### Table Requirements

1. Record the `applicationName` as a string: do not attempt to normalize. This preserves the name against modifications.
2. Store the `timestamp` as a Unix timestamp.
3. Do not alter any existing columns.

### Option 1: Sidecar Table

For every `edfi.*` table, create a secondary table `edfi_lineage.*` with structure like:

| Name            | Type             | Options     | Notes              |
| --------------- | ---------------- | ----------- | ------------------ |
| Id              | int              | primary key | synthetic          |
| ParentId        | uniqueidentifier | not null    | FK to parent table |
| ApplicationName | varchar(200)     | not null    |                    |
| Timestamp       | bigint           | not null    |                    |

> [!NOTE]
>
> - Some table names already run into length problems in PostgreSQL. By moving the into a different schema, we preserve the original name.
> - The "parent" tables have a unique index on their `Id` columns; joining to `ParentId` in this table will be efficient.
> - The `applicationName` in `EdFi_Admin.dbo.Applications` has an unlimited length. This was not a reasonable choice. The lineage tracking will truncate to max 200 characters.

### Option 2: JSON storage in the Core Table

For every `edfi.*` table, create a new string / JSON column `lineage` that will hold the exact payload.

### Choosing a Table Design

- Option 1 requires an extra join for reads, whereas Option 2 requires an extra read before the write.
- Option 2 requires additional storage.

In either case, NHibernate must be modified, which might not be feasible.
