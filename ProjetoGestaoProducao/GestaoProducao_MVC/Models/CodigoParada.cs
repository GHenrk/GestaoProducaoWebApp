using System.Reflection.Metadata.Ecma335;

namespace GestaoProducao_MVC.Models
{
    public class CodigoParada
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public ICollection<RegistroParada> ParadasRegistradas { get; set; } = new List<RegistroParada>();

        public CodigoParada()
        {

        }

        public CodigoParada(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
           
        }

        public void AddRegistroParada (RegistroParada parada)
        {
            ParadasRegistradas.Add(parada);
        }

        public void RemoveRegistroParada(RegistroParada parada)
        {
            ParadasRegistradas.Remove(parada);
        }
    }
}
