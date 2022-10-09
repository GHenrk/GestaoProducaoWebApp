using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace GestaoProducao_MVC.Controllers
{
    public class LoginController : Controller
    {

        private readonly UsuarioService _usuarioService;
        private readonly SessaoService _sessaoService; 
        public LoginController(UsuarioService usuarioService, SessaoService sessaoService)
        {
            _usuarioService = usuarioService;
            _sessaoService = sessaoService;
        }

        public IActionResult Index()
        {
            if (_sessaoService.BuscarSessaoDoUsuario() != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult RemoverSessao()
        {
            _sessaoService.RemoverSessaoUsuario();

            return RedirectToAction("Index", "Login");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                   Usuario usuario = _usuarioService.FindByLogin(loginModel.Login);
                    if (usuario != null)
                    {
                        if (usuario.SenhaIsValid(loginModel.Senha))
                        {
                            _sessaoService.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }


                        TempData["MensagemErro"] = $"Senha do usuário é inválida! Por favor, tente novamente";
                        return View(nameof(Index));
                    }

                    TempData["MensagemErro"] = $"Usuário ou senha incorreto! Por favor, tente novamente";
                }

                return View(nameof(Index));
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $" Não conseguimos realizar seu login! Error: {erro.Message}";
                return RedirectToAction(nameof(Index));
            }
        }



    }
}
