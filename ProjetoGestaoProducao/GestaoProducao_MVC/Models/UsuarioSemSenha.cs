
using GestaoProducao_MVC.Models.Enums;
using System.ComponentModel.DataAnnotations;


namespace GestaoProducao_MVC.Models
{
    public class UsuarioSemSenha
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o nome do Usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Insira o Login do Usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Insira o Email do Usuário")]
        [EmailAddress(ErrorMessage = "Você precisa inserir um e-mail válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Selecione um perfil para o usuario")]
        public PerfilUsuario Perfil { get; set; }


        public UsuarioSemSenha()
        {

        }

        public UsuarioSemSenha(int id, string nome, string email, PerfilUsuario perfil)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Perfil = perfil;
        }
    }
}
