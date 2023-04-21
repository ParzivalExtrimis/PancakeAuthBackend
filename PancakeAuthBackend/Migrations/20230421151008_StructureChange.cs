using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PancakeAuthBackend.Migrations
{
    /// <inheritdoc />
    public partial class StructureChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Grades_GradeId",
                table: "Batches");

            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Schools_SchoolId",
                table: "Batches");

            migrationBuilder.DropTable(
                name: "BatchSubject");

            migrationBuilder.DropTable(
                name: "ChapterSubscription");

            migrationBuilder.DropTable(
                name: "SchoolSubscription");

            migrationBuilder.DropIndex(
                name: "IX_Batches_GradeId",
                table: "Batches");

            migrationBuilder.DropIndex(
                name: "IX_Batches_SchoolId",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "SchoolName",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Payments",
                newName: "DueDate");

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SchoolId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "Chapters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AvailedSubscription",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailedSubscription", x => new { x.SchoolId, x.SubscriptionId });
                    table.ForeignKey(
                        name: "FK_AvailedSubscription_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvailedSubscription_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_GradeId",
                table: "Students",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolId",
                table: "Students",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_SubscriptionId",
                table: "Chapters",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailedSubscription_SubscriptionId",
                table: "AvailedSubscription",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Subscriptions_SubscriptionId",
                table: "Chapters",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Grades_GradeId",
                table: "Students",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Subscriptions_SubscriptionId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Grades_GradeId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "AvailedSubscription");

            migrationBuilder.DropIndex(
                name: "IX_Students_GradeId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SchoolId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Chapters_SubscriptionId",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Chapters");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Payments",
                newName: "Date");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolId",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Mode",
                table: "Payments",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Chapters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Batches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Batches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SchoolName",
                table: "Addresses",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BatchSubject",
                columns: table => new
                {
                    BatchesId = table.Column<int>(type: "int", nullable: false),
                    SubjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchSubject", x => new { x.BatchesId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_BatchSubject_Batches_BatchesId",
                        column: x => x.BatchesId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChapterSubscription",
                columns: table => new
                {
                    IncludedChaptersId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterSubscription", x => new { x.IncludedChaptersId, x.SubscriptionsId });
                    table.ForeignKey(
                        name: "FK_ChapterSubscription_Chapters_IncludedChaptersId",
                        column: x => x.IncludedChaptersId,
                        principalTable: "Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChapterSubscription_Subscriptions_SubscriptionsId",
                        column: x => x.SubscriptionsId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolSubscription_Subscriptions_SubscriptionsId",
                        column: x => x.SubscriptionsId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batches_GradeId",
                table: "Batches",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Batches_SchoolId",
                table: "Batches",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchSubject_SubjectsId",
                table: "BatchSubject",
                column: "SubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterSubscription_SubscriptionsId",
                table: "ChapterSubscription",
                column: "SubscriptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSubscription_SubscriptionsId",
                table: "SchoolSubscription",
                column: "SubscriptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Grades_GradeId",
                table: "Batches",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Schools_SchoolId",
                table: "Batches",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
