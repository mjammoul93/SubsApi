using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubsApi.Migrations
{
    public partial class updatingSubsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Subsciptions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Subsciptions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Subsciptions");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Subsciptions");
        }
    }
}
