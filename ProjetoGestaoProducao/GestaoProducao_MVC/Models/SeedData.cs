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

                OrdemProduto Op1 = new OrdemProduto(1, "CODIGO010203", 8, new DateTime(5, 08, 2022), new DateTime(5, 11, 2022));
                OrdemProduto Op2 = new OrdemProduto(2, "CODIGO020203", 8, new DateTime(4, 06, 2022), new DateTime(4, 9, 2022));
                OrdemProduto Op3 = new OrdemProduto(3, "CODIGO010203", 8, new DateTime(5, 08, 2022), new DateTime(5, 11, 2022));

                context.OrdemProduto.AddRange(Op1, Op2, Op3);
                
                Processo Pro1 = new Processo(1, "ACHMI30JJ", "Cabeçote Dianteiro", 8, new DateTime(12, 09, 2022), Op1);
                Processo Pro2 = new Processo(2, "ACHMI30JJ", "Cabeçote Traseiro", 8, new DateTime(12, 09, 2022), Op1);
                Processo Pro3 = new Processo(3, "ACHMI30JJ", "Munhão", 8, new DateTime(12, 09, 2022), Op1);
                Processo Pro4 = new Processo(4, "ACHMI30JJ", "Haste", 8, new DateTime(12, 09, 2022), Op1);

                Processo Pro5 = new Processo(5, "ACHMI60JJ", "Cabeçote Dianteiro", 6, new DateTime(12, 09, 2022), Op2);
                Processo Pro6 = new Processo(6, "ACHMI60JJ", "Cabeçote Traseiro", 6, new DateTime(12, 09, 2022), Op2);
                Processo Pro7 = new Processo(7, "ACHMI60JJ", "Camisa", 6, new DateTime(12, 09, 2022), Op2);
                Processo Pro8 = new Processo(8, "ACHMI60JJ", "Haste", 6, new DateTime(12, 09, 2022), Op2);


                Processo Pro9 = new Processo(9, "Produto4Exemplo", "Cabeçote Dianteiro", 4, new DateTime(12, 09, 2022), Op3);
                Processo Pro10 = new Processo(10, "Produto4Exemplo", "Cabeçote Traseiro", 4, new DateTime(12, 09, 2022), Op3);
                Processo Pro11 = new Processo(11, "Produto4Exemplo", "Haste", 4, new DateTime(12, 09, 2022), Op3);


                context.Processo.AddRange(Pro1, Pro2, Pro3, Pro4, Pro5, Pro6, Pro7, Pro8, Pro9, Pro10, Pro11);

                Funcionario Func1 = new Funcionario(1, "Gustavo Henrique", "Desenvolvedor de Softwares");
                Funcionario Func2 = new Funcionario(2, "Funcionaro Exemplo", "Programador CNC");
                Funcionario Func3 = new Funcionario(3, "João da silva", "Back-End developer");
                Funcionario Func4 = new Funcionario(4, "Exemplo 2", "Front-end developer");
                Funcionario Func5 = new Funcionario(5, "Charlie brown", "Teste de Cargo");

                context.Funcionario.AddRange(Func1, Func2, Func3, Func4, Func5);

                Maquina maq1 = new Maquina(1, "Hartford ABR-1000");
                Maquina maq2 = new Maquina(2, "Feeler FV-1300");
                Maquina maq3 = new Maquina(3, "PanMachine VI-1000L");
                Maquina maq4 = new Maquina(4, "Torno CNC");

                context.Maquina.AddRange(maq1, maq2, maq3, maq4);

                CodigoParada codParada1 = new CodigoParada(1, "Parada por falta de ferramenta");
                CodigoParada codParada2 = new CodigoParada(2, "Parada por felta de incerto");
                CodigoParada codParada3 = new CodigoParada(3, "Parada por falta material");

                context.CodigoParada.AddRange(codParada1, codParada2, codParada3);

                Apontamento apt1 = new Apontamento(1, new DateTime(14, 9, 2022), new DateTime(14, 9, 2022), null, "Peça concluida", Enums.AptStatus.Encerrado, Pro1, maq1, Func1);
                Apontamento apt2 = new Apontamento(2, new DateTime(14, 9, 2022), new DateTime(14, 9, 2022), null, "Peça concluida", Enums.AptStatus.Encerrado, Pro1, maq2, Func2);
                Apontamento apt3 = new Apontamento(3, new DateTime(12, 9, 2022), new DateTime(12, 9, 2022), null, "Peça concluida", Enums.AptStatus.Encerrado, Pro2, maq3, Func3);
                Apontamento apt4 = new Apontamento(4, new DateTime(14, 9, 2022), new DateTime(14, 9, 2022), null, "Peça concluida", Enums.AptStatus.Encerrado, Pro2, maq2, Func2);
                Apontamento apt5 = new Apontamento(5, new DateTime(13, 9, 2022), new DateTime(13, 9, 2022), null, "Houve uma falha no material", Enums.AptStatus.Encerrado, Pro2, maq1, Func1);
                Apontamento apt6 = new Apontamento(6, new DateTime(14, 9, 2022), new DateTime(14, 9, 2022), null, "Peça concluida", Enums.AptStatus.Encerrado, Pro4, maq4, Func3);

                context.Apontamento.AddRange(apt1, apt2, apt3, apt4, apt5, apt6);

                RegistroParada rg1 = new RegistroParada(1, new DateTime(12, 9, 2022), new DateTime(12, 9, 2022), null, null, Enums.AptStatus.Encerrado, codParada1, apt3);

                context.RegistroParada.Add(rg1);

                context.SaveChanges();
            
            }




        }

    }
}
