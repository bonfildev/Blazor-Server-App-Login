using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor_Server_App_Login.Migrations
{
    /// <inheritdoc />
    public partial class segunda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperDigito");

            migrationBuilder.CreateTable(
                name: "SDigito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SDigito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SDigito_UsersLogin_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersLogin",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SDigito_UserId",
                table: "SDigito",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SDigito");

            migrationBuilder.CreateTable(
                name: "SuperDigito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Number = table.Column<long>(type: "bigint", nullable: true),
                    Result = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperDigito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperDigito_UsersLogin_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersLogin",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuperDigito_UserId",
                table: "SuperDigito",
                column: "UserId");
        }
    }
}
