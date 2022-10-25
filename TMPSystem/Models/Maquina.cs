using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GestaoProducao_MVC.Models
{
    public class Maquina
    {
        [Display(Name = "Código máquina")]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nome é obrigatório!")]
        [Display(Name = "Máquina")]
        [JsonProperty(PropertyName = "Nome")]
        public string Nome { get; set; }

        //Referencia
        [JsonIgnore]
        public ICollection<Apontamento>? Apontamentos { get; set; }


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
