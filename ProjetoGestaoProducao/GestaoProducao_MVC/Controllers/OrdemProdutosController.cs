using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;

namespace GestaoProducao_MVC.Controllers
{
    public class OrdemProdutosController : Controller
    {
        private readonly GestaoProducao_MVCContext _context;

        public OrdemProdutosController(GestaoProducao_MVCContext context)
        {
            _context = context;
        }

        // GET: OrdemProdutoesOrdemProdutoes
        public async Task<IActionResult> Index()
        {
              return _context.OrdemProduto != null ? 
                          View(await _context.OrdemProduto.ToListAsync()) :
                          Problem("Entity set 'GestaoProducao_MVCContext.OrdemProduto'  is null.");
        }

        // GET: OrdemProdutoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrdemProduto == null)
            {
                return NotFound();
            }

            var ordemProduto = await _context.OrdemProduto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordemProduto == null)
            {
                return NotFound();
            }

            return View(ordemProduto);
        }

        // GET: OrdemProdutoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrdemProdutoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodigoProduto,QuantidadeProduto,DataVenda,DataEntrega")] OrdemProduto ordemProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordemProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ordemProduto);
        }

        // GET: OrdemProdutoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrdemProduto == null)
            {
                return NotFound();
            }

            var ordemProduto = await _context.OrdemProduto.FindAsync(id);
            if (ordemProduto == null)
            {
                return NotFound();
            }
            return View(ordemProduto);
        }

        // POST: OrdemProdutoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoProduto,QuantidadeProduto,DataVenda,DataEntrega")] OrdemProduto ordemProduto)
        {
            if (id != ordemProduto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordemProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdemProdutoExists(ordemProduto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ordemProduto);
        }

        // GET: OrdemProdutoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrdemProduto == null)
            {
                return NotFound();
            }

            var ordemProduto = await _context.OrdemProduto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordemProduto == null)
            {
                return NotFound();
            }

            return View(ordemProduto);
        }

        // POST: OrdemProdutoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdemProduto == null)
            {
                return Problem("Entity set 'GestaoProducao_MVCContext.OrdemProduto'  is null.");
            }
            var ordemProduto = await _context.OrdemProduto.FindAsync(id);
            if (ordemProduto != null)
            {
                _context.OrdemProduto.Remove(ordemProduto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdemProdutoExists(int id)
        {
          return (_context.OrdemProduto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
