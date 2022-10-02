using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Models.ViewModel;
using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Diagnostics;

namespace GestaoProducao_MVC.Controllers
{
    public class ProcessosController : Controller
    {

        private readonly ProcessoService _processoService;
        private readonly OrdemProdutoService _ordemProdutoService;
        private readonly ApontamentoService _apontamentoService;
        public ProcessosController(ProcessoService processoService,OrdemProdutoService ordemProdutoService, ApontamentoService apontamentoService)
        {
            _processoService = processoService;
            _ordemProdutoService = ordemProdutoService;
            _apontamentoService = apontamentoService;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var list = await _processoService.FindAllAsync();
        //    return View(list);
        //}

        public async Task<IActionResult> Index(string searchString)
        {
            var list = await _processoService.FindByNameCodeAsync(searchString);
            return View(list);
        }



        public IActionResult Create()
        {
            return View();
        }


        //Post Criando Processo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoPeca, Descricao, QuantidadePeca, OrdemProdutoId, DataCriacao, TempoEstimadoSpan")] Processo processo)
        {
            processo.TempoEstimado = processo.TempoEstimadoSpan.Ticks;
            var process = processo;
            try 
            {
                await _processoService.InsertAsync(processo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                return View(processo);

            }

        }


        //Esse metodo só sera acessado por ADM OU MANAGER;
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //Algo deu errado na requisição!
                return BadRequest();
            }

            var processo = await _processoService.FindByIdAsync(id.Value);

            
            if (processo == null)
            {
                //Elemento não encontrado;
                return NotFound();
            }

            //var oP = await _ordemProdutoService.FindByIdAsync(processo.OrdemProdutoId);
            //ProcessoFormViewModel viewModel = new ProcessoFormViewModel()
            //{
            //    OrdemProduto = oP,
            //    Processo = processo
            //};

            return View(processo);
        }

        //Esse metodo só sera acessado por ADM OU MANAGER;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("Id,CodigoPeca, Descricao, QuantidadePeca,OrdemProdutoId ,TempoEstimadoSpan")] Processo processo)
        {
            processo.TempoEstimado = processo.TempoEstimadoSpan.Ticks;
            processo.DataCriacao = DateTime.Now;

            if (id != processo.Id)
            {
                return BadRequest();
            }

          if (ModelState.IsValid)
            {
                await _processoService.UpdateAsync(processo);

                return RedirectToAction(nameof(Index));

            }
            
            return View(processo);
                
        }



        //get remove
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {

                //Algo errado na requisição bebe
                return BadRequest();
            }

            var obj = await _processoService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                //Elemento não encontrado
                return NotFound();
            }

            return View(obj);
 
        }


        //POST DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _processoService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest();
            }
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) 
            {
                //Algo deu errado na requisição;
                return BadRequest();
            }

            var processo = await _processoService.FindByIdAsync(id.Value);

            if (processo == null)
            {
                //Elemento não encontrado;
                return NotFound();
            }

            List<Apontamento> apontamentos = await _apontamentoService.FindByProcesso(processo);

            TimeSpan soma = new TimeSpan(0, 0, 0);
            foreach (var apontamento in apontamentos)
            {
               if (apontamento.TempoTotal != null)
                {
                    TimeSpan decorrido = TimeSpan.FromTicks(apontamento.TempoTotal.Value);
                    soma = soma + decorrido;

                } else
                {
                    TimeSpan pontoAtivo = DateTime.Now - apontamento.DataInicial;
                    soma = soma + pontoAtivo;
                } 
               
            }
            
            processo.TotalTempoDecorrido =  (int)soma.TotalHours + soma.ToString("\\:mm\\:ss");
            
            if (soma.TotalHours > processo.TempoEstimadoSpan.TotalHours)
            {
                TimeSpan diferenca = soma.Subtract(processo.TempoEstimadoSpan);
                string diferencaFormatado = (int)diferenca.TotalHours + diferenca.ToString("\\:mm\\:ss");
                TempData["TempoEstimado"] = "Este processo superou o tempo estimado em " + diferencaFormatado;
            }




            ProcessoViewModel viewModel = new ProcessoViewModel
            {
                Processo = processo,
                Apontamentos = apontamentos
            };

            return View(viewModel);
        }





    }
}
