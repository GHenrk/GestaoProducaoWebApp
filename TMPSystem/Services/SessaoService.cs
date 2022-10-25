using GestaoProducao_MVC.Models;
using Newtonsoft.Json;

namespace GestaoProducao_MVC.Services
{
    public class SessaoService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SessaoService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Usuario BuscarSessaoDoUsuario()
        {
            string sessaoUsuario = _contextAccessor.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);
        }

        public void CriarSessaoDoUsuario(Usuario usuario)
        {

            string valor = JsonConvert.SerializeObject(usuario);

            _contextAccessor.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }


        public void RemoverSessaoUsuario()
        {
            _contextAccessor.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }




        
    }
}
