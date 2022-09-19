using System.Reflection.Metadata.Ecma335;

namespace GestaoProducao_MVC.Models
{
    public class CodigoParada
    {
        public int Id { get; set; }

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
