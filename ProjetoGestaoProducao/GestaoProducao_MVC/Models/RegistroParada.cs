using GestaoProducao_MVC.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoProducao_MVC.Models
{
    public class RegistroParada
    {
        public int Id { get; set; }

        public DateTime DataInicial { get; set; }

        public DateTime? DataFinal { get; set; }

        public long? TempoTotal { get; set; }
        public string? Descricao { get; set; }

        public bool ParadaAtiva { get; set; }
        
        public AptStatus Status { get; set; }
        
        public CodigoParada? CodigoParada { get; set; }
        public int CodigoParadaId { get; set; }

        public Apontamento? Apontamento { get; set; }
        public int ApontamentoId { get; set; }

        [NotMapped]
        public TimeSpan? TempoDeParada { get; set; }

        [NotMapped]
        public string? TempoDeParadaFormatado { get; set; }



        public RegistroParada()
        {

        }

        public RegistroParada(DateTime dataInicial, DateTime? dataFinal, long? tempoTotal, string? descricao, bool paradaAtiva, AptStatus status, CodigoParada? codigoParada, Apontamento? apontamento)
        {
           
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            TempoTotal = tempoTotal;
            Descricao = descricao;
            ParadaAtiva = paradaAtiva;
            Status = status;
            CodigoParada = codigoParada;
            Apontamento = apontamento;
        }





    }
}