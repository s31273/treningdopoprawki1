using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class druga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Genre", "Title" },
                values: new object[,]
                {
                    { 101, "Fantasy", "The Hobbit" },
                    { 102, "Fantasy", "Harry Potter" }
                });

            migrationBuilder.InsertData(
                table: "Reader",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "anna.nowak@example.edu", "Anna Nowwak" },
                    { 2, "anna.kowalska@example.edu", "Anna Kowalska" }
                });

            migrationBuilder.InsertData(
                table: "Borrowing",
                columns: new[] { "Book_Id", "Reader_Id", "BorrowDate", "ReturnStatus" },
                values: new object[,]
                {
                    { 101, 1, new DateTime(2025, 6, 18, 18, 30, 0, 0, DateTimeKind.Unspecified), "Borrowed" },
                    { 102, 2, new DateTime(2025, 5, 13, 12, 30, 0, 0, DateTimeKind.Unspecified), "Borrowed" }
                });

            migrationBuilder.InsertData(
                table: "ReaderBook",
                columns: new[] { "Book_Id", "Reader_Id" },
                values: new object[,]
                {
                    { 101, 1 },
                    { 102, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Borrowing",
                keyColumns: new[] { "Book_Id", "Reader_Id" },
                keyValues: new object[] { 101, 1 });

            migrationBuilder.DeleteData(
                table: "Borrowing",
                keyColumns: new[] { "Book_Id", "Reader_Id" },
                keyValues: new object[] { 102, 2 });

            migrationBuilder.DeleteData(
                table: "ReaderBook",
                keyColumns: new[] { "Book_Id", "Reader_Id" },
                keyValues: new object[] { 101, 1 });

            migrationBuilder.DeleteData(
                table: "ReaderBook",
                keyColumns: new[] { "Book_Id", "Reader_Id" },
                keyValues: new object[] { 102, 2 });

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Reader",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reader",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
