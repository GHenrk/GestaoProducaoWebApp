using GestaoProducao_MVC.Models.Enums;

namespace GestaoProducao_MVC.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }

        public string Email {get; set;}

        public PerfilUsuario Perfil { get; set;}

        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

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

    }
}
