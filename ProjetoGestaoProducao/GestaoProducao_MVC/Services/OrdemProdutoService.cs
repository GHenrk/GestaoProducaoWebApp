using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoProducao_MVC.Services
{
    public class OrdemProdutoService
    {
        private readonly GestaoProducao_MVCContext _context;


        public OrdemProdutoService(GestaoProducao_MVCContext context)
        {
            _context = context;
        }


        //Retorna lista com todas OPs ordenada por Data de venda mais recente;
        //Get
        public async Task<List<OrdemProduto>> FindAllAsync()
        {
           return await _context.OrdemProduto.OrderByDescending(x => x.DataVenda).ToListAsync();
        
        }



        //Retorna uma OP pelo ID Enviado;
        //Get
        public async Task<OrdemProduto> FindByIdAsync(int id)
        {
            return await _context.OrdemProduto.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        //Insere um obj no Database;
        public async Task InsertAsync(OrdemProduto obj)
        {
           _context.OrdemProduto.Add(obj);
           await _context.SaveChangesAsync();

        }


        //Essa função só pode ser executada pelo setor Vendas e Adm Geral;
        public async Task RemoveAsync(int? id)
        {
            try
            {
                var obj = await _context.OrdemProduto.FindAsync(id);
                _context.OrdemProduto.Remove(obj);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException)
            {
                //Tratar caso de algum erro na exclusão
            }

        }


        //Essa função só pode ser executada pelo setor Vendas e AdmGeral;
        public async Task UpdateAsync(OrdemProduto obj)
        {
            bool seExiste = await _context.OrdemProduto.AnyAsync(x => x.Id == obj.Id);
            if (!seExiste)
            {
                //Se existe for falso retorna que nao encontrado;
                throw new Exception("Elemento não encontrado!!!!");
            }
            try
            {
                //faz o update do objeto;
                _context.OrdemProduto.Update(obj);
                await _context.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException e)
            {
                //Trata erro caso ocorrer algum erro na hora do update;
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }







    }
}
