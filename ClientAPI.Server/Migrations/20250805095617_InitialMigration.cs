using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClientAPI.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RefreshTokenCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshTokenExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SavingsDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsActive", "LastName", "MiddleName", "Password", "PasswordSalt", "RefreshToken", "RefreshTokenCreatedAt", "RefreshTokenExpiresAt", "Role" },
                values: new object[,]
                {
                    { new Guid("2247f43c-7b46-432d-ab5c-637c8ae9bfef"), "anna@gmail.com", "Anna", true, "Cruz", "Gerona", new byte[] { 189, 4, 77, 105, 124, 5, 120, 91, 118, 96, 142, 183, 241, 115, 53, 163, 242, 90, 80, 9, 249, 114, 83, 122, 208, 81, 200, 99, 45, 160, 221, 205, 114, 174, 217, 35, 44, 153, 236, 21, 121, 133, 35, 165, 225, 41, 147, 159, 235, 176, 200, 13, 217, 182, 178, 98, 49, 4, 158, 224, 160, 193, 54, 71 }, new byte[] { 129, 176, 47, 19, 253, 4, 209, 168, 192, 126, 104, 118, 77, 120, 11, 9, 72, 241, 184, 178, 222, 95, 83, 225, 60, 187, 103, 245, 58, 107, 17, 213, 196, 122, 50, 73, 71, 115, 79, 104, 52, 163, 198, 190, 225, 82, 143, 234, 135, 205, 46, 243, 65, 14, 175, 196, 6, 89, 167, 71, 126, 97, 130, 147, 129, 4, 162, 186, 100, 231, 154, 32, 10, 68, 10, 117, 213, 28, 214, 72, 197, 148, 171, 166, 212, 231, 120, 130, 44, 191, 94, 247, 54, 241, 163, 194, 115, 123, 81, 74, 162, 7, 127, 235, 34, 52, 135, 149, 57, 108, 178, 130, 101, 207, 19, 132, 17, 23, 121, 149, 192, 71, 205, 106, 88, 16, 14, 157 }, "", new DateTime(2025, 8, 5, 17, 56, 17, 422, DateTimeKind.Unspecified).AddTicks(8328), new DateTime(2025, 8, 12, 17, 56, 17, 422, DateTimeKind.Unspecified).AddTicks(8329), 1 },
                    { new Guid("c6db612f-d73b-4c04-b8c1-21af92d3db79"), "admin@gmail.com", "System", true, "User", "Admin", new byte[] { 14, 72, 101, 192, 56, 233, 72, 68, 69, 21, 69, 80, 168, 83, 249, 4, 93, 53, 67, 61, 154, 102, 69, 15, 24, 94, 29, 174, 204, 112, 55, 21, 67, 143, 123, 74, 244, 113, 204, 204, 213, 251, 224, 67, 30, 0, 104, 165, 195, 36, 115, 27, 82, 91, 68, 105, 23, 77, 171, 127, 139, 221, 105, 30 }, new byte[] { 83, 160, 28, 40, 178, 71, 96, 54, 150, 93, 240, 182, 41, 77, 42, 66, 90, 47, 153, 210, 254, 46, 128, 12, 77, 60, 216, 215, 190, 211, 179, 166, 127, 14, 211, 45, 118, 186, 107, 27, 207, 239, 58, 203, 220, 24, 96, 58, 240, 232, 55, 45, 90, 196, 116, 68, 160, 13, 246, 185, 25, 77, 65, 89, 159, 226, 132, 240, 108, 191, 105, 236, 43, 189, 195, 124, 125, 66, 155, 112, 95, 87, 138, 233, 153, 96, 113, 166, 79, 56, 81, 25, 192, 70, 34, 182, 78, 21, 93, 143, 20, 148, 131, 14, 227, 82, 207, 224, 159, 81, 69, 190, 83, 199, 102, 101, 170, 228, 133, 140, 124, 120, 248, 167, 215, 48, 140, 160 }, "", new DateTime(2025, 8, 5, 17, 56, 17, 422, DateTimeKind.Unspecified).AddTicks(8291), new DateTime(2025, 8, 12, 17, 56, 17, 422, DateTimeKind.Unspecified).AddTicks(8324), 0 },
                    { new Guid("d0107e18-3d56-44f2-a7e6-7c73d4e93e8f"), "mark@gmail.com", "Mark", true, "Rivera", "Tan", new byte[] { 164, 147, 33, 110, 174, 42, 170, 41, 30, 15, 162, 50, 124, 74, 74, 96, 153, 164, 232, 31, 26, 36, 73, 158, 28, 95, 99, 12, 114, 201, 79, 99, 159, 10, 194, 47, 144, 9, 161, 111, 42, 159, 114, 20, 225, 8, 62, 83, 6, 79, 219, 109, 217, 2, 196, 103, 243, 95, 201, 204, 121, 17, 63, 90 }, new byte[] { 63, 124, 150, 197, 174, 199, 161, 74, 132, 194, 59, 90, 53, 211, 177, 164, 6, 108, 162, 89, 25, 117, 100, 113, 80, 143, 25, 225, 31, 221, 168, 163, 181, 104, 68, 20, 130, 223, 103, 38, 130, 4, 206, 209, 98, 253, 245, 186, 161, 74, 57, 99, 127, 113, 151, 170, 6, 125, 186, 46, 144, 100, 156, 73, 9, 81, 91, 69, 186, 35, 230, 196, 59, 163, 184, 187, 137, 139, 64, 209, 29, 71, 233, 106, 89, 52, 40, 218, 203, 217, 128, 117, 110, 134, 215, 124, 213, 207, 142, 95, 165, 211, 87, 156, 4, 89, 209, 246, 147, 117, 148, 143, 222, 177, 159, 246, 33, 188, 121, 203, 223, 31, 108, 138, 1, 150, 18, 132 }, "", new DateTime(2025, 8, 5, 17, 56, 17, 422, DateTimeKind.Unspecified).AddTicks(8330), new DateTime(2025, 8, 12, 17, 56, 17, 422, DateTimeKind.Unspecified).AddTicks(8331), 1 },
                    { new Guid("fafe0885-91cc-4dce-82b3-e7d41a98f3e0"), "sofie@gmail.com", "Sofia", true, "Lopez", "Go", new byte[] { 146, 158, 52, 182, 1, 252, 60, 202, 155, 195, 9, 230, 69, 172, 49, 7, 177, 85, 25, 203, 160, 248, 147, 219, 178, 114, 29, 150, 32, 18, 194, 126, 80, 13, 169, 88, 251, 243, 209, 157, 222, 50, 249, 131, 124, 129, 213, 61, 169, 193, 115, 170, 214, 122, 87, 107, 108, 168, 79, 132, 0, 101, 84, 204 }, new byte[] { 125, 236, 255, 238, 206, 210, 218, 181, 100, 225, 127, 129, 198, 32, 170, 86, 98, 188, 160, 236, 213, 18, 88, 0, 43, 201, 253, 74, 73, 13, 217, 190, 235, 89, 72, 114, 123, 198, 154, 24, 118, 99, 103, 162, 63, 95, 74, 67, 90, 0, 5, 116, 199, 87, 113, 63, 31, 24, 144, 5, 237, 92, 159, 161, 43, 254, 76, 32, 220, 156, 159, 38, 109, 146, 242, 204, 93, 190, 32, 41, 244, 197, 222, 12, 205, 169, 147, 39, 139, 125, 47, 143, 112, 182, 44, 63, 139, 146, 16, 142, 177, 32, 165, 206, 30, 116, 132, 30, 150, 94, 197, 79, 164, 236, 235, 63, 128, 125, 152, 123, 240, 64, 176, 76, 63, 43, 188, 58 }, "", new DateTime(2025, 8, 5, 17, 56, 17, 422, DateTimeKind.Unspecified).AddTicks(8332), new DateTime(2025, 8, 12, 17, 56, 17, 422, DateTimeKind.Unspecified).AddTicks(8333), 1 }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Birthday", "SavingsDeposit", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200m, new Guid("2247f43c-7b46-432d-ab5c-637c8ae9bfef") },
                    { 2, new DateTime(1985, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25000m, new Guid("d0107e18-3d56-44f2-a7e6-7c73d4e93e8f") },
                    { 3, new DateTime(1975, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 3000m, new Guid("fafe0885-91cc-4dce-82b3-e7d41a98f3e0") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
