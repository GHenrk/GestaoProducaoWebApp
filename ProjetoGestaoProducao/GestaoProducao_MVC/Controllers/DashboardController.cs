using GestaoProducao_MVC.Filters;
using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestaoProducao_MVC.Controllers
{
    [UserLogado]
    public class DashboardController : Controller
    {

        private readonly MaquinaService _maquinaService;

        public DashboardController(MaquinaService maquinaService)
        {
            _maquinaService = maquinaService;
        }

        public IActionResult Index()
        {
            return View();

        }



        public async Task<JsonResult> ListaMaquinas()
        {
            var listMaquinas = await _maquinaService.FindAllAsync();

            List<object> listJson = new List<object>();

            foreach (var item in listMaquinas)
            {

                var itemConvertido = new
                {
                    Id = item.Id,
                    Nome = item.Nome

                    
                };
                
                listJson.Add(itemConvertido);
            }


            return Json(listJson);
        }
 


    }
}
