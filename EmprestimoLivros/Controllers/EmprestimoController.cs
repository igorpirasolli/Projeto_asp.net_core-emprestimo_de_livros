using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly AppDbContext _context;

        public EmprestimoController(AppDbContext dbContext) => _context = dbContext;
       
        public IActionResult Index()
        {
            IEnumerable<EmprestimosModel> resultado =  _context.emprestimos;
            return View(resultado);
        }
    }
}
