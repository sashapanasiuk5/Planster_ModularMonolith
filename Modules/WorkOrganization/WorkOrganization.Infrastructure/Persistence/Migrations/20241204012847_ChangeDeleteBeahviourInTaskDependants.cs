using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkOrganization.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteBeahviourInTaskDependants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Assignees_AssigneeId",
                schema: "work",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Sprints_SprintId",
                schema: "work",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Assignees_AssigneeId",
                schema: "work",
                table: "Tasks",
                column: "AssigneeId",
                principalSchema: "work",
                principalTable: "Assignees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Sprints_SprintId",
                schema: "work",
                table: "Tasks",
                column: "SprintId",
                principalSchema: "work",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Assignees_AssigneeId",
                schema: "work",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Sprints_SprintId",
                schema: "work",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Assignees_AssigneeId",
                schema: "work",
                table: "Tasks",
                column: "AssigneeId",
                principalSchema: "work",
                principalTable: "Assignees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Sprints_SprintId",
                schema: "work",
                table: "Tasks",
                column: "SprintId",
                principalSchema: "work",
                principalTable: "Sprints",
                principalColumn: "Id");
        }
    }
}
