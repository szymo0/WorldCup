using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorldCup.App.Migrations
{
    public partial class AddPointHisotry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PointHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BetId = table.Column<Guid>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointHistories_Bets_BetId",
                        column: x => x.BetId,
                        principalTable: "Bets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PointPolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PolicyType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Applied = table.Column<bool>(nullable: false),
                    Points = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    PointHistoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointPolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointPolicies_PointHistories_PointHistoryId",
                        column: x => x.PointHistoryId,
                        principalTable: "PointHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PointHistories_BetId",
                table: "PointHistories",
                column: "BetId");

            migrationBuilder.CreateIndex(
                name: "IX_PointPolicies_PointHistoryId",
                table: "PointPolicies",
                column: "PointHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointPolicies");

            migrationBuilder.DropTable(
                name: "PointHistories");
        }
    }
}
