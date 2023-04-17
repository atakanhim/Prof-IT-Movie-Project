using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmProject.Infrastructure.Migrations
{
    public partial class last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "MovieCategoryMaps");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MovieCategoryMaps");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Favorite");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Favorite");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "MovieCategoryMaps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MovieCategoryMaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Favorite",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Favorite",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
