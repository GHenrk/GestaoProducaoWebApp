using GestaoProducao_MVC.Models.Enums;

namespace GestaoProducao_MVC.Models
{
    public class RegistroParada
    {
        public int Id { get; set; }

        public DateTime DataInicial { get; set; }

        public DateTime? DataFinal { get; set; }

        public TimeSpan? TempoTotal { get; set; }

        public string? Descricao { get; set; }

        public AptStatus Status { get; set; }

        public CodigoParada CodigoParada { get; set; }

        public Apontamento Apontamento { get; set; }

        public RegistroParada()
        {

        }

        public RegistroParada(DateTime dataInicial, DateTime? dataFinal, TimeSpan? tempoTotal, string? descricao, AptStatus status, CodigoParada codigoParada, Apontamento apontamento)
        {
            
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            TempoTotal = tempoTotal;
            Descricao = descricao;
            Status = status;
            CodigoParada = codigoParada;
            Apontamento = apontamento;

        }





    }
}