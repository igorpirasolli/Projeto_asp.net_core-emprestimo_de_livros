using EmprestimoLivros.Models;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoLivros.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmprestimosModel> emprestimos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmprestimosModel>().HasData(
                new EmprestimosModel { Id = 1, Recebedor = "Caio Fernando", Fornecedor = "Luiz Eduardo", LivroEmprestado = "It", DataEmprestimo = new DateTime(2025,1,1) },
                new EmprestimosModel { Id = 2, Recebedor = "Lorena Carla", Fornecedor = "Erica Fernandes", LivroEmprestado = "Harry Potter", DataEmprestimo = new DateTime(2025, 1, 1) });
            


            base.OnModelCreating(modelBuilder);
        }
    }
}
