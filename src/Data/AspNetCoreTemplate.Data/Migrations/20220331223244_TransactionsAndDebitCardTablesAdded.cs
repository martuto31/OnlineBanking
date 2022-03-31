using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreTemplate.Data.Migrations
{
    public partial class TransactionsAndDebitCardTablesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebitCard_Accounts_AccountId",
                table: "DebitCard");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_DebitCard_DebitCardId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DebitCard",
                table: "DebitCard");

            migrationBuilder.RenameTable(
                name: "DebitCard",
                newName: "DebitCards");

            migrationBuilder.RenameIndex(
                name: "IX_DebitCard_AccountId",
                table: "DebitCards",
                newName: "IX_DebitCards_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DebitCards",
                table: "DebitCards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DebitCards_Accounts_AccountId",
                table: "DebitCards",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_DebitCards_DebitCardId",
                table: "Transactions",
                column: "DebitCardId",
                principalTable: "DebitCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebitCards_Accounts_AccountId",
                table: "DebitCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_DebitCards_DebitCardId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DebitCards",
                table: "DebitCards");

            migrationBuilder.RenameTable(
                name: "DebitCards",
                newName: "DebitCard");

            migrationBuilder.RenameIndex(
                name: "IX_DebitCards_AccountId",
                table: "DebitCard",
                newName: "IX_DebitCard_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DebitCard",
                table: "DebitCard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DebitCard_Accounts_AccountId",
                table: "DebitCard",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_DebitCard_DebitCardId",
                table: "Transactions",
                column: "DebitCardId",
                principalTable: "DebitCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
