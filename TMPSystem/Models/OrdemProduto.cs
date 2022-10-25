using GestaoProducao_MVC.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoProducao_MVC.Models
{
    public class OrdemProduto
    {
        [Display(Name = "Ordem Produção")]
        public int Id { get; set; }

        [Display(Name = "Código produto")]
        public string CodigoProduto { get; set; }

        [Display(Name = "Qntd.")]
        public int QuantidadeProduto { get; set; }



        [DataType(DataType.Date)]
        [Display(Name = "Data de Venda")]
        public DateTime DataVenda { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Entrega")]
        public DateTime DataEntrega { get; set; }

        [Display(Name = "Status")]
        public OpStatus OpStatus { get; set; }

        //Fazer relações;

        public ICollection<Processo>? Processos { get; set; } = new List<Processo>();


        //Propriedades nao mapeadas para conversao de dados e informacos no sistema;

        [NotMapped]
        public TimeSpan TempoTotalEstimadoSpan { get; set; }


        [NotMapped]
        public TimeSpan TempoTotalDecorridoSpan { get; set; }


        [NotMapped]
        public TimeSpan TempoTotalParadasSpan { get; set; }

        [NotMapped]
        [Display(Name = "Tempo total estimado")]
        public string TempoEstimadoFormatado { get; set; }

        [NotMapped]
        [Display(Name = "Tempo total decorrido")]
        public string TempoTotalDecorridoFormatado { get; set; }

        [NotMapped]
        [Display(Name = "Tempo total de paradas")]
        public string TempoTotalParadasFormatado { get; set; }


        [NotMapped]
        [Display(Name = "Tempo útil")]
        public string TempoTotalUtilFormatado { get; set; }


        [NotMapped]
        [Display(Name = "Tempo aproximado por item")]
        public string TempoTotalAproxFormatado { get; set; }





        //Método construtor vazio
        public OrdemProduto()
        {

        }


        //Método construtor com parametros;
        public OrdemProduto(string codigoProduto, int quantidadeProduto, DateTime dtVenda, DateTime dtEntrega, OpStatus status)
        {
            CodigoProduto = codigoProduto;
            QuantidadeProduto = quantidadeProduto;
            DataVenda = dtVenda;
            DataEntrega = dtEntrega;
            OpStatus = status;

        }


       public void AddProcesso (Processo processo)
        {
            Processos.Add(processo);
        }

        public void RemoveProcesso(Processo processo)
        {
            Processos.Remove(processo);
        }


        public string FormataTempo(TimeSpan time)
        {
            string timeFormatado = (int)time.TotalHours + time.ToString("\\:mm\\:ss");

            return timeFormatado;
        }

        public TimeSpan TempoTotalEstimado()
        {
            TimeSpan tempoTotalEstimado = TimeSpan.Zero;
            if (Processos != null)
            {
                foreach (var processo in Processos)
                {
                    tempoTotalEstimado += processo.TempoEstimadoSpan;
                }
            }

           return tempoTotalEstimado;
        }


        public TimeSpan TempoTotalDecorrido()
        {
            TimeSpan tempoTotalDecorrido = TimeSpan.Zero;
            if (Processos != null)
            {
                foreach (var processo in Processos)
                {
                    tempoTotalDecorrido += processo.TempoDecorridoApontamentos;
                }
            }

            return tempoTotalDecorrido;
        }


        public TimeSpan TempoTotalParadas()
        {
            TimeSpan tempoTotalParadas = TimeSpan.Zero;
            if (Processos != null)
            {
                foreach (var processo in Processos)
                {
                    tempoTotalParadas += processo.TotalTempoParadasProcesso;
                }
            }

            return tempoTotalParadas;
        }


        public TimeSpan TempoTotalUtil()
        {
            TimeSpan tempoUtil = TempoTotalDecorridoSpan - TempoTotalParadas();
            return tempoUtil;
        
        }

        public TimeSpan TempoAproxPorItem()
        {
            TimeSpan tempoAprox = TempoTotalUtil() / QuantidadeProduto;

            return tempoAprox;
        }



    }
}
