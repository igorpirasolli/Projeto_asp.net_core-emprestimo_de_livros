using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimoLivros.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "emprestimos",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataUltimaAtualizacao",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "emprestimos",
                columns: new[] { "Id", "DataUltimaAtualizacao", "Fornecedor", "LivroEmprestado", "Recebedor" },
                values: new object[] { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Erica Fernandes", "Harry Potter", "Lorena Carla" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "emprestimos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "emprestimos",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataUltimaAtualizacao",
                value: new DateTime(2025, 1, 26, 12, 37, 12, 739, DateTimeKind.Local).AddTicks(1031));
        }
    }
}
