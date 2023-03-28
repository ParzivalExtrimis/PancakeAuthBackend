using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PancakeAuthBackend.Migrations
{
    /// <inheritdoc />
    public partial class SubsciptionToIncludeChaptersOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Subscriptions_SubscriptionId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Subscriptions_SubscriptionId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SubscriptionId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Grades_SubscriptionId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Grades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubscriptionId",
                table: "Subjects",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_SubscriptionId",
                table: "Grades",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Subscriptions_SubscriptionId",
                table: "Grades",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Subscriptions_SubscriptionId",
                table: "Subjects",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
