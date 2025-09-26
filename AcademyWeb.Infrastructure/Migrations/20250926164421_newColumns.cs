using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademyWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "CourseName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Courses",
                newName: "CourseDescription");

            migrationBuilder.RenameColumn(
                name: "CourseCode",
                table: "Courses",
                newName: "NumberOfCredits");

            migrationBuilder.AddColumn<int>(
                name: "Accreditation",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SAQAId",
                table: "Courses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CommencementDate",
                table: "CourseOfferings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "CourseOfferings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryMode",
                table: "CourseOfferings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DurationUnit",
                table: "CourseOfferings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DurationValue",
                table: "CourseOfferings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FacilitatorIds",
                table: "CourseOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "CourseOfferings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "CourseOfferings",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CourseOfferings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "CourseOfferings",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accreditation",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SAQAId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CommencementDate",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "DeliveryMode",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "DurationUnit",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "DurationValue",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "FacilitatorIds",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "Venue",
                table: "CourseOfferings");

            migrationBuilder.RenameColumn(
                name: "NumberOfCredits",
                table: "Courses",
                newName: "CourseCode");

            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "Courses",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "CourseDescription",
                table: "Courses",
                newName: "Description");
        }
    }
}
