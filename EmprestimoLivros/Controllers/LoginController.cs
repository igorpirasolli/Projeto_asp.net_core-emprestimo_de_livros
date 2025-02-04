using EmprestimoLivros.Data;
using EmprestimoLivros.Dto;
using EmprestimoLivros.Services.LoginService;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginInterface _login;
        public LoginController(ILoginInterface login)
        {
            _login = login;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioRegisterDto usuariodto)
        {
           if (ModelState.IsValid)
            {
                var usuario = await _login.Registrar(usuariodto);

                if(usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;

                }
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                    return View(usuariodto);
                }

                return RedirectToAction("Index");
            }
           else
            {
                return View(usuariodto);
            }
        }
    }
}
