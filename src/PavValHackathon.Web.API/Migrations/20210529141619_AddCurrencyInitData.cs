using Microsoft.EntityFrameworkCore.Migrations;

namespace PavValHackathon.Web.API.Migrations
{
    public partial class AddCurrencyInitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "USD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
