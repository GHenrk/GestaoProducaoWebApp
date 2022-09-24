using System.ComponentModel.DataAnnotations;

namespace GestaoProducao_MVC.Models
{
    public class Maquina
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nome é obrigatório!")]
        public string Nome { get; set; }

    //Referencia
        public ICollection<Apontamento> Apontamentos { get; set; }


        public Maquina()
        {

        }
        public Maquina(string nome)
        {
           
            Nome = nome;
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
