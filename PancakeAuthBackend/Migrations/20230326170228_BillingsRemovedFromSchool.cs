using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PancakeAuthBackend.Migrations
{
    /// <inheritdoc />
    public partial class BillingsRemovedFromSchool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billings_Schools_SchoolId",
                table: "Billings");

            migrationBuilder.DropIndex(
                name: "IX_Billings_SchoolId",
                table: "Billings");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Billings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Billings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Billings_SchoolId",
                table: "Billings",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Billings_Schools_SchoolId",
                table: "Billings",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
