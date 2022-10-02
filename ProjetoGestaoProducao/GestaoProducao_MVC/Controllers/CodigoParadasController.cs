using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProducao_MVC.Controllers
{
    public class CodigoParadasController : Controller

    {

        private readonly CodigoParadaService _codigoParadaService;

        public CodigoParadasController(CodigoParadaService codigoParadaService)
        {
            _codigoParadaService = codigoParadaService;
        }


        public async Task<IActionResult> Index(string searchString)
        {
            var list = await _codigoParadaService.FindByNameCodeAsync(searchString);

            return View(list);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var codigoParada = await _codigoParadaService.FindByIdAsync(id.Value);

            if (codigoParada == null)
            {
                return NotFound();
            }

            return View(codigoParada);
        }


        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CodigoParada codigoParada)
        {
            try
            {
                await _codigoParadaService.InsertAsync(codigoParada);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(codigoParada);
            }

        }

    }
}
