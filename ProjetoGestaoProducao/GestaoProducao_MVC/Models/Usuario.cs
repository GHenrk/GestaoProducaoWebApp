
using GestaoProducao_MVC.Helper;
using GestaoProducao_MVC.Models.Enums;
using System.ComponentModel.DataAnnotations;


namespace GestaoProducao_MVC.Models
{
    public class Usuario
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

        [Required(ErrorMessage = "Insira a senha do Usuário")]
        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public Usuario()
        {

        }

        public Usuario(int id, string nome, string login, string email, PerfilUsuario perfil, string senha, DateTime dataCadastro, DateTime? dataAtualizacao)
        {
            Id = id;
            Nome = nome;
            Login = login;
            Email = email;
            Perfil = perfil;
            Senha = senha;
            DataCadastro = dataCadastro;
            DataAtualizacao = dataAtualizacao;
        }

        public bool SenhaIsValid(string senha)
        {


            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

    }
}
