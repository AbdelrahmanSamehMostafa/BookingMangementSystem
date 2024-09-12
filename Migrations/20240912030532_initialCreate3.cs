using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingMangementSystem.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "517ec86f-f72e-462e-b392-ba34d254b40e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b103e0a4-1d35-4356-abc7-8b8ec4a62d9f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "76783b8f-1bf8-44eb-a71d-e029196771ba", null, "admin", "admin" },
                    { "c32bb34b-e291-4ee9-b085-aaed800e2322", null, "user", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76783b8f-1bf8-44eb-a71d-e029196771ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c32bb34b-e291-4ee9-b085-aaed800e2322");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "517ec86f-f72e-462e-b392-ba34d254b40e", null, "admin", "user" },
                    { "b103e0a4-1d35-4356-abc7-8b8ec4a62d9f", null, "user", null }
                });
        }
    }
}
