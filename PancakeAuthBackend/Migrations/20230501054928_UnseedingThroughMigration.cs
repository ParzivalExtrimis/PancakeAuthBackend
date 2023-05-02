using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace PancakeAuthBackend.Migrations
{
    /// <inheritdoc />
    public partial class UnseedingThroughMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.DeleteData(
                table: "AvailedSubscription",
                keyColumns: new[] { "SchoolId", "SubscriptionId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AvailedSubscription",
                keyColumns: new[] { "SchoolId", "SubscriptionId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "AvailedSubscription",
                keyColumns: new[] { "SchoolId", "SubscriptionId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "AvailedSubscription",
                keyColumns: new[] { "SchoolId", "SubscriptionId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "AvailedSubscription",
                keyColumns: new[] { "SchoolId", "SubscriptionId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AvailedSubscription",
                keyColumns: new[] { "SchoolId", "SubscriptionId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ChaptersIncluded",
                keyColumns: new[] { "ChapterId", "SubscriptionId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ChaptersIncluded",
                keyColumns: new[] { "ChapterId", "SubscriptionId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ChaptersIncluded",
                keyColumns: new[] { "ChapterId", "SubscriptionId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ChaptersIncluded",
                keyColumns: new[] { "ChapterId", "SubscriptionId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "ChaptersIncluded",
                keyColumns: new[] { "ChapterId", "SubscriptionId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "ChaptersIncluded",
                keyColumns: new[] { "ChapterId", "SubscriptionId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "ChaptersIncluded",
                keyColumns: new[] { "ChapterId", "SubscriptionId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "ChaptersIncluded",
                keyColumns: new[] { "ChapterId", "SubscriptionId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Chapters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Chapters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Chapters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Chapters",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Chapters",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Chapters",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "PostalCode", "Region", "State", "StreetName" },
                values: new object[,]
                {
                    { 1, "Hershey", "USA", "2456-82", "Downtown", "Pennsylvania", "Wagner Street #22" },
                    { 2, "NYC", "USA", "1155-82", "Bronx", "New York", "Browning Ave" },
                    { 3, "Birmingham", "UK", "S4356", "Central England", "England", "Turing Street, 35th Avenue" }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "1" },
                    { 2, "2" },
                    { 3, "3" },
                    { 4, "4" },
                    { 5, "5" },
                    { 6, "6" },
                    { 7, "7" },
                    { 8, "8" },
                    { 9, "9" },
                    { 10, "10" },
                    { 11, "11" },
                    { 12, "12" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Physics" },
                    { 2, "Math" },
                    { 3, "Chemistry" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Standard Subscription", "Default", "Included" },
                    { 2, "Added Modules for Math", "Math Magic", "Add-On" },
                    { 3, "Added Modules for Physics", "Physics Booster", "Add-On" }
                });

            migrationBuilder.InsertData(
                table: "Chapters",
                columns: new[] { "Id", "Description", "SubjectId", "Title" },
                values: new object[,]
                {
                    { 1, "Intro to Newtonian Gravitation", 1, "Gravity" },
                    { 2, "Intro to Thermodynamics", 1, "Thermodynamics" },
                    { 3, "Intro to Kinematics", 1, "Kinematics" },
                    { 4, "Intro to Matrix Manipulation", 2, "Matrices" },
                    { 5, "Intro to 2D Vectors", 2, "Vectors" },
                    { 6, "Intro to Organic Chemistry", 3, "Organic Chemistry" }
                });

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "Id", "AddressId", "Name", "SchoolManagerId" },
                values: new object[,]
                {
                    { 1, 1, "Hershey", null },
                    { 2, 2, "Jefferson High", null },
                    { 3, 3, "Hawthrone Elementary", null }
                });

            migrationBuilder.InsertData(
                table: "AvailedSubscription",
                columns: new[] { "SchoolId", "SubscriptionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Batches",
                columns: new[] { "Id", "Name", "SchoolId" },
                values: new object[,]
                {
                    { 1, "5A", 1 },
                    { 2, "7A", 1 },
                    { 3, "2A", 2 },
                    { 4, "3A", 2 },
                    { 5, "5B", 3 },
                    { 6, "7B", 3 }
                });

            migrationBuilder.InsertData(
                table: "ChaptersIncluded",
                columns: new[] { "ChapterId", "SubscriptionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 },
                    { 4, 1 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 1 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "Details", "DueDate", "SchoolId", "Status" },
                values: new object[,]
                {
                    { 1, 156200, "Billed on 6 subscriptions. First Availed on the date 12/11/22", new DateTime(2023, 4, 28, 20, 36, 20, 149, DateTimeKind.Local).AddTicks(3556), 1, "Pending" },
                    { 2, 23900, "Billed on 1 subscription(s). First Availed on the date 09/10/22", new DateTime(2023, 4, 28, 20, 36, 20, 149, DateTimeKind.Local).AddTicks(3568), 1, "Completed" },
                    { 3, 78650, "Billed on 4 subscription(s). First Availed on the date 04/12/22", new DateTime(2023, 4, 28, 20, 36, 20, 149, DateTimeKind.Local).AddTicks(3569), 2, "Pending" },
                    { 4, 245790, "Billed on 8 subscription(s). First Availed on the date 02/01/23", new DateTime(2023, 4, 28, 20, 36, 20, 149, DateTimeKind.Local).AddTicks(3570), 3, "Pending" },
                    { 5, 156200, "Billed on 6 subscriptions. First Availed on the date 01/02/23", new DateTime(2023, 4, 28, 20, 36, 20, 149, DateTimeKind.Local).AddTicks(3571), 3, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BatchId", "CityOfOrigin", "CountryOfOrigin", "Email", "GradeId", "Name", "Nationality", "PhoneNumber", "SchoolId", "StateOfOrigin", "StudentUID", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Orlando", "US", "Jon@TonightShow.com", 5, "Jon Stewart", "American", "555-7896", 1, "Florida", "1HE234089", null },
                    { 2, 3, "Detroit", "US", "green@barrel.com", 2, "Millie Dyer", "American", "555-2561", 2, "Michigan", "1JE768901", null },
                    { 3, 6, "Jersey City", "US", "Braazen@fox.com", 7, "Corey Black", "American", "555-8576", 3, "New Jersey", "1HT234586", null },
                    { 4, 1, "Tuscon", "US", "Dana@Verasity.com", 5, "Dana White", "American", "555-1111", 1, "Arizona", "1HE456456", null },
                    { 5, 1, "Miami", "US", "Power@Ranger.com", 5, "Blake Shelling", "American", "555-7905", 1, "Florida", "1HE093455", null },
                    { 6, 2, "Seattle", "US", "dark@detective.com", 7, "Naomi Wattson", "American", "555-5467", 1, "Washington", "1HE890123", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
