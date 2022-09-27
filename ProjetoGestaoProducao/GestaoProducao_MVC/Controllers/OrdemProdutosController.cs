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

namespace GestaoProducao_MVC.Controllers
{
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

            var obj = await _ordemProdutoService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                //Nao encontrado
                return NotFound();

            }

            //Cria uma lista de processos;
            //EM processoService busca processos que contenham essa OP;
            List<Processo> processos = await _processoService.FindByOpAsync(obj);

            //CriaViewModel
            OrdemProdutoViewModel viewModel = new OrdemProdutoViewModel
            {
                OrdemProduto = obj,
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
        public async Task<IActionResult> Create([Bind("CodigoProduto,QuantidadeProduto,DataVenda,DataEntrega")] OrdemProduto ordemProduto)
        {
            if (ModelState.IsValid)
            {
                await _ordemProdutoService.InsertAsync(ordemProduto);
                return RedirectToAction(nameof(Index));
            }
            return View(ordemProduto);
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
