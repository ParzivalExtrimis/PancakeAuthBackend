using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PancakeAuthBackend.Migrations
{
    /// <inheritdoc />
    public partial class SchoolSubscriptionChangedNN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Schools_SchoolId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_SchoolId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Subscriptions");

            migrationBuilder.CreateTable(
                name: "SchoolSubscription",
                columns: table => new
                {
                    SchoolsId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSubscription", x => new { x.SchoolsId, x.SubscriptionsId });
                    table.ForeignKey(
                        name: "FK_SchoolSubscription_Schools_SchoolsId",
                        column: x => x.SchoolsId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolSubscription_Subscriptions_SubscriptionsId",
                        column: x => x.SubscriptionsId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSubscription_SubscriptionsId",
                table: "SchoolSubscription",
                column: "SubscriptionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolSubscription");

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SchoolId",
                table: "Subscriptions",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Schools_SchoolId",
                table: "Subscriptions",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
