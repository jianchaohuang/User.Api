using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Infrastruture.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Company = table.Column<string>(nullable: true),
                    Introduction = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    FinStage = table.Column<string>(nullable: true),
                    FinMoney = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false),
                    ProvinceName = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    CityName = table.Column<string>(nullable: true),
                    Revenue = table.Column<string>(nullable: true),
                    Valution = table.Column<string>(nullable: true),
                    BrokerageOption = table.Column<string>(nullable: true),
                    SourceId = table.Column<int>(nullable: false),
                    ReferenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectContributor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ProjectId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsCloaer = table.Column<short>(nullable: false),
                    ContributorType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectContributor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectContributor_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPropertys",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    Key = table.Column<string>(maxLength: 100, nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Value = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPropertys", x => new { x.ProjectId, x.Key, x.Value });
                    table.ForeignKey(
                        name: "FK_ProjectPropertys_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectViewer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ProjectId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectViewer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectViewer_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectContributor_ProjectId",
                table: "ProjectContributor",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectViewer_ProjectId",
                table: "ProjectViewer",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectContributor");

            migrationBuilder.DropTable(
                name: "ProjectPropertys");

            migrationBuilder.DropTable(
                name: "ProjectViewer");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
