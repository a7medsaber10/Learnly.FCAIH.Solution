using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Learnly.Repository.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31C0AC99-29F4-4A55-AF0F-07402617FC47", null, "Student", "STUDENT" },
                    { "C1DA0795-351E-45E3-8C4F-74DA1438BB50", null, "Teacher", "TEACHER" },
                    { "DDC16163-28DC-4325-BECE-56A2B5BBE8E0", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "IsApproved", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0BE7B103-1D31-420F-853C-EE3BC9236FB4", 0, "961d70ce-39b5-4548-86cc-a996e4d211f2", "System Admin", "admin@gmail.com", true, false, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEArs8pUic5SrjLJs3ozG8jQ2fmLqbbfMNulVi8yZFYzMxFqLKoxhMj4eg7ySyvf9BA==", null, false, "48f0ce8e-22f1-4f09-913d-6f6e27708e19", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "DDC16163-28DC-4325-BECE-56A2B5BBE8E0", "0BE7B103-1D31-420F-853C-EE3BC9236FB4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31C0AC99-29F4-4A55-AF0F-07402617FC47");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "C1DA0795-351E-45E3-8C4F-74DA1438BB50");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "DDC16163-28DC-4325-BECE-56A2B5BBE8E0", "0BE7B103-1D31-420F-853C-EE3BC9236FB4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DDC16163-28DC-4325-BECE-56A2B5BBE8E0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0BE7B103-1D31-420F-853C-EE3BC9236FB4");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
