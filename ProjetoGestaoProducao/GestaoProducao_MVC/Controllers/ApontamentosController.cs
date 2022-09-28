using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestaoProducao_MVC.Controllers
{
    public class ApontamentosController : Controller
    {

        private readonly ApontamentoService _apontamentoService;
        private readonly OrdemProdutoService _ordemProdutoService;
        private readonly ProcessoService _processoService;
        private readonly FuncionarioService _funcionarioService;
        public ApontamentosController (ApontamentoService apontamentoService, OrdemProdutoService ordemProdutoService, ProcessoService processoService, FuncionarioService funcionarioService
            )
        {
            _apontamentoService = apontamentoService;
            _ordemProdutoService = ordemProdutoService;
            _processoService = processoService;
            _funcionarioService = funcionarioService;
        }


        public async Task<IActionResult> Index(string searchString)
        {
            var list = await _apontamentoService.FindByNameCodeAsync(searchString);

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
            //Verifica se existe pontoAtivo
            bool ativo = await _apontamentoService.isFuncionarioAtivo(apontamento.FuncionarioId);
            if (ativo)
            {
                ViewData["Ativo"] = "Este funcionário já possui um apontamento ativo!";
                return View(apontamento);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    apontamento.IsAtivo = true;
                    await _apontamentoService.InsertAsync(apontamento);
                    return RedirectToAction(nameof(Index));

                }
            } 
            catch
            {
                return View(apontamento);
            }

            return View(apontamento);

        }

        //View que encerra um ponto
        public IActionResult Finalizar()
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

            var obj = apontamentoAtivo;


            if (apontamentoAtivo == null)
            {   
                //Você não tem um apontamento ativo.
                return View();
            }

            apontamentoAtivo.DataFinal = DateTime.Now;
            apontamentoAtivo.Descricao = descricao;
            apontamentoAtivo.Status = Models.Enums.AptStatus.Encerrado ;
            apontamentoAtivo.IsAtivo = false;


            TimeSpan tempoTotal =(TimeSpan)(apontamentoAtivo.DataFinal - apontamentoAtivo.DataInicial);


            apontamentoAtivo.TempoTotal = tempoTotal.Ticks;


            if (ModelState.IsValid)
            {
                await _apontamentoService.UpdateAsync(apontamentoAtivo);
                return RedirectToAction(nameof(Index));
            }

            return View() ;

        }


        //Só permite editar pontos encerrados;
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var obj = await _apontamentoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                //Elemento nao encontrado;
                return NotFound();
            }

            if (obj.IsAtivo == true)
            {
                //VocÊ não pode alterar um apontamento ativo
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }



        ///PAREI AQUI
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Apontamento apontamento)
        {

            return RedirectToAction(nameof(Index));
        }




    }
}
