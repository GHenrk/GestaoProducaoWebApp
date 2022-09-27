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

           //Fazer um método sozinho pra isso;
            foreach(var item in list)
            {   
                if (item.TempoTotal != null)
                {
                    item.TotalTime = TimeSpan.FromTicks(item.TempoTotal.Value);
                }
                
            }

            return View(list.OrderByDescending(x => x.Status == Models.Enums.AptStatus.Ativo));
        }


        public IActionResult Create()
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

        //View que encerra um ponto
        public async Task<IActionResult> Finalizar()
        {
            return View();
        }




        //Ação que encerra um ponto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finalizar(int id, string? descricao)
        {
            //Busca apontamento do funcionario que esteja ativo
             Apontamento apontamentoAtivo = await _apontamentoService.FindByIdStatus(id);

            if (apontamentoAtivo == null)
            {   
                //Você não tem um apontamento ativo.
                return BadRequest();
            }

            apontamentoAtivo.DataFinal = DateTime.Now;
            apontamentoAtivo.Descricao = descricao;
            apontamentoAtivo.Status = Models.Enums.AptStatus.Encerrado ;


            TimeSpan tempoTotal =(TimeSpan)(apontamentoAtivo.DataFinal - apontamentoAtivo.DataInicial);


            apontamentoAtivo.TempoTotal = tempoTotal.Ticks;


            if (ModelState.IsValid)
            {
                await _apontamentoService.UpdateAsync(apontamentoAtivo);
                return RedirectToAction(nameof(Index));
            }

            return View() ;

        }










    }
}
