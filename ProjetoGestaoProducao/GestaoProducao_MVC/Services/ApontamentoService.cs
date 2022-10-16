using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace GestaoProducao_MVC.Services
{
    public class ApontamentoService
    {
        private readonly GestaoProducao_MVCContext _context;

        public ApontamentoService(GestaoProducao_MVCContext context)
        {
            _context = context;
        }


        //Busca todos
        public async Task<List<Apontamento>> FindAllAsync()
        {
            var list = await _context.Apontamento.OrderByDescending(x => x.DataInicial)
                 .Include(obj => obj.Maquina)
                 .Include(obj => obj.Funcionario)
                 .Include(obj => obj.Processo)
                 .ToListAsync();

            list = ConvertTimeList(list);

            return list;
        }


        //Busca por Id
        public async Task<Apontamento> FindByIdAsync(int id)
        {
            var obj = await _context.Apontamento
                .Include(obj => obj.Maquina)
                .Include(obj => obj.Funcionario)
                .Include(obj => obj.Processo)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (obj != null)
            {
                obj = ConvertTime(obj);
            }


            return obj;

        }

        public async Task<Apontamento> FindApontamentoMachineAsync(int id)
        {
            var obj = await _context.Apontamento.FirstOrDefaultAsync(x => x.MaquinaId == id && x.IsAtivo == true);

            if (obj != null)
            {
                obj = ConvertTime(obj);
            }


            return obj;
        }



        //Busca por funcionario e Apontamento Ativo;
        //Método utilizado para EncerrarApontamento. 


        public async Task<Apontamento> FindByMaquinaAtiva(int id)
        {
            var result = from obj in _context.Apontamento select obj;


            //Seleciona os apontamentos do Funcionario
            result = result.Where(x => x.MaquinaId == id);
            result = result.Where(x => x.IsAtivo == true);


            return await result.FirstOrDefaultAsync();

        }


        //Método que retorna todos pontos de um funcionario --- TESTAR --
        public async Task<List<Apontamento>> FindAllByFuncAsync(Funcionario funcionario)
        {
            var result = from obj in _context.Apontamento select obj;

            result = result.Where(obj => obj.FuncionarioId == funcionario.Id);

            return await result
                .Include(obj => obj.Maquina)
                .Include(obj => obj.Funcionario)
                .Include(obj => obj.Processo)
                .ToListAsync();
        }


        //Busca por processo
        public async Task<List<Apontamento>> FindByProcesso(Processo processo)
        {
            var result = from obj in _context.Apontamento select obj;

            result = result.Where(x => x.ProcessoId == processo.Id);

            var list = await result
                .Include(obj => obj.Maquina)
                .Include(obj => obj.Funcionario)
                .Include(obj => obj.RegistroParadas)
                .OrderByDescending(obj => obj.IsAtivo)
                .ToListAsync();


            list = ConvertTimeList(list);

             return list;

        }


        //Busca por nome;
        public async Task<List<Apontamento>> FindByNameCodeAsync(string searchString)
        {
            var result = from obj in _context.Apontamento select obj;

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(obj => obj.Maquina.Nome.Contains(searchString) || obj.Processo.Id.ToString() == searchString || obj.Funcionario.Name.Contains(searchString));
            }


            var list = await result.OrderByDescending(obj => obj.DataInicial)
                     .Include(obj => obj.Maquina)
                     .Include(obj => obj.Funcionario)
                     .Include(obj => obj.Processo)
                     .ToListAsync();

            list = ConvertTimeList(list);


            return list;
        }



        //Inicia um Apontamento;
        public async Task InsertAsync(Apontamento apontamento)
        {
            _context.Apontamento.Add(apontamento);
            await _context.SaveChangesAsync();
        }



        //Edit um Apontamento;
        //Essa função só pode ser executada pelo setor Vendas e AdmGeral;
        public async Task UpdateAsync(Apontamento apontamento)
        {
            bool seExiste = await _context.Apontamento.AnyAsync(x => x.Id == apontamento.Id);
            if (!seExiste)
            {
                //Se existe for falso retorna que nao encontrado;
                throw new Exception("Elemento não encontrado!!!!");
            }
            try
            {
                //faz o update do objeto;
                _context.Apontamento.Update(apontamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                //Trata erro caso ocorrer algum erro na hora do update;
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }



        //Remove um Apontamento;
        //Essa função só pode ser executada pelo  AdmGeral;
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Apontamento.FindAsync(id);
                if (obj == null)
                {
                    throw new ApplicationException();
                }
                _context.Apontamento.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException(e.Message);
            }
        }






        //Converte tempo de uma lista de apontamentos;
        public List<Apontamento> ConvertTimeList(List<Apontamento> list)
        {
            foreach (var item in list)
            {

                //CalculaParada(item);
                if (item.TempoTotal == null)
                {
                    TimeSpan decorrido = DateTime.Now - item.DataInicial;
                    string time = (int)decorrido.TotalHours + decorrido.ToString("\\:mm\\:ss");
                    item.TotalTime = time;
                    item.TempoDecorridoSpan = decorrido;
                }
                else
                {
                    TimeSpan decorrido = TimeSpan.FromTicks(item.TempoTotal.Value);
                    decorrido.ToString();
                    string time = (int)decorrido.TotalHours + decorrido.ToString("\\:mm\\:ss");
                    item.TotalTime = time;
                    item.TempoDecorridoSpan = decorrido;
                }

            }



            return list;
        }



        //Converte tempo de um unico apontamento;
        public Apontamento ConvertTime(Apontamento apontamento)
        {
            //CalculaParada(apontamento);
            if (apontamento.TempoTotal == null)
            {
                TimeSpan decorrido = DateTime.Now - apontamento.DataInicial;
                string time = (int)decorrido.TotalHours + decorrido.ToString("\\:mm\\:ss");
                apontamento.TotalTime = time;
                apontamento.TempoDecorridoSpan = decorrido;
            }
            else
            {
                TimeSpan decorrido = TimeSpan.FromTicks(apontamento.TempoTotal.Value);
                string time = (int)decorrido.TotalHours + decorrido.ToString("\\:mm\\:ss");
                apontamento.TotalTime = time;
                apontamento.TempoDecorridoSpan = decorrido;
            }


            return apontamento;
        }


        public void CalculaParada(List<Apontamento> apontamentos)
        {

            TimeSpan tempoTotalParada = TimeSpan.Zero;

            foreach (var item in apontamentos)
            {
                tempoTotalParada += item.TempoTotalParadas();

            }



        }



        //Verficia se existe um Apt ativo para o funcionario
        public async Task<bool> isFuncionarioAtivo(int? id)
        {
            if (id == null)
            {
                return true;
            }

            return await _context.Apontamento.AnyAsync(x => x.FuncionarioId == id.Value && x.IsAtivo == true);
        }


        //Verficia se existe um Apt ativo para a maquina
        public async Task<bool> isMaquinaAtiva(int id)
        {
            return await _context.Apontamento.AnyAsync(x => x.MaquinaId == id && x.IsAtivo == true);
        }


    }
}
