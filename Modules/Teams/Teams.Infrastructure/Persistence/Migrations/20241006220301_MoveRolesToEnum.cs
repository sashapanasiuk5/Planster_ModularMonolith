using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Teams.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MoveRolesToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMember_Member_MemberId",
                schema: "teams",
                table: "ProjectMember");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMember_Roles_RoleId",
                schema: "teams",
                table: "ProjectMember");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "teams");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMember_RoleId",
                schema: "teams",
                table: "ProjectMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                schema: "teams",
                table: "Member");

            migrationBuilder.RenameTable(
                name: "Member",
                schema: "teams",
                newName: "Members",
                newSchema: "teams");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "teams",
                table: "ProjectMember",
                newName: "Role");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                schema: "teams",
                table: "Members",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMember_Members_MemberId",
                schema: "teams",
                table: "ProjectMember",
                column: "MemberId",
                principalSchema: "teams",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMember_Members_MemberId",
                schema: "teams",
                table: "ProjectMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                schema: "teams",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Members",
                schema: "teams",
                newName: "Member",
                newSchema: "teams");

            migrationBuilder.RenameColumn(
                name: "Role",
                schema: "teams",
                table: "ProjectMember",
                newName: "RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                schema: "teams",
                table: "Member",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_RoleId",
                schema: "teams",
                table: "ProjectMember",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMember_Member_MemberId",
                schema: "teams",
                table: "ProjectMember",
                column: "MemberId",
                principalSchema: "teams",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMember_Roles_RoleId",
                schema: "teams",
                table: "ProjectMember",
                column: "RoleId",
                principalSchema: "teams",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
