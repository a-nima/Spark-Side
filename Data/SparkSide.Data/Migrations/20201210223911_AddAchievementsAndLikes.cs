using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkSide.Data.Migrations
{
    public partial class AddAchievementsAndLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChallengeTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeTasks_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersChallengeTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeTaskId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersChallengeTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersChallengeTasks_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersChallengeTasks_ChallengeTasks_ChallengeTaskId",
                        column: x => x.ChallengeTaskId,
                        principalTable: "ChallengeTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeTasks_ChallengeId",
                table: "ChallengeTasks",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeTasks_IsDeleted",
                table: "ChallengeTasks",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UsersChallengeTasks_ChallengeTaskId",
                table: "UsersChallengeTasks",
                column: "ChallengeTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersChallengeTasks_IsDeleted",
                table: "UsersChallengeTasks",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UsersChallengeTasks_UserID",
                table: "UsersChallengeTasks",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersChallengeTasks");

            migrationBuilder.DropTable(
                name: "ChallengeTasks");
        }
    }
}
