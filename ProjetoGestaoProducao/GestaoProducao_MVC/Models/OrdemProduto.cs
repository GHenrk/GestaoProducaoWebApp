using GestaoProducao_MVC.Models.Enums;
using System.ComponentModel.DataAnnotations;

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

        public ICollection<Processo> Processos { get; set; } = new List<Processo>();



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

    }
}
