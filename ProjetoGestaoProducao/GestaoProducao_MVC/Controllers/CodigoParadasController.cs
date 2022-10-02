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


    }
}
