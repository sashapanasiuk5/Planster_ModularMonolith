using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WorkOrganization.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddManyManyToAssignees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignees_Projects_ProjectId",
                schema: "work",
                table: "Assignees");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Assignees_AssigneeId_AssigneeProjectId",
                schema: "work",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssigneeId_AssigneeProjectId",
                schema: "work",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignees",
                schema: "work",
                table: "Assignees");

            migrationBuilder.DropIndex(
                name: "IX_Assignees_ProjectId",
                schema: "work",
                table: "Assignees");

            migrationBuilder.DropColumn(
                name: "AssigneeProjectId",
                schema: "work",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "work",
                table: "Assignees");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "work",
                table: "Assignees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignees",
                schema: "work",
                table: "Assignees",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssigneeProject",
                schema: "work",
                columns: table => new
                {
                    AssigneesId = table.Column<int>(type: "integer", nullable: false),
                    ProjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssigneeProject", x => new { x.AssigneesId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_AssigneeProject_Assignees_AssigneesId",
                        column: x => x.AssigneesId,
                        principalSchema: "work",
                        principalTable: "Assignees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssigneeProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalSchema: "work",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeId",
                schema: "work",
                table: "Tasks",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssigneeProject_ProjectsId",
                schema: "work",
                table: "AssigneeProject",
                column: "ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Assignees_AssigneeId",
                schema: "work",
                table: "Tasks",
                column: "AssigneeId",
                principalSchema: "work",
                principalTable: "Assignees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Assignees_AssigneeId",
                schema: "work",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "AssigneeProject",
                schema: "work");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssigneeId",
                schema: "work",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignees",
                schema: "work",
                table: "Assignees");

            migrationBuilder.AddColumn<int>(
                name: "AssigneeProjectId",
                schema: "work",
                table: "Tasks",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "work",
                table: "Assignees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                schema: "work",
                table: "Assignees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignees",
                schema: "work",
                table: "Assignees",
                columns: new[] { "Id", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeId_AssigneeProjectId",
                schema: "work",
                table: "Tasks",
                columns: new[] { "AssigneeId", "AssigneeProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Assignees_ProjectId",
                schema: "work",
                table: "Assignees",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignees_Projects_ProjectId",
                schema: "work",
                table: "Assignees",
                column: "ProjectId",
                principalSchema: "work",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Assignees_AssigneeId_AssigneeProjectId",
                schema: "work",
                table: "Tasks",
                columns: new[] { "AssigneeId", "AssigneeProjectId" },
                principalSchema: "work",
                principalTable: "Assignees",
                principalColumns: new[] { "Id", "ProjectId" });
        }
    }
}
