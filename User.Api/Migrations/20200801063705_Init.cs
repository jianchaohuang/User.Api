using Microsoft.EntityFrameworkCore.Migrations;

namespace User.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserProperties_AppUserId",
                table: "UserProperties",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProperties_Users_AppUserId",
                table: "UserProperties",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProperties_Users_AppUserId",
                table: "UserProperties");

            migrationBuilder.DropIndex(
                name: "IX_UserProperties_AppUserId",
                table: "UserProperties");
        }
    }
}
