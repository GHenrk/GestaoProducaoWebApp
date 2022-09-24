using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProducao_MVC.Controllers
{
    public class ProcessosController : Controller
    {

        private readonly ProcessoService _processoService;
        private readonly OrdemProdutoService _ordemProdutoService;
        public ProcessosController(ProcessoService processoService,OrdemProdutoService ordemProdutoService)
        {
            _processoService = processoService;
            _ordemProdutoService = ordemProdutoService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _processoService.FindAllAsync();
            return View(list);
        }



        public async Task<IActionResult> Create()
        {
            return View();
        }


        //Post Criando Processo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Processo processo)
        {
            //var objOp = await _ordemProdutoService.FindByIdAsync(processo.OrdemProdutoId);

            //if (objOp == null)
            //{

            //    //Retornar dizendo que a OP Não existe;
            //    return NotFound();
            //}
            //processo.OrdemProduto = objOp;

           await _processoService.InsertAsync(processo);
           return RedirectToAction(nameof(Index));
         
           
             //return View(processo);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var obj = await _processoService.FindByIdAsync(id);

            return View(obj);
        }



    }
}
