using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PancakeAuthBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutStandingAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    LastPaymentData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Billings");
        }
    }
}
