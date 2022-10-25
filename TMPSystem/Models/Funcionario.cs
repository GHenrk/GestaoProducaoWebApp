using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GestaoProducao_MVC.Models
{
    public class Funcionario
    {

        [Display(Name = "Código funcionário")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Cargo")]
        public string Cargo { get; set; }
    
        public ICollection<Apontamento> Apontamentos { get; set; }


        public Funcionario()
        {

        }

        public Funcionario(string name, string cargo)
        {
           
            Name = name;
            Cargo = cargo;
        }

        public void AddApontamento(Apontamento apontamento)
        {
            Apontamentos.Add(apontamento);
        }

        public void RemoveApontamento(Apontamento apontamento)
        {
            Apontamentos.Remove(apontamento);

        }
    }
}
