using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace GestaoProducao_MVC.Models
{
    public class CodigoParada
    {
        [Display(Name = "Código para apontamento")]
        public int Id { get; set; }

        [Display(Name = "Motivo de parada")]
        public string Descricao { get; set; }

        public ICollection<RegistroParada> RegistroParadas { get; set; } = new List<RegistroParada>();

        public CodigoParada()
        {

        }

        public CodigoParada(string descricao)
        {
            
            Descricao = descricao;
           
        }

        public void AddRegistroParada (RegistroParada parada)
        {
            RegistroParadas.Add(parada);
        }

        public void RemoveRegistroParada(RegistroParada parada)
        {
            RegistroParadas.Remove(parada);
        }
    }
}
