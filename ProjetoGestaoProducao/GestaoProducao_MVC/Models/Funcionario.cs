namespace GestaoProducao_MVC.Models
{
    public class Funcionario
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Cargo { get; set; }
    
        public ICollection<Apontamento> Apontamentos { get; set; }


        public Funcionario()
        {

        }

        public Funcionario(string name, string cargo)
        {
           
            Name = name;
            Cargo = cargo;
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
