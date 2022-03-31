using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreTemplate.Data.Migrations
{
    public partial class DebitCardUpdateCVCCOdeCardNumberToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CardNumber",
                table: "DebitCard",
                type: "int",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CVCCode",
                table: "DebitCard",
                type: "int",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "DebitCard",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "CVCCode",
                table: "DebitCard",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 3);
        }
    }
}
