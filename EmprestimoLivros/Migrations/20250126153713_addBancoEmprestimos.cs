using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimoLivros.Migrations
{
    /// <inheritdoc />
    public partial class addBancoEmprestimos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "emprestimos",
                columns: new[] { "Id", "DataUltimaAtualizacao", "Fornecedor", "LivroEmprestado", "Recebedor" },
                values: new object[] { 1, new DateTime(2025, 1, 26, 12, 37, 12, 739, DateTimeKind.Local).AddTicks(1031), "Luiz Eduardo", "It", "Caio Fernando" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "emprestimos",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
