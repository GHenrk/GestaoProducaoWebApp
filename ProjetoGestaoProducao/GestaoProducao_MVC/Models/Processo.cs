using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoProducao_MVC.Models
{
    public class Processo
    {
        [Display(Name = "Código APT")]
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

        [Display(Name = "OP")]
        public OrdemProduto OrdemProduto { get; set; }

        [Display(Name = "Número da OP")]
        public int OrdemProdutoId { get; set; }

        [DataType(DataType.Time)]
        public long? TempoEstimado { get; set; }

        [NotMapped]
        public TimeSpan TempoEstimadoSpan { get; set; }

        [NotMapped]
        public string TempoEstimadoFormatado { get; set; }
        
        [NotMapped]
        public string TotalTempoDecorrido { get; set; }

      


        public ICollection<Apontamento> Apontamentos { get; set; } = new List<Apontamento>();

        public Processo()
        {

        }
       
        public Processo(string codigoPeca, string descricao, int quantidadePeca, DateTime dataCriacao, OrdemProduto oProduto, long? tempoEstimado )
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

    }
}
