using ClosedXML.Excel;
using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using EmprestimoLivros.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmprestimoLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISessaoInterface _sessaoInterface;

        public EmprestimoController(AppDbContext dbContext, ISessaoInterface sessaoInterface)
        {
            _context = dbContext;
            _sessaoInterface = sessaoInterface;
        }
       
        public IActionResult Index()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            IEnumerable<EmprestimosModel> resultado =  _context.emprestimos;
            return View(resultado);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmprestimosModel emprestimosModel)
        {
            if (ModelState.IsValid)
            {
                emprestimosModel.DataEmprestimo = DateTime.Now;

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

            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

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
                var emprestimosBD = _context.emprestimos.Find(emprestimosModel.Id);

                emprestimosBD.Fornecedor = emprestimosModel.Fornecedor;
                emprestimosBD.Recebedor = emprestimosModel.Recebedor;
                emprestimosBD.LivroEmprestado = emprestimosModel.LivroEmprestado;

                _context.emprestimos.Update(emprestimosBD);
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

            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

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

        public IActionResult Exportar()
        {
            var dados = GetData();

            using(XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Dados Empréstimos");

                using(MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Emprestimo.xls");
                }
            }

            
        }

        private DataTable GetData()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados Empréstimo";

            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Fornecedor", typeof(string));
            dataTable.Columns.Add("Livro", typeof(string));
            dataTable.Columns.Add("Data empréstimo", typeof(DateTime));

            var dados = _context.emprestimos.ToList();

            if (dados.Count > 0)
            {
                dados.ForEach(emprestimo =>
                {
                    dataTable.Rows.Add(
                        emprestimo.Recebedor, 
                        emprestimo.Fornecedor,
                        emprestimo.LivroEmprestado, 
                        emprestimo.DataEmprestimo);
                });
            }

            return dataTable;
        }


    }
}
