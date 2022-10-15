using GestaoProducao_MVC.Filters;
using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Models.Enums;
using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestaoProducao_MVC.Controllers
{
    [UserLogado]
    public class DashboardController : Controller
    {

        private readonly MaquinaService _maquinaService;
        private readonly ApontamentoService _apontamentoService;
        private readonly ProcessoService _processoService;
        private readonly RegistroParadaService _registroParadaService;

        public DashboardController(MaquinaService maquinaService, ApontamentoService apontamentoService, ProcessoService processoService, RegistroParadaService registroParadaService)
        {
            _maquinaService = maquinaService;
            _apontamentoService = apontamentoService;
            _processoService = processoService;
            _registroParadaService = registroParadaService;
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
                bool maquinaAtiva = await _apontamentoService.isMaquinaAtiva(item.Id);

                Apontamento apontamentoMaquina = new Apontamento();

                Processo processo = new Processo();

                List<RegistroParada>? registroParadas = new List<RegistroParada>();

                if (maquinaAtiva)
                {
                    apontamentoMaquina = await _apontamentoService.FindApontamentoMachineAsync(item.Id);
                    processo = await _processoService.FindByIdAsync(apontamentoMaquina.ProcessoId);
                    registroParadas = await _registroParadaService.FindByApontamentoAsync(apontamentoMaquina);
                    
                }
                else
                {
                   
                   apontamentoMaquina.Status = AptStatus.Ocioso;
                    apontamentoMaquina.ProcessoId = 0;
                    processo.OrdemProdutoId = 0;
                    registroParadas = null;
                    apontamentoMaquina.TempoDecorridoSpan = TimeSpan.Zero;
                    
                   
                }

                var itemConvertido = new
                {
                    Id = apontamentoMaquina.Id,
                    Nome = item.Nome,
                    MaquinaAtiva = maquinaAtiva,
                    maquinaId = item.Id,
                    Status = apontamentoMaquina.Status.ToString(),
                    AptMaquina = processo.Id,
                    Op = processo.OrdemProdutoId,
                    totalHoras = (int)apontamentoMaquina.TempoDecorridoSpan.TotalHours,
                    totalMinutos = (int)apontamentoMaquina.TempoDecorridoSpan.Minutes,
                    totalSegundos = (int)apontamentoMaquina.TempoDecorridoSpan.Seconds,
                    qntdParadas = registroParadas == null ? "0" : registroParadas.Count().ToString()

                };

                listJson.Add(itemConvertido);
            }


            return Json(listJson);
        }
 


    }
}
