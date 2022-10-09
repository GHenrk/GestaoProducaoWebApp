using GestaoProducao_MVC.Filters;
using GestaoProducao_MVC.Helper;
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
        private readonly EmailService _email;

        public LoginController(UsuarioService usuarioService, SessaoService sessaoService, EmailService email)
        {
            _usuarioService = usuarioService;
            _sessaoService = sessaoService;
            _email = email;
        }

        public IActionResult Index()
        {
            if (_sessaoService.BuscarSessaoDoUsuario() != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }



        public IActionResult RedefinirSenha()
        {
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Usuario usuario = _usuarioService.FindByLoginEmail(redefinirSenhaModel.Email, redefinirSenhaModel.Login);
                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Sua senha foi redefinida! Nova senha: {novaSenha}";
                        bool emailEnviado = _email.Enviar(usuario.Email, "TMPSystem - Nova senha", mensagem);

                        if (emailEnviado)
                        {
                            await _usuarioService.UpdateAsync(usuario);
                            TempData["MensagemSucesso"] = $"Tudo certo! Enviamos para seu e-mail cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Infezlimente algo deu errado ao enviar o e-mail! Por favor, tente novamente.";
                        }
                        return RedirectToAction("Index", "Login");

                    }

                    TempData["MensagemErro"] = $"Não encontramos um usuário com estas informações! Por favor, verifique e tente novamente.";
                }

                return RedirectToAction("RedefinirSenha", "Login");

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha! Error: {erro.Message}";
                return RedirectToAction("RedefinirSenha", "Login");
            }


        }


    }
}
