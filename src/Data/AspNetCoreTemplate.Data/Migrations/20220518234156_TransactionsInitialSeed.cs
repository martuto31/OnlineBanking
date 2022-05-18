using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreTemplate.Data.Migrations
{
    public partial class TransactionsInitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Date", "Message", "TransactionCurrency", "Receipt", "Payment", "DebitCardId", "CreatedOn", },
                values: new object[] { 1, "12/12/2012", "msg", 3, 10, 20, 1, "12/12/2012" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
