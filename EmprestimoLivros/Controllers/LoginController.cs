using EmprestimoLivros.Data;
using EmprestimoLivros.Dto;
using EmprestimoLivros.Services.LoginService;
using EmprestimoLivros.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginInterface _login;
        private readonly ISessaoInterface _sessao;
        public LoginController(ILoginInterface login, ISessaoInterface sessao)
        {
            _login = login;
            _sessao = sessao;
        }
        public IActionResult Login()
        {

            var usuario = _sessao.BuscarSessao();
            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            _sessao.RemoverSessao();
            return RedirectToAction("Login");
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

                return RedirectToAction("Login");
            }
           else
            {
                return View(usuariodto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _login.Login(usuarioLoginDto);

                if (usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                    return View(usuarioLoginDto);
                }
            }
            else
            {
                return View(usuarioLoginDto);
            }
        }
    }
}
