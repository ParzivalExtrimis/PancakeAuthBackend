using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PancakeAuthBackend.Migrations
{
    /// <inheritdoc />
    public partial class PaymentsRestructuredBillingsRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Billings_BillingId",
                table: "Payment");

            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.RenameColumn(
                name: "BillingId",
                table: "Payment",
                newName: "SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_BillingId",
                table: "Payment",
                newName: "IX_Payment_SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Schools_SchoolId",
                table: "Payment",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Schools_SchoolId",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "SchoolId",
                table: "Payment",
                newName: "BillingId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_SchoolId",
                table: "Payment",
                newName: "IX_Payment_BillingId");

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastPaymentData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutStandingAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Billings_BillingId",
                table: "Payment",
                column: "BillingId",
                principalTable: "Billings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
