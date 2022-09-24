using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestaoProducao_MVC.Services
{
    public class MaquinaService
    {

        private readonly GestaoProducao_MVCContext _context;


        public MaquinaService(GestaoProducao_MVCContext context) { _context = context; }



        //Busca todas Maquinas
        public async Task<List<Maquina>> FindAllAsync()
        {
            return await _context.Maquina.ToListAsync();
        }


        //Busca uma maquina por ID;
        public async Task<Maquina> FindByIdAsync(int id)
        {
            return await _context.Maquina.FirstOrDefaultAsync(obj => obj.Id == id);
        }



        //Cria uma maquina
        public async Task InsertAsync(Maquina obj)
        {
            _context.Maquina.Add(obj);
            await _context.SaveChangesAsync();
        
        }



        //Deleta uma maquina
        public async Task RemoveAsync(int id)
        {

            try
            {
                var obj = await  _context.Maquina.FindAsync(id);
                _context.Maquina.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //Tratar caso de algum erro;
            }
        }


        public async Task UpdateAsync(Maquina obj)
        {
            bool seExiste = await _context.Maquina.AnyAsync(x => x.Id == obj.Id);
            if (!seExiste)
            {
                //se não existe retorna nao encontrado
                throw new Exception("Elemento nao encontrado!!!");

            }
            try
            {
                _context.Maquina.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);

            }
        }


     }
}
