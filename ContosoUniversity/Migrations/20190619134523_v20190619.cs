using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class v20190619 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Credits",
                table: "Course",
                nullable: false,
                defaultValue: 2,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyTime",
                table: "Course",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<decimal>(
                name: "MoneySecurity",
                table: "Course",
                type: "money",
                nullable: false,
                defaultValue: 111111m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifyTime",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "MoneySecurity",
                table: "Course");

            migrationBuilder.AlterColumn<int>(
                name: "Credits",
                table: "Course",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 2);
        }
    }
}
