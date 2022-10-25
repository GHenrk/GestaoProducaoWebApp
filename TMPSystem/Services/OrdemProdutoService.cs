using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

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
            var list = await _context.OrdemProduto.OrderByDescending(x => x.DataVenda).ToListAsync();

            list = await VerificaStatusList(list);


            return list;


        }



        //Retorna uma OP pelo ID Enviado;
        //Get
        public async Task<OrdemProduto> FindByIdAsync(int id)
        {
            OrdemProduto op = await _context.OrdemProduto.FirstOrDefaultAsync(obj => obj.Id == id);

            if (op != null)
            {
                op = await VerificaStatus(op);

            }

            return op;
        }

        //Insere um obj no Database;
        public async Task InsertAsync(OrdemProduto obj)
        {
            _context.OrdemProduto.Add(obj);
            await _context.SaveChangesAsync();

        }


        //Essa função só pode ser executada pelo setor Vendas e Adm Geral;
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.OrdemProduto.FindAsync(id);
                _context.OrdemProduto.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
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

            }
            catch (DbUpdateConcurrencyException e)
            {
                //Trata erro caso ocorrer algum erro na hora do update;
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }


        public async Task<OrdemProduto> VerificaStatus(OrdemProduto op)
        {
            if (op.OpStatus == Models.Enums.OpStatus.Finalizado)
            {
                return op;
            }

            if (op.OpStatus == Models.Enums.OpStatus.Entregue)
            {
                return op;
            }

            //verificaSetemAlgumPontoAtivo;

            var list = from obj in _context.Processo select obj;

            list = list.Where(obj => obj.OrdemProdutoId == op.Id);

            List<Processo> processos = await list.ToListAsync();

            if (processos != null)
            {

                foreach (var processo in processos)
                {


                    List<Apontamento> apontamentosList = _context.Apontamento.Where(obj => obj.ProcessoId == processo.Id).ToList();
                    //pega lista de apontamentos de cada processo;

                    if (apontamentosList != null)
                    {
                        foreach(var item in apontamentosList)
                        {
                            if (item.IsAtivo)
                            {
                                op.OpStatus = Models.Enums.OpStatus.Fabricação;
                                return op;
                            }
                        }
                                        
                        op.OpStatus = Models.Enums.OpStatus.Iniciado;
                        return op;
                    }

                    op.OpStatus = Models.Enums.OpStatus.Aguardando;
                    return op;

                }

            }
            return op;

        }


        public async Task<List<OrdemProduto>> VerificaStatusList(List<OrdemProduto> list)
        {
            List<OrdemProduto> newList = new List<OrdemProduto>();
            foreach (var item in list)
            {
                newList.Add(await VerificaStatus(item));
            } 

            return newList;
        }









    }




}



