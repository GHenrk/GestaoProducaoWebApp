using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProducao_MVC.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly SessaoService _sessaoService;

        public AlterarSenhaController(UsuarioService usuarioService,
                                      SessaoService sessaoService)
        {
            _usuarioService = usuarioService;
            _sessaoService = sessaoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alterar(AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                Usuario usuarioLogado = _sessaoService.BuscarSessaoDoUsuario();
                alterarSenhaModel.Id = usuarioLogado.Id;

                if (ModelState.IsValid)
                {

                   await _usuarioService.AlterarSenha(alterarSenhaModel);
                    TempData["MensagemSucesso"] = $"Senha alterada com sucesso!";


                    return View("Index", alterarSenhaModel);
                }

                TempData["MensagemErro"] = $"Algo deu errado";
                return View("Index", alterarSenhaModel);
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, não conseguimos alterar sua senha, tente novamente. Erro: {erro.Message}";
                return View("Index", alterarSenhaModel);
            }

            return View("Index", alterarSenhaModel);


        }

    }
}
