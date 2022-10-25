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
using GestaoProducao_MVC.Filters;

namespace GestaoProducao_MVC.Controllers
{
    [UserLogado]
    public class FuncionariosController : Controller
    {
        private readonly FuncionarioService _funcionarioService;

        public FuncionariosController(FuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }


        // GET: Funcionarios
        public async Task<IActionResult> Index(string searchString)
        {
            var list = await _funcionarioService.FindByNameCodeAsync(searchString);
            return View(list);
        }

        // GET
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null )
            {
                return BadRequest();
            }

            var funcionario = await _funcionarioService.FindByIdAsync(id.Value);

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }


        [UserMaster]
        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Funcionario funcionario)
        {
            try
            {
                await _funcionarioService.InsertAsync(funcionario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(funcionario);
            }

        }



        // GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var funcionario = await _funcionarioService.FindByIdAsync(id.Value);
            
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        [UserMaster]
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return BadRequest();
            }

            try
            {
                await _funcionarioService.UpdateAsync(funcionario);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View(funcionario);
            }
        }


        [UserMaster]
        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var funcionario = await _funcionarioService.FindByIdAsync(id.Value);

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _funcionarioService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest();
            }
        }        
    }
}
