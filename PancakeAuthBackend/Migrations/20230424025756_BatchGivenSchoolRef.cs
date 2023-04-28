using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PancakeAuthBackend.Migrations
{
    /// <inheritdoc />
    public partial class BatchGivenSchoolRef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Batches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Batches_SchoolId",
                table: "Batches",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Schools_SchoolId",
                table: "Batches",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Schools_SchoolId",
                table: "Batches");

            migrationBuilder.DropIndex(
                name: "IX_Batches_SchoolId",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Batches");
        }
    }
}
