using GestaoProducao_MVC.Models;
using GestaoProducao_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoProducao_MVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController (UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _usuarioService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, Login, Email, Perfil, Senha")] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            try {
                await _usuarioService.InsertAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(usuario);
            }
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Usuario usuario = await _usuarioService.FindByIdAsync(id.Value);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            try
            {
                await _usuarioService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest();
            }
        }


        // GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Usuario usuario = await _usuarioService.FindByIdAsync(id.Value);

            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            try
            {
                await _usuarioService.UpdateAsync(usuario);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View(usuario);
            }
        }





        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
        
            Usuario usuario = await _usuarioService.FindByIdAsync(id.Value);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

    }
}
