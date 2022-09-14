namespace GestaoProducao_MVC.Models
{
    public class Maquina
    {
        public int Id { get; set; }
    
        public string Nome { get; set; }

    //Referencia
        public ICollection<Apontamento> Apontamentos { get; set; }


        public Maquina()
        {

        }
        public Maquina(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public void AddApontamento(Apontamento apontamento)
        {
            Apontamentos.Add(apontamento);  
        }

        public void RemoveApontamento(Apontamento apontamento)
        {
            Apontamentos.Remove(apontamento);
        }


        
    }

   
}
