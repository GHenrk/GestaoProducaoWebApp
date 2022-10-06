using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProducao_MVC.Controllers
{
    public class RegistroParadasController : Controller
    {
        private readonly RegistroParadaService _registroParadaService;
        private readonly ApontamentoService _apontamentoService;

        public RegistroParadasController(RegistroParadaService service, ApontamentoService apontamentoService)
        {
            _registroParadaService = service;
            _apontamentoService = apontamentoService;
        }



        public async Task<IActionResult> Index()
        {
            var list = await _registroParadaService.FindAllAsync();

            return View(list);
        }


        public IActionResult IniciarParada()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IniciarParada(int funcionarioId, [Bind("CodigoParadaId")] RegistroParada registroParada)
        {
            bool funcionarioAtivo = await _apontamentoService.isFuncionarioAtivo(funcionarioId);


            if (!funcionarioAtivo)
            {
                //Voce precisa ter um apontamentoAtivo para parada;
                return View();
            }

            Apontamento apontamento = await _apontamentoService.FindByIdStatus(funcionarioId);

            if (apontamento == null)
            {
                //Algo deu errado;;;
                return NotFound();
            }

            if (apontamento.Status == Models.Enums.AptStatus.Parado)
            {
                return View();
            }

            apontamento.Status = Models.Enums.AptStatus.Parado;
            registroParada.DataInicial = DateTime.Now;
            registroParada.ParadaAtiva = true;
            registroParada.Status = Models.Enums.AptStatus.Ativo;
            registroParada.Apontamento = apontamento;

            if (ModelState.IsValid)
            {
                await _registroParadaService.InsertAsync(registroParada);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        public IActionResult EncerrarParada()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EncerrarParada(int funcionarioId, string? descricao)
        {
            bool paradaAtiva = await _registroParadaService.IsFuncionarioEmParada(funcionarioId);
            if (!paradaAtiva)
            {
                //Não foi encontrado parada ativa.
                return View();
            }


            RegistroParada registroParada = await _registroParadaService.FindByFuncAtivoAsync(funcionarioId);

            if (registroParada == null)
            {

                //algo deu errrado na requisuição;
                return BadRequest();
            }

            if (!String.IsNullOrEmpty(descricao))
            {
                registroParada.Descricao = descricao;
            }

            registroParada.DataFinal = DateTime.Now;
            registroParada.ParadaAtiva = false;
            registroParada.Status = Models.Enums.AptStatus.Encerrado;
            registroParada.TempoDeParada = registroParada.DataFinal - registroParada.DataInicial;
            registroParada.TempoTotal = registroParada.TempoDeParada.Value.Ticks;


            var apontamento = await _apontamentoService.FindByIdStatus(funcionarioId);
            if (apontamento == null)
            {
                return BadRequest();
            }

            apontamento.Status = Models.Enums.AptStatus.Ativo;

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await _registroParadaService.UpdateAsync(registroParada);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
           
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }

            var obj = await _registroParadaService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return NotFound();

            }

            if (obj.ParadaAtiva)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("Id,DataInicial,DataFinal,Descricao,CodigoParadaId,Status,ParadaAtiva,ApontamentoId")] RegistroParada registroParada)
        {
            if (id != registroParada.Id)
            {
                return BadRequest();
            }

            registroParada.TempoDeParada = registroParada.DataFinal - registroParada.DataInicial;
            registroParada.TempoTotal = registroParada.TempoDeParada.Value.Ticks;
            
            if (!ModelState.IsValid)
            {

                return View(registroParada);
            }

            try
            {
                await _registroParadaService.UpdateAsync(registroParada);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(registroParada);
            }

        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var obj = await _registroParadaService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var obj = await _registroParadaService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            if (obj.ParadaAtiva)
            {
                TempData["Exclusao"] = "Você não pode remover uma parada ativa";
                return RedirectToAction(nameof(Index));
            }


            return View(obj);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _registroParadaService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction();
            }
        }


    }
}
