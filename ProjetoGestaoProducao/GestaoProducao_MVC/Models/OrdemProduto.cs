using System.ComponentModel.DataAnnotations;

namespace GestaoProducao_MVC.Models
{
    public class OrdemProduto
    {
        public int Id { get; set; }

        public string CodigoProduto { get; set; }

        public int QuantidadeProduto { get; set; }

       
        public DateTime DataVenda { get; set; }

        public DateTime DataEntrega { get; set; }

        //Fazer relações;

        public ICollection<Processo> Processos { get; set; } = new List<Processo>();



        //Método construtor vazio
        public OrdemProduto()
        {

        }


        //Método construtor com parametros;
        public OrdemProduto(int id, string codigoProduto, int quantidadeProduto, DateTime dtVenda, DateTime dtEntrega)
        {
            Id = id;
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
