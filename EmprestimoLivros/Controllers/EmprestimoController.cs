﻿using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly AppDbContext _context;

        public EmprestimoController(AppDbContext dbContext) => _context = dbContext;
       
        public async Task<IActionResult> Index()
        {
            IEnumerable<EmprestimosModel> resultado = await _context.emprestimos.ToListAsync();
            return View(resultado);
        }
    }
}
