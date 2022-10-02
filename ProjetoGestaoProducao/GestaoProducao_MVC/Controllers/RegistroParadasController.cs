using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProducao_MVC.Controllers
{
    public class RegistroParadasController : Controller
    {
        private readonly RegistroParadaService _registroParadaService;

        public RegistroParadasController (RegistroParadaService service)
        {
            _registroParadaService = service;
        }



        public async Task<IActionResult> Index()
        {
            var list = await _registroParadaService.FindAllAsync();

            return View(list);
        }
    }
}
