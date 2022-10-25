using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoProducao_MVC.Models
{
    public class Processo
    {
        [Display(Name = "Código apontamento")]
        public int Id { get; set; }


        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "Código Peça")]
        public string CodigoPeca { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        [Display(Name = "Quantidade")]
        public int QuantidadePeca { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data Criação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCriacao { get; set; }

        [Display(Name = "Ordem Produção")]
        public OrdemProduto? OrdemProduto { get; set; }

        [Display(Name = "Número da OP")]
        public int OrdemProdutoId { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Tempo estimado")]
        public long? TempoEstimado { get; set; }

        [NotMapped]
        public TimeSpan TempoEstimadoSpan { get; set; }

        [NotMapped]
        [Display(Name = "Tempo estimado")]
        public string? TempoEstimadoFormatado { get; set; }

        [NotMapped]
        public TimeSpan TempoDecorridoApontamentos { get; set; }

        [NotMapped]
        [Display(Name = "Tempo total decorrido")]
        public string? TotalTempoDecorridoFormatado { get; set; }

        [NotMapped]
        public TimeSpan TotalTempoParadasProcesso { get; set; }

        [NotMapped]
        [Display(Name = "Tempo total de Paradas")]
        public string? TotalTempoParadasFormatado { get; set; }

        [NotMapped]
        [Display(Name = "Tempo total útil")]
        public string? TotalTempoUtilFormatado { get; set; }

        [NotMapped]
        [Display(Name = "Tempo aproximado por item")]
        public string? TempoAproximadoItem { get; set; }




        public ICollection<Apontamento> Apontamentos { get; set; } = new List<Apontamento>();

        public Processo()
        {

        }


        public Processo(string codigoPeca, string descricao, int quantidadePeca, DateTime dataCriacao, OrdemProduto oProduto, long? tempoEstimado)
        {

            CodigoPeca = codigoPeca;
            Descricao = descricao;
            QuantidadePeca = quantidadePeca;
            DataCriacao = dataCriacao;
            OrdemProduto = oProduto;

            TempoEstimado = tempoEstimado.Value;

        }

        public void AddApontamento(Apontamento apontamento)
        {
            Apontamentos.Add(apontamento);
        }

        public void RemoveApontamento(Apontamento apontamento)
        {
            Apontamentos.Remove(apontamento);
        }



        //TempoTotalDoProcesso;

        //public TimeSpan TempoTotalParadasProcesso()
        //{
        //    TimeSpan tempoTotalParadasProcesso = TimeSpan.Zero;
        //    if (Apontamentos.Any())
        //    {
        //        foreach (var apontamento in Apontamentos)
        //        {
        //            tempoTotalParadasProcesso += apontamento.TempoTotalParadasSpan;
        //        }


        //        return tempoTotalParadasProcesso;
        //    }


        //    return TimeSpan.Zero;

        //}


        public TimeSpan TempoTotalUtilProcesso()
        {
            TimeSpan tempoUtil = TempoDecorridoApontamentos - TotalTempoParadasProcesso;

            return tempoUtil;
        }


        public TimeSpan TempoAproxPorItem()
        {
            TimeSpan tempoAprox = TempoTotalUtilProcesso() / QuantidadePeca;
            return tempoAprox;
        }
    }


}

