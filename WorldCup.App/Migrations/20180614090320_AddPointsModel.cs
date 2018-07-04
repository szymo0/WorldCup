using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorldCup.App.Migrations
{
    public partial class AddPointsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointHistories_Bets_BetId",
                table: "PointHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_PointPolicies_PointHistories_PointHistoryId",
                table: "PointPolicies");

            migrationBuilder.DropIndex(
                name: "IX_PointHistories_BetId",
                table: "PointHistories");

            migrationBuilder.DropColumn(
                name: "BetId",
                table: "PointHistories");

            migrationBuilder.RenameColumn(
                name: "PointHistoryId",
                table: "PointPolicies",
                newName: "PointResultId");

            migrationBuilder.RenameIndex(
                name: "IX_PointPolicies_PointHistoryId",
                table: "PointPolicies",
                newName: "IX_PointPolicies_PointResultId");

            migrationBuilder.AddColumn<Guid>(
                name: "ResultId",
                table: "Bets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PointResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    AddedPoints = table.Column<int>(nullable: false),
                    SumPoints = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    PointHistoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointResults_PointHistories_PointHistoryId",
                        column: x => x.PointHistoryId,
                        principalTable: "PointHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PointResults_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_ResultId",
                table: "Bets",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_PointResults_PointHistoryId",
                table: "PointResults",
                column: "PointHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PointResults_UserId",
                table: "PointResults",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_PointResults_ResultId",
                table: "Bets",
                column: "ResultId",
                principalTable: "PointResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PointPolicies_PointResults_PointResultId",
                table: "PointPolicies",
                column: "PointResultId",
                principalTable: "PointResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_PointResults_ResultId",
                table: "Bets");

            migrationBuilder.DropForeignKey(
                name: "FK_PointPolicies_PointResults_PointResultId",
                table: "PointPolicies");

            migrationBuilder.DropTable(
                name: "PointResults");

            migrationBuilder.DropIndex(
                name: "IX_Bets_ResultId",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Bets");

            migrationBuilder.RenameColumn(
                name: "PointResultId",
                table: "PointPolicies",
                newName: "PointHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PointPolicies_PointResultId",
                table: "PointPolicies",
                newName: "IX_PointPolicies_PointHistoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "BetId",
                table: "PointHistories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PointHistories_BetId",
                table: "PointHistories",
                column: "BetId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointHistories_Bets_BetId",
                table: "PointHistories",
                column: "BetId",
                principalTable: "Bets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PointPolicies_PointHistories_PointHistoryId",
                table: "PointPolicies",
                column: "PointHistoryId",
                principalTable: "PointHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
