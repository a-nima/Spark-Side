using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkSide.Data.Migrations
{
    public partial class AddPictureLinkFieldToChallenge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureLink",
                table: "Challenges",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureLink",
                table: "Challenges");
        }
    }
}
