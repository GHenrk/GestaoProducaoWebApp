
using System.ComponentModel.DataAnnotations;

namespace GestaoProducao_MVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Insira o Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Insira a Senha")]
        public string Senha { get; set; }


    }
}
