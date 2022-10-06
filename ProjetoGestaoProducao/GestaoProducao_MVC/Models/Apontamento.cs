using GestaoProducao_MVC.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace GestaoProducao_MVC.Models
{
    public class Apontamento
    {
        public int Id { get; set; }


        [Display(Name = "Data Inicial")]
        public DateTime DataInicial { get; set; }


        [Display(Name = "Data Final")]
        public DateTime? DataFinal { get; set; }

        [Display(Name = "Tempo Total:")]
        public long? TempoTotal { get; set; }

        public string?  Descricao { get; set; }

        public bool IsAtivo { get; set; }
    
        public AptStatus Status { get; set; }

        public Operacao? Operacao { get; set; }

        public Processo? Processo { get; set; }

        public int ProcessoId { get; set; }

        public Maquina? Maquina { get; set; }

        public int MaquinaId { get; set; }
        public Funcionario? Funcionario { get; set; }

        public int FuncionarioId { get; set; }

        [NotMapped]
        public string? TotalTime { get; set; }

        [NotMapped]
        [Display(Name = "Tempo De Paradas:")]
        public string? TempoDeParadasFormatado { get; set; }

        [NotMapped]
        public string? TempoUtilFormatado { get; set; }

       public ICollection<RegistroParada>? RegistroParadas { get; set; }

        public Apontamento()
        {

        }

        public Apontamento(DateTime dataInicial, DateTime? dataFinal, long? tempoTotal, string descricao, AptStatus status,Operacao operacao, Processo processo, Maquina maquina, Funcionario funcionario, bool isAtivo)
        {
       
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            TempoTotal = tempoTotal;
            Descricao = descricao;
            Status = status;
            Operacao = operacao;
            Processo = processo;
            Maquina = maquina;
            Funcionario = funcionario;
            IsAtivo = isAtivo;
        }

        
        public void AddParada(RegistroParada parada)
        {
            RegistroParadas.Add(parada);

        }

        public void RemoveParada(RegistroParada parada)
        {
            RegistroParadas.Remove(parada);
        }
    }

}