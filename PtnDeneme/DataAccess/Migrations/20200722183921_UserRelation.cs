using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class UserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserCalenders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserCalenders_UserId",
                table: "UserCalenders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCalenders_Users_UserId",
                table: "UserCalenders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCalenders_Users_UserId",
                table: "UserCalenders");

            migrationBuilder.DropIndex(
                name: "IX_UserCalenders_UserId",
                table: "UserCalenders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserCalenders");
        }
    }
}
