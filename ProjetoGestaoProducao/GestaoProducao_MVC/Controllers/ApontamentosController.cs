using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProducao_MVC.Controllers
{
    public class ApontamentosController : Controller
    {

        private readonly ApontamentoService _apontamentoService;
        private readonly OrdemProdutoService _ordemProdutoService;
        public ApontamentosController (ApontamentoService apontamentoService, OrdemProdutoService ordemProdutoService
            )
        {
            _apontamentoService = apontamentoService;
            _ordemProdutoService = ordemProdutoService;
        }


        public async Task<IActionResult> Index(string searchString)
        {
            var list = await _apontamentoService.FindByNameCodeAsync(searchString);
            return View(list.OrderByDescending(x => x.Status == Models.Enums.AptStatus.Ativo));
        }


        public async Task<IActionResult> Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProcessoId, FuncionarioId,MaquinaId,Operacao,DataInicial,Status")] Apontamento apontamento)
        {

            if (ModelState.IsValid)
            {
                await _apontamentoService.InsertAsync(apontamento);
                return RedirectToAction(nameof(Index));
            
            }

            return View(apontamento);

        }







    }
}
