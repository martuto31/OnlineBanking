using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreTemplate.Data.Migrations
{
    public partial class TransactionsTestDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "Transactions",
               columns: new[] { "Id", "Date", "Message", "TransactionCurrency", "Receipt", "Payment", "DebitCardId", "CreatedOn", "Balance" },
               values: new object[] { 1, "10/12/2012", "msg", 3, 10, 0, 1, "10/12/2012", 0 });

            migrationBuilder.InsertData(
               table: "Transactions",
               columns: new[] { "Id", "Date", "Message", "TransactionCurrency", "Receipt", "Payment", "DebitCardId", "CreatedOn", "Balance" },
               values: new object[] { 2, "11/12/2012", "msg", 2, 20.15, 0, 1, "11/12/2012", 0 });

            migrationBuilder.InsertData(
               table: "Transactions",
               columns: new[] { "Id", "Date", "Message", "TransactionCurrency", "Receipt", "Payment", "DebitCardId", "CreatedOn", "Balance" },
               values: new object[] { 3, "12/12/2012", "msg", 1, 0, 350.50, 1, "12/12/2012", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
