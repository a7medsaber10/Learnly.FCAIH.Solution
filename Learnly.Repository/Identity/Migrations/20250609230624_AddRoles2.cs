using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learnly.Repository.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "DDC16163-28DC-4325-BECE-56A2B5BBE8E0", "0BE7B103-1D31-420F-853C-EE3BC9236FB4" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0BE7B103-1D31-420F-853C-EE3BC9236FB4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "IsApproved", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "22930a4b-96b7-49ec-a5b8-c3aa100f11fe", 0, "78e08064-fae7-4ab9-be3b-76670d7f6ea3", "System Admin", "admin@gmail.com", true, false, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEGm9cQqtKbleJ9VePwVSDixBaewc1OxveZBFjX2VROgAQUAREfoiUpV6UXoTQvhQgg==", null, false, "acbce4b7-5b8a-4653-83a9-2fae72434ea4", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "DDC16163-28DC-4325-BECE-56A2B5BBE8E0", "22930a4b-96b7-49ec-a5b8-c3aa100f11fe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "DDC16163-28DC-4325-BECE-56A2B5BBE8E0", "22930a4b-96b7-49ec-a5b8-c3aa100f11fe" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22930a4b-96b7-49ec-a5b8-c3aa100f11fe");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "IsApproved", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0BE7B103-1D31-420F-853C-EE3BC9236FB4", 0, "961d70ce-39b5-4548-86cc-a996e4d211f2", "System Admin", "admin@gmail.com", true, false, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEArs8pUic5SrjLJs3ozG8jQ2fmLqbbfMNulVi8yZFYzMxFqLKoxhMj4eg7ySyvf9BA==", null, false, "48f0ce8e-22f1-4f09-913d-6f6e27708e19", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "DDC16163-28DC-4325-BECE-56A2B5BBE8E0", "0BE7B103-1D31-420F-853C-EE3BC9236FB4" });
        }
    }
}
