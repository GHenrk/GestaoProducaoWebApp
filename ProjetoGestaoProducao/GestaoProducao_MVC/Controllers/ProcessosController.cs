using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Models.ViewModel;
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
        public async Task<IActionResult> Create([Bind("CodigoPeca, Descricao, QuantidadePeca, OrdemProdutoId, DataCriacao")] Processo processo)
        {
            //var objOp = await _ordemProdutoService.FindByIdAsync(processo.OrdemProdutoId);

            //if (objOp == null)
            //{

            //    //Retornar dizendo que a OP Não existe;
            //    return NotFound();
            //}
            //processo.OrdemProduto = objOp;
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
        public async Task<IActionResult> Edit(int id, Processo processo)
        {
          if (id != processo.Id)
            {
                return BadRequest();
            }

          try
            {
                await _processoService.UpdateAsync(processo);

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View(processo);
            }    
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

            var obj = await _processoService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                //Elemento não encontrado;
                return NotFound();
            }

            return View(obj);
        }





    }
}
