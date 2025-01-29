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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmprestimosModel emprestimosModel)
        {
            if (ModelState.IsValid)
            {
                _context.emprestimos.Add(emprestimosModel);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso";

                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Algum erro ocorreu ao realizar o cadastro!";

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimosModel emprestimosModel = _context.emprestimos.FirstOrDefault(emp => emp.Id == id);

            if (emprestimosModel == null)
            {
                return NotFound();
            }

            return View(emprestimosModel);

        }

        [HttpPost]
        public IActionResult Edit(EmprestimosModel emprestimosModel)
        {
            if (ModelState.IsValid)
            {
                _context.emprestimos.Update(emprestimosModel);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Editação realizado com sucesso";

                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Algum erro ocorreu ao realizar a edição!";

            return View(emprestimosModel);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimosModel emprestimosModel = _context.emprestimos.FirstOrDefault(emp => emp.Id == id);

            if (emprestimosModel == null)
            {
                return NotFound();
            }

            return View(emprestimosModel);
        }

        [HttpPost]
        public IActionResult Delete(EmprestimosModel emprestimosModel)
        {
            if (emprestimosModel == null)  { return NotFound(); }

            _context.emprestimos.Remove(emprestimosModel);
            _context.SaveChanges();

            TempData["MensagemSucesso"] = "Remoção realizada com sucesso";

            return RedirectToAction("Index");
        }


    }
}
