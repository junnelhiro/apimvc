using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apihiro.Migrations
{
    /// <inheritdoc />
    public partial class adddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 1, "jhon", "$2a$11$sR34AGmNkZt6g3AD0WcyJujRdw4kr.BFuuwvYjreZCLjbvOdHHlq." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
