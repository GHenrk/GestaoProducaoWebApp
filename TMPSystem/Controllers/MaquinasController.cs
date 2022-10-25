using GestaoProducao_MVC.Filters;
using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;

namespace GestaoProducao_MVC.Controllers
{
    [UserLogado]

    //Essa função só pode ser executada pelo setor Vendas e Adm Geral;
    public class MaquinasController: Controller
    {

        private readonly MaquinaService _maquinaService;

        public MaquinasController (MaquinaService maquinaService)
        {
            _maquinaService = maquinaService;
        }

        public async Task<IActionResult> Index(string searchString)
        {

            List<Maquina> list = await _maquinaService.FindByNameCodeAsync(searchString);
            
            return View(list);
        } 


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                //Algo deu errado
                return BadRequest();
            }

            var obj = await _maquinaService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                //Elemento n encontrado;
                return NotFound();
            }

            return View(obj);
        }


        [UserMaster]
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Maquina maquina)
        {
            //try 
            //{
            //    await _maquinaService.InsertAsync(maquina);
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View(maquina);
            //}

            if (ModelState.IsValid)
            {
                await _maquinaService.InsertAsync(maquina);
                return RedirectToAction(nameof(Index));
            }
            return View(maquina);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var obj = await _maquinaService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Maquina maquina)
        {
            if (id != maquina.Id)
            {
                return BadRequest();
            }

            try
            {
                await _maquinaService.UpdateAsync(maquina);
                return RedirectToAction(nameof(Index));

            }catch
            {
                return View(maquina);
            }


        }

        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var obj = await _maquinaService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            try
            {
                await _maquinaService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest();
            }
        }




    }
}
