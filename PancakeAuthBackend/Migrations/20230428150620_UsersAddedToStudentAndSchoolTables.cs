using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PancakeAuthBackend.Migrations
{
    /// <inheritdoc />
    public partial class UsersAddedToStudentAndSchoolTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolManagerId",
                table: "Schools",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassManagerGradeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassManagerSchoolId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoSMSchoolId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDate",
                value: new DateTime(2023, 4, 28, 20, 36, 20, 149, DateTimeKind.Local).AddTicks(3556));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "DueDate",
                value: new DateTime(2023, 4, 28, 20, 36, 20, 149, DateTimeKind.Local).AddTicks(3568));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                column: "DueDate",
                value: new DateTime(2023, 4, 28, 20, 36, 20, 149, DateTimeKind.Local).AddTicks(3569));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                column: "DueDate",
                value: new DateTime(2023, 4, 28, 20, 36, 20, 149, DateTimeKind.Local).AddTicks(3570));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                column: "DueDate",
                value: new DateTime(2023, 4, 28, 20, 36, 20, 149, DateTimeKind.Local).AddTicks(3571));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 1,
                column: "SchoolManagerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 2,
                column: "SchoolManagerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 3,
                column: "SchoolManagerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_SchoolManagerId",
                table: "Schools",
                column: "SchoolManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClassManagerGradeId",
                table: "AspNetUsers",
                column: "ClassManagerGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClassManagerSchoolId",
                table: "AspNetUsers",
                column: "ClassManagerSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CoSMSchoolId",
                table: "AspNetUsers",
                column: "CoSMSchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Grades_ClassManagerGradeId",
                table: "AspNetUsers",
                column: "ClassManagerGradeId",
                principalTable: "Grades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Schools_ClassManagerSchoolId",
                table: "AspNetUsers",
                column: "ClassManagerSchoolId",
                principalTable: "Schools",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Schools_CoSMSchoolId",
                table: "AspNetUsers",
                column: "CoSMSchoolId",
                principalTable: "Schools",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_AspNetUsers_SchoolManagerId",
                table: "Schools",
                column: "SchoolManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Grades_ClassManagerGradeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Schools_ClassManagerSchoolId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Schools_CoSMSchoolId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_AspNetUsers_SchoolManagerId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Schools_SchoolManagerId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClassManagerGradeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClassManagerSchoolId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CoSMSchoolId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SchoolManagerId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "ClassManagerGradeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClassManagerSchoolId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CoSMSchoolId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "DueDate",
                value: new DateTime(2023, 4, 26, 21, 24, 59, 510, DateTimeKind.Local).AddTicks(1662));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "DueDate",
                value: new DateTime(2023, 4, 26, 21, 24, 59, 510, DateTimeKind.Local).AddTicks(1676));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3,
                column: "DueDate",
                value: new DateTime(2023, 4, 26, 21, 24, 59, 510, DateTimeKind.Local).AddTicks(1677));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4,
                column: "DueDate",
                value: new DateTime(2023, 4, 26, 21, 24, 59, 510, DateTimeKind.Local).AddTicks(1678));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5,
                column: "DueDate",
                value: new DateTime(2023, 4, 26, 21, 24, 59, 510, DateTimeKind.Local).AddTicks(1679));
        }
    }
}
