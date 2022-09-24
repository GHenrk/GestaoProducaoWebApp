using System.ComponentModel.DataAnnotations;

namespace GestaoProducao_MVC.Models
{
    public class Processo
    {
        [Display(Name = "Código APT")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Código Peça")]
        public string CodigoPeca { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }


        [Required]
        [Display(Name = "Quantidade")]
        public int QuantidadePeca { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data Criação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        [Display(Name = "OP")]
        public OrdemProduto OrdemProduto { get; set; }

        public int OrdemProdutoId { get; set; }


        public ICollection<Apontamento> Apontamentos { get; set; } = new List<Apontamento>();

        public Processo()
        {

        }
       
        public Processo(string codigoPeca, string descricao, int quantidadePeca, DateTime dataCriacao, OrdemProduto oProduto)
        {
          
            CodigoPeca = codigoPeca;
            Descricao = descricao;
            QuantidadePeca = quantidadePeca;
            DataCriacao = dataCriacao;
            OrdemProduto = oProduto;
 
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
