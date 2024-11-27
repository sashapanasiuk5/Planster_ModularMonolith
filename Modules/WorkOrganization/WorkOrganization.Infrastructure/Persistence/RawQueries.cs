namespace WorkOrganization.Infrastructure.Persistence;

public static class RawQueries
{
    public const string RecursiveTaskById = """
                                            WITH RECURSIVE task
                                            AS(
                                                -- anchor member
                                                SELECT "Id", "Code", "Title", "Description", "AcceptanceCriteria", "Priority", "Estimation", "CreatedAt", "StatusId", "Type", "ProjectId", "AssigneeId", "SprintId", "ParentTaskId" FROM "work"."Tasks" WHERE "Id" = {0}
                                                UNION ALL
                                                -- recursive term
                                                SELECT t2."Id", t2."Code", t2."Title", t2."Description", t2."AcceptanceCriteria", t2."Priority", t2."Estimation", t2."CreatedAt", t2."StatusId", t2."Type", t2."ProjectId", t2."AssigneeId", t2."SprintId", t2."ParentTaskId" FROM "work"."Tasks" as t2
                                            	JOIN task tr ON t2."ParentTaskId" = tr."Id"
                                            )
                                            SELECT * FROM task
                                            """;
}