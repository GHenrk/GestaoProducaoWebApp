using System.ComponentModel.DataAnnotations;

namespace GestaoProducao_MVC.Models
{
    public class RedefinirSenhaModel
    {
       
            [Required(ErrorMessage = "Insira o Login")]
            public string Login { get; set; }

            [Required(ErrorMessage = "Insira o Email")]
            public string Email { get; set; }

    }
}
