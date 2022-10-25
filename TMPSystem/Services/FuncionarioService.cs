using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoProducao_MVC.Services
{
    public class FuncionarioService
    {
        private readonly GestaoProducao_MVCContext _context;


        public FuncionarioService(GestaoProducao_MVCContext context) { _context = context; }



        //Busca todas Funcionarios
        public async Task<List<Funcionario>> FindAllAsync()
        {
            return await _context.Funcionario.ToListAsync();
        }


        //Busca uma funcionario por ID;
        public async Task<Funcionario> FindByIdAsync(int id)
        {
            return await _context.Funcionario.FirstOrDefaultAsync(obj => obj.Id == id);
        }


        //Busca funcionario em pesquisa
        public async Task<List<Funcionario>> FindByNameCodeAsync(string searchString)
        {
            var result = from obj in _context.Funcionario select obj;

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(obj => obj.Name.Contains(searchString) || obj.Cargo.Contains(searchString) || obj.Id.ToString() == searchString);
            }


            return await result.ToListAsync();

        }



        //Cria uma Funcionario
        public async Task InsertAsync(Funcionario obj)
        {
            _context.Funcionario.Add(obj);
            await _context.SaveChangesAsync();

        }



        //Deleta uma Funcionario
        public async Task RemoveAsync(int id)
        {

            try
            {
                var obj = await _context.Funcionario.FindAsync(id);
                _context.Funcionario.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //Tratar caso de algum erro;
            }
        }


        public async Task UpdateAsync(Funcionario obj)
        {
            bool seExiste = await _context.Funcionario.AnyAsync(x => x.Id == obj.Id);
            if (!seExiste)
            {
                //se não existe retorna nao encontrado
                throw new Exception("Elemento nao encontrado!!!");

            }
            try
            {
                _context.Funcionario.Update(obj);
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

            return await _context.Funcionario.AnyAsync(x => x.Id == id.Value);
        }
    }
}
