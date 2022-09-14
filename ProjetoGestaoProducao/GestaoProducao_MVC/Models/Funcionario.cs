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

        public Funcionario(int id, string name, string cargo)
        {
            Id = id;
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
