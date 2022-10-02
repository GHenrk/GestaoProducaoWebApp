using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoProducao_MVC.Services
{
    public class CodigoParadaService
    {
        private readonly GestaoProducao_MVCContext _context;

        public CodigoParadaService(GestaoProducao_MVCContext context)
        {
            _context = context;
        }

        public async Task<List<CodigoParada>> FindAllAsync()
        {
            return await _context.CodigoParada.ToListAsync();
        } 


        public async Task<CodigoParada> FindByIdAsync(int id)
        {
            return await _context.CodigoParada.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<List<CodigoParada>> FindByNameCodeAsync(string searchString)
        {
            var result = from obj in _context.CodigoParada select obj;

            
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(x => x.Descricao.Contains(searchString) || x.Id.ToString() == searchString);

            }


            return await result.ToListAsync();
        }


        public async Task InserAsync(CodigoParada obj)
        {
            _context.CodigoParada.Add(obj);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.CodigoParada.FindAsync(id);

                _context.CodigoParada.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
               //Tratar esse erro
            }
        }


        public async Task UpdateAsync(CodigoParada obj)
        {
            bool seExiste = await _context.CodigoParada.AnyAsync(x => x.Id == obj.Id);
            if (!seExiste)
            {
                //se não existe retorna nao encontrado
                throw new Exception("Elemento nao encontrado!!!");

            }
            try
            {
                _context.CodigoParada.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);

            }
        }

        public async Task<bool> isExist(int? id)
        {
            if (id == null)
            {
                return false;
            }

            return await _context.CodigoParada.AnyAsync(x => x.Id == id.Value);
        }
    }


}

