using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace GestaoProducao_MVC.Services
{
    public class ProcessoService
    {

        private readonly GestaoProducao_MVCContext _context;

        public ProcessoService(GestaoProducao_MVCContext context)
        {
            _context = context;
        }


        //Busca todos processos;
        public async Task<List<Processo>> FindAllAsync()
        {
            var list = await _context.Processo.OrderByDescending(x => x.DataCriacao).Include(obj => obj.OrdemProduto).ToListAsync();

            list = ConvertTimeList(list);
            return list;
        }


        //Busca processo por ID;
        public async Task<Processo> FindByIdAsync(int id)
        {
            Processo processo = await _context.Processo.Include(obj => obj.OrdemProduto).FirstOrDefaultAsync(x => x.Id == id);

            processo = ConvertTime(processo);

            return processo;
        }



        //Busca processos que pertencem a uma OP;
        public async Task<List<Processo>> FindByOpAsync(OrdemProduto ordemPrduto)
        {
            //Seleciona o Objeto da tabela processo
            var result = from obj in _context.Processo select obj;
            //Onde a OP seja igual a OP Enviada como parametro
            result = result.Where(x => x.OrdemProduto.Id == ordemPrduto.Id);

            var list = await result.ToListAsync();
            list = ConvertTimeList(list);

            return list;


        }


        //Busca por nome;
        public async Task<List<Processo>> FindByNameCodeAsync(string searchString)
        {
            var result = from obj in _context.Processo select obj;

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(obj => obj.CodigoPeca.Contains(searchString) || obj.Id.ToString() == searchString || obj.OrdemProdutoId.ToString() == searchString);
            }

            var list = await result.OrderByDescending(obj => obj.DataCriacao).Include(obj => obj.OrdemProduto).ToListAsync();

            list = ConvertTimeList(list);

            return list;

        }



        //Cria um processo;
        public async Task InsertAsync(Processo processo)
        {
            _context.Processo.Add(processo);
            await _context.SaveChangesAsync();
        }


        //Edit um processo;
        //Essa função só pode ser executada pelo setor Vendas e AdmGeral;
        public async Task UpdateAsync(Processo processo)
        {
            bool seExiste = await _context.Processo.AnyAsync(x => x.Id == processo.Id);
            if (!seExiste)
            {
                //Se existe for falso retorna que nao encontrado;
                throw new Exception("Elemento não encontrado!!!!");
            }
            try
            {
                //faz o update do objeto;
                _context.Processo.Update(processo);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {
                //Trata erro caso ocorrer algum erro na hora do update;
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }


        //Remove um processo;
        //Essa função só pode ser executada pelo setor Vendas e AdmGeral;
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Processo.FindAsync(id);
                if (obj == null)
                {
                    throw new ApplicationException();
                }
                _context.Processo.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException(e.Message);
            }

        }


        //Verifica se processo Existe;
        public async Task<bool> isExist(int? id)
        {
            if (id == null)
            {
                return false;
            }

            return await _context.Processo.AnyAsync(x => x.Id == id.Value);
        }




        public List<Processo> ConvertTimeList(List<Processo> list)
        {
            foreach (var item in list)
            {
                if (item.TempoEstimado != null)
                {
                    TimeSpan estimado = TimeSpan.FromTicks(item.TempoEstimado.Value);
                    item.TempoEstimadoSpan = estimado;
                    string time = (int)estimado.TotalHours + estimado.ToString("\\:mm\\:ss");
                    item.TempoEstimadoFormatado = time;
                }
            }

            return list;
        }



        //Converte tempo de um unico apontamento;
        public Processo ConvertTime(Processo processo)
        {
            if (processo.TempoEstimado != null)
            {
                TimeSpan estimado = TimeSpan.FromTicks(processo.TempoEstimado.Value);
                estimado.ToString();
                processo.TempoEstimadoSpan = estimado;
                string time = (int)estimado.TotalHours + estimado.ToString("\\:mm\\:ss");
                processo.TempoEstimadoFormatado = time;
            }

            return processo;
        }



    }
}
