using GestaoProducao_MVC.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace GestaoProducao_MVC.Models
{
    public class RegistroParada
    {
        

        public int Id { get; set; }

        [Display(Name = "Data Inicial")]

        public DateTime DataInicial { get; set; }

        [Display(Name = "Data Final")]

        public DateTime? DataFinal { get; set; }

        [Display(Name = "Tempo total")]

        public long? TempoTotal { get; set; }

        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        
        public bool ParadaAtiva { get; set; }
        

        public AptStatus Status { get; set; }
        
        public CodigoParada? CodigoParada { get; set; }

        [Display(Name = "Código de Parada")]

        public int CodigoParadaId { get; set; }

        public Apontamento? Apontamento { get; set; }
        public int ApontamentoId { get; set; }

        [NotMapped]
        public TimeSpan? TempoDeParada { get; set; }

        [NotMapped]
        [Display(Name = "Tempo total de parada")]

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