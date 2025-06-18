using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class trzecia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Genre", "Title" },
                values: new object[] { 103, "History", "Sapiens" });

            migrationBuilder.InsertData(
                table: "Borrowing",
                columns: new[] { "Book_Id", "Reader_Id", "BorrowDate", "ReturnStatus" },
                values: new object[] { 103, 1, new DateTime(2025, 5, 20, 17, 30, 5, 0, DateTimeKind.Unspecified), "Borrowed" });

            migrationBuilder.InsertData(
                table: "ReaderBook",
                columns: new[] { "Book_Id", "Reader_Id" },
                values: new object[] { 103, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Borrowing",
                keyColumns: new[] { "Book_Id", "Reader_Id" },
                keyValues: new object[] { 103, 1 });

            migrationBuilder.DeleteData(
                table: "ReaderBook",
                keyColumns: new[] { "Book_Id", "Reader_Id" },
                keyValues: new object[] { 103, 1 });

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 103);
        }
    }
}
