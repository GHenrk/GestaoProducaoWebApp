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

namespace GestaoProducao_MVC.Controllers
{
    public class OrdemProdutosController : Controller
    {
        private readonly OrdemProdutoService _ordemProdutoService;

        public OrdemProdutosController(OrdemProdutoService ordemProdutoService)
        {
            _ordemProdutoService = ordemProdutoService;
        }



       public async Task<IActionResult> Index()
        {
            var list = await _ordemProdutoService.FindAllAsync();

            return View(list);
        }





        // GET: OrdemProdutoes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.OrdemProduto == null)
        //    {
        //        return NotFound();
        //    }

        //    var ordemProduto = await _context.OrdemProduto
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (ordemProduto == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ordemProduto);
        //}


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

        //Ate aqui

        // POST: OrdemProdutoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoProduto,QuantidadeProduto,DataVenda,DataEntrega")] OrdemProduto ordemProduto)
        //{
        //    if (id != ordemProduto.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(ordemProduto);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrdemProdutoExists(ordemProduto.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(ordemProduto);
        //}

        // GET: OrdemProdutoes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.OrdemProduto == null)
        //    {
        //        return NotFound();
        //    }

        //    var ordemProduto = await _context.OrdemProduto
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (ordemProduto == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ordemProduto);
        //}

        // POST: OrdemProdutoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.OrdemProduto == null)
        //    {
        //        return Problem("Entity set 'GestaoProducao_MVCContext.OrdemProduto'  is null.");
        //    }
        //    var ordemProduto = await _context.OrdemProduto.FindAsync(id);
        //    if (ordemProduto != null)
        //    {
        //        _context.OrdemProduto.Remove(ordemProduto);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrdemProdutoExists(int id)
        //{
        //  return (_context.OrdemProduto?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
