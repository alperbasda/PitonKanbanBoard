using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class simplfy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "UserCalenders");

            migrationBuilder.DropColumn(
                name: "ExpectedLastSuccessDate",
                table: "UserCalenders");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "UserCalenders");

            migrationBuilder.DropColumn(
                name: "SuccessDate",
                table: "UserCalenders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "UserCalenders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedLastSuccessDate",
                table: "UserCalenders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "UserCalenders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SuccessDate",
                table: "UserCalenders",
                type: "datetime2",
                nullable: true);
        }
    }
}
