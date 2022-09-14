using GestaoProducao_MVC.Models.Enums;
using System.Numerics;

namespace GestaoProducao_MVC.Models
{
    public class Apontamento
    {
        public int Id { get; set; }

        public DateTime DataInicial { get; set; } = DateTime.Now;

        public DateTime? DataFinal { get; set; }

        public TimeSpan? TempoTotal { get; set; }

        public string  Descricao { get; set; }
    
        public AptStatus Status { get; set; }

        public ICollection<RegistroParada> RegistrosParada { get; set; }

        public Apontamento()
        {

        }


        public Apontamento(int id, DateTime dataInicial, DateTime? dataFinal, TimeSpan? tempoTotal, string descricao, AptStatus status)
        {
            Id = id;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            TempoTotal = tempoTotal;
            Descricao = descricao;
            Status = status;
        }

        
        public void AddParada(RegistroParada parada)
        {
            RegistrosParada.Add(parada);

        }

        public void RemoveParada(RegistroParada parada)
        {
            RegistrosParada.Remove(parada);
        }
    }

}