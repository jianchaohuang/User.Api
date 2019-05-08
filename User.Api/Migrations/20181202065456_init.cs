using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BPFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    AppUserId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    OriginFilePath = table.Column<string>(nullable: true),
                    FromatFilePath = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BPFiles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BPFiles");
        }
    }
}
