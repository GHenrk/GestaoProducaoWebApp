using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoProducao_MVC.Services
{
    public class UsuarioService
    {
        private readonly GestaoProducao_MVCContext _context;

        public UsuarioService(GestaoProducao_MVCContext context)
        {
            _context = context;
        }


        public async Task<List<Usuario>> FindAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }


        
        public async Task<Usuario> FindByIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(obj => obj.Id == id);
        }


        public async Task<List<Usuario>> FindByNameCodeAsync(string searchString)
        {
            var result = from obj in _context.Usuarios select obj;

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(obj => obj.Nome.Contains(searchString) || obj.Email.ToString() == searchString);
            }


            return await result.ToListAsync();

        }



        
        public async Task InsertAsync(Usuario usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

        }



        
        public async Task RemoveAsync(int id)
        {

            try
            {
                var obj = await _context.Usuarios.FindAsync(id);
                _context.Usuarios.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //Tratar caso de algum erro;
            }
        }


        public async Task UpdateAsync(Usuario obj)
        {
            bool seExiste = await _context.Usuarios.AnyAsync(x => x.Id == obj.Id);
            if (!seExiste)
            {
                //se não existe retorna nao encontrado
                throw new Exception("Elemento nao encontrado!!!");

            }
            try
            {
                obj.DataAtualizacao = DateTime.Now;
                _context.Usuarios.Update(obj);
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

            return await _context.Usuarios.AnyAsync(x => x.Id == id.Value);
        }


        public Usuario FindByLogin(string login)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());

        }


        public Usuario FindByLoginEmail(string email, string login)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper()); ;

        }



    }
}
