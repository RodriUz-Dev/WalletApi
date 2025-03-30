using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionHistoryWalletId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_Wallets_WalletId",
                table: "TransactionsHistory");

            migrationBuilder.AlterColumn<int>(
                name: "WalletId",
                table: "TransactionsHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_Wallets_WalletId",
                table: "TransactionsHistory",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_Wallets_WalletId",
                table: "TransactionsHistory");

            migrationBuilder.AlterColumn<int>(
                name: "WalletId",
                table: "TransactionsHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_Wallets_WalletId",
                table: "TransactionsHistory",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }
    }
}
