using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Processo.OrderByDescending(x => x.DataCriacao).Include(obj => obj.OrdemProduto).ToListAsync();
        }


        //Busca processo por ID;
        public async Task<Processo> FindByIdAsync(int id)
        {
            return await _context.Processo.Include(obj => obj.OrdemProduto).FirstOrDefaultAsync(x => x.Id == id);
        }



        //Busca processos que pertencem a uma OP;
        public async Task<List<Processo>> FindByOpAsync(OrdemProduto op)
        {
            //Seleciona o Objeto da tabela processo
            var result = from obj in _context.Processo select obj;
            //Onde a OP seja igual a OP Enviada como parametro
            result.Where(x => x.OrdemProduto == op);

            return await result.ToListAsync();
            

        }


        ////Busca por nome, IMPLEMENTAR DEPOIS;
        //public async Task<Processo> FindByNameAsync(string name)
        //{
        //    return await _context.
        //}



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
            }catch (DbUpdateException e)
            {
                throw new DbUpdateException(e.Message);
            }

        }




    }
}
