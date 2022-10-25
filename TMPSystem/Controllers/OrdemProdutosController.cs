using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using GestaoProducao_MVC.Models.ViewModel;
using GestaoProducao_MVC.Filters;
using System.Diagnostics;

namespace GestaoProducao_MVC.Controllers
{

    [UserLogado]
    public class OrdemProdutosController : Controller
    {
        private readonly OrdemProdutoService _ordemProdutoService;
        private readonly ProcessoService _processoService;

        public OrdemProdutosController(OrdemProdutoService ordemProdutoService, ProcessoService processoService)
        {
            _ordemProdutoService = ordemProdutoService;
            _processoService = processoService;
        }



        public async Task<IActionResult> Index()
        {
            var list = await _ordemProdutoService.FindAllAsync();

            return View(list);
        }





        // GET OP Detalhes
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                //Tratar melhor;
                return NotFound();
            }

            var ordemProduto = await _ordemProdutoService.FindByIdAsync(id.Value);

            if (ordemProduto == null)
            {
                //Nao encontrado
                return NotFound();

            }

            //Cria uma lista de processos;
            //EM processoService busca processos que contenham essa OP;
            List<Processo> processos = await _processoService.FindByOpAsync(ordemProduto);
            ordemProduto.Processos = processos;
            
            ordemProduto.TempoTotalEstimadoSpan = ordemProduto.TempoTotalEstimado();
            ordemProduto.TempoEstimadoFormatado = ordemProduto.FormataTempo(ordemProduto.TempoTotalEstimadoSpan);

            ordemProduto.TempoTotalDecorridoSpan = ordemProduto.TempoTotalDecorrido();
            ordemProduto.TempoTotalDecorridoFormatado = ordemProduto.FormataTempo(ordemProduto.TempoTotalDecorridoSpan);


            ordemProduto.TempoTotalParadasSpan = ordemProduto.TempoTotalParadas();
            ordemProduto.TempoTotalParadasFormatado = ordemProduto.FormataTempo(ordemProduto.TempoTotalParadasSpan);

            TimeSpan tempoUtil = ordemProduto.TempoTotalUtil();
            ordemProduto.TempoTotalUtilFormatado = ordemProduto.FormataTempo(tempoUtil);

            ordemProduto.TempoTotalAproxFormatado = ordemProduto.FormataTempo(ordemProduto.TempoAproxPorItem());


            //verifica tempo estimado
            TimeSpan total = ordemProduto.TempoTotalDecorridoSpan;
            TimeSpan estimado = ordemProduto.TempoTotalEstimadoSpan;
            if ( total.TotalHours > estimado.TotalHours)
            {
                TimeSpan diferenca = total.Subtract(estimado);
                string diferencaFormatado = ordemProduto.FormataTempo(diferenca);
                TempData["TempoEstimado"] = "Essa ordem de produção superou o tempo estimado em " + diferencaFormatado;
            }

            //CriaViewModel
            OrdemProdutoViewModel viewModel = new OrdemProdutoViewModel
            {
                OrdemProduto = ordemProduto,
                Processos = processos
            };

            return View(viewModel);
        }   




        //GET: OrdemProdutoes/Create
        public IActionResult Create()
        {
            return View();
        }

       
        //Post - Action do btn para insert no db;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoProduto,QuantidadeProduto,DataVenda,DataEntrega,OpStatus")] OrdemProduto ordemProduto)
        {
            TimeSpan tempoInicial = TimeSpan.Zero;
            string convertidoTempo = ordemProduto.FormataTempo(tempoInicial);
            ordemProduto.TempoEstimadoFormatado = convertidoTempo;
            ordemProduto.TempoTotalDecorridoFormatado = convertidoTempo;
            ordemProduto.TempoTotalParadasFormatado = convertidoTempo;
            ordemProduto.TempoTotalUtilFormatado = convertidoTempo;
            ordemProduto.TempoTotalAproxFormatado = convertidoTempo;

            try { 
            
                await _ordemProdutoService.InsertAsync(ordemProduto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(ordemProduto);
            }
            
        }

        //Get - Retorna pagina edit com info da OP;
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();//Algo errado na requisição;
            }

            var ordemProduto = await _ordemProdutoService.FindByIdAsync(id.Value);
            if (ordemProduto == null)
            {
                return NotFound();//Elemento nao encontrado no DB;
            }

            //Se tudo certo retorna view e envia a OP;
            return View(ordemProduto);
        }



        //Post Edit OP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,OrdemProduto ordemProduto)
        {
            if(id != ordemProduto.Id)
            {
                return BadRequest();
            }
            //Se obj for inválido
            if (!ModelState.IsValid)
            {
                return View(ordemProduto);
            }

            try
            {
                //Chama o Update do Serviço e envia OP
                await _ordemProdutoService.UpdateAsync(ordemProduto);
                return RedirectToAction(nameof(Index));

            } catch 
            {
                //Caso der algo errado no update..
                //Tratar melhor este erro.
                return NotFound();

            }
            
        }





        //Get Delete OP
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //Tratar melhor esse erro;
                return BadRequest();
            }


            var obj = await _ordemProdutoService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //Metodo Delete Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _ordemProdutoService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest();
            }
        }

     }
}
