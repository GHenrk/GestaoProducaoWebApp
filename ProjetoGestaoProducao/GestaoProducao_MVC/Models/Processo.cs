using System.ComponentModel.DataAnnotations;

namespace GestaoProducao_MVC.Models
{
    public class Processo
    {

        public int Id { get; set; }

        [Required]
        public string CodigoPeca { get; set; }


        public string Descricao { get; set; }


        [Required]
        public int QuantidadePeca { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public OrdemProduto OrdemProduto { get; set; }


        public ICollection<Apontamento> Apontamentos { get; set; } = new List<Apontamento>();

        public Processo()
        {

        }

        public Processo(int id, string codigoPeca, string descricao, int quantidadePeca, DateTime dataCriacao, OrdemProduto oProduto)
        {
            Id = id;
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
