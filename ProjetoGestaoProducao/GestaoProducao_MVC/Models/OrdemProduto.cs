using System.ComponentModel.DataAnnotations;

namespace GestaoProducao_MVC.Models
{
    public class OrdemProduto
    {
        [Display(Name = "OP")]
        public int Id { get; set; }

        [Display(Name = "Código Produto")]
        public string CodigoProduto { get; set; }

        [Display(Name = "Qntd.")]
        public int QuantidadeProduto { get; set; }

        [Display(Name = "Data de Venda")]
        public DateTime DataVenda { get; set; }

        [Display(Name = "Data de Entrega")]
        public DateTime DataEntrega { get; set; }

        //Fazer relações;

        public ICollection<Processo> Processos { get; set; } = new List<Processo>();



        //Método construtor vazio
        public OrdemProduto()
        {

        }


        //Método construtor com parametros;
        public OrdemProduto(string codigoProduto, int quantidadeProduto, DateTime dtVenda, DateTime dtEntrega)
        {
            CodigoProduto = codigoProduto;
            QuantidadeProduto = quantidadeProduto;
            DataVenda = dtVenda;
            DataEntrega = dtEntrega;

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
