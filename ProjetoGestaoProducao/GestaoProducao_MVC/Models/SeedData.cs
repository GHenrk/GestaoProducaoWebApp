using GestaoProducao_MVC.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace GestaoProducao_MVC.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GestaoProducao_MVCContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<GestaoProducao_MVCContext>>()))
            {
                if (context.Apontamento.Any() || context.OrdemProduto.Any() || context.Processo.Any() || context.RegistroParada.Any() ||  context.CodigoParada.Any() || context.Funcionario.Any() || context.Maquina.Any())
                {
                    return;   // DB has been seeded
                }

                OrdemProduto Op1 = new OrdemProduto("CODIGO010203", 8, new DateTime(2022, 08, 05), new DateTime(2022, 08, 06), Enums.OpStatus.Vendido);
                OrdemProduto Op2 = new OrdemProduto("CODIGO020203", 8, new DateTime(2022, 06, 04 ), new DateTime(2022, 9, 4), Enums.OpStatus.Entregue);
                OrdemProduto Op3 = new OrdemProduto("CODIGO010203", 8, new DateTime(2022, 08, 05), new DateTime(2022, 11, 5), Enums.OpStatus.Fabricação);

                context.OrdemProduto.AddRange(Op1, Op2, Op3);
                
                Processo Pro1 = new Processo("ACHMI30JJ", "Cabeçote Dianteiro", 8, new DateTime(2022, 09, 12), Op1);
                Processo Pro2 = new Processo("ACHMI30JJ", "Cabeçote Traseiro", 8, new DateTime(2022, 09, 12), Op1);
                Processo Pro3 = new Processo("ACHMI30JJ", "Munhão", 8, new DateTime(2022, 09, 12), Op1);
                Processo Pro4 = new Processo("ACHMI30JJ", "Haste", 8, new DateTime(2022, 09, 12), Op1);

                Processo Pro5 = new Processo("ACHMI60JJ", "Cabeçote Dianteiro", 6, new DateTime(2022, 09, 12), Op2);
                Processo Pro6 = new Processo("ACHMI60JJ", "Cabeçote Traseiro", 6, new DateTime(2022, 09, 12), Op2);
                Processo Pro7 = new Processo("ACHMI60JJ", "Camisa", 6, new DateTime(2022, 09, 12), Op2);
                Processo Pro8 = new Processo("ACHMI60JJ", "Haste", 6, new DateTime(2022, 09, 12), Op2);


                Processo Pro9 = new Processo("Produto4Exemplo", "Cabeçote Dianteiro", 4, new DateTime(2022, 09, 12), Op3);
                Processo Pro10 = new Processo("Produto4Exemplo", "Cabeçote Traseiro", 4, new DateTime(2022, 09, 12), Op3);
                Processo Pro11 = new Processo("Produto4Exemplo", "Haste", 4, new DateTime(2022, 09, 12), Op3);


                context.Processo.AddRange(Pro1, Pro2, Pro3, Pro4, Pro5, Pro6, Pro7, Pro8, Pro9, Pro10, Pro11);

                Funcionario Func1 = new Funcionario("Gustavo Henrique", "Desenvolvedor de Softwares");
                Funcionario Func2 = new Funcionario("Funcionaro Exemplo", "Programador CNC");
                Funcionario Func3 = new Funcionario("João da silva", "Back-End developer");
                Funcionario Func4 = new Funcionario("Exemplo 2", "Front-end developer");
                Funcionario Func5 = new Funcionario("Charlie brown", "Teste de Cargo");

                context.Funcionario.AddRange(Func1, Func2, Func3, Func4, Func5);

                Maquina maq1 = new Maquina("Hartford ABR-1000");
                Maquina maq2 = new Maquina("Feeler FV-1300");
                Maquina maq3 = new Maquina("PanMachine VI-1000L");
                Maquina maq4 = new Maquina("Torno CNC");

                context.Maquina.AddRange(maq1, maq2, maq3, maq4);

                CodigoParada codParada1 = new CodigoParada("Parada por falta de ferramenta");
                CodigoParada codParada2 = new CodigoParada("Parada por felta de incerto");
                CodigoParada codParada3 = new CodigoParada("Parada por falta material");

                context.CodigoParada.AddRange(codParada1, codParada2, codParada3);

                Apontamento apt1 = new Apontamento(new DateTime(2022, 09, 14), new DateTime(2022, 09, 14), null, "Peça concluida", Enums.AptStatus.Encerrado,Enums.Operacao.Desbaste, Pro1, maq1, Func1);
                Apontamento apt2 = new Apontamento(new DateTime(2022, 09, 14), new DateTime(2022, 09, 14), null, "Peça concluida", Enums.AptStatus.Encerrado, Enums.Operacao.Pintura, Pro1, maq2, Func2);
                Apontamento apt3 = new Apontamento(new DateTime(2022, 09, 14), new DateTime(2022, 09, 14), null, "Peça concluida", Enums.AptStatus.Encerrado, Enums.Operacao.Acabamento, Pro2, maq3, Func3);
                Apontamento apt4 = new Apontamento(new DateTime(2022, 09, 14), new DateTime(2022, 09, 14), null, "Peça concluida", Enums.AptStatus.Encerrado, Enums.Operacao.Desbaste, Pro2, maq2, Func2);
                Apontamento apt5 = new Apontamento(new DateTime(2022, 09, 14), new DateTime(2022, 09, 14), null, "Houve uma falha no material", Enums.AptStatus.Encerrado, Enums.Operacao.Acabamento, Pro2, maq1, Func1);
                Apontamento apt6 = new Apontamento(new DateTime(2022, 09, 14), new DateTime(2022, 09, 14), null, "Peça concluida", Enums.AptStatus.Encerrado, Enums.Operacao.Desbaste, Pro4, maq4, Func3);

                context.Apontamento.AddRange(apt1, apt2, apt3, apt4, apt5, apt6);

                RegistroParada rg1 = new RegistroParada( new DateTime(2022, 09, 14), new DateTime(2022, 09, 14), null, null, Enums.AptStatus.Encerrado, codParada1, apt3);

                context.RegistroParada.Add(rg1);

                context.SaveChanges();
            
            }




        }

    }
}
