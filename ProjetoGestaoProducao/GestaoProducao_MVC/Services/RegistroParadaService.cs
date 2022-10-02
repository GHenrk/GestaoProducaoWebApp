using GestaoProducao_MVC.Data;
using GestaoProducao_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoProducao_MVC.Services
{
    public class RegistroParadaService
    {
        private readonly GestaoProducao_MVCContext _context;

        public RegistroParadaService(GestaoProducao_MVCContext context)
        {
            _context = context;
        }

        public async Task<List<RegistroParada>> FindAllAsync()
        {
            var list = await _context.RegistroParada.OrderByDescending(x => x.DataInicial)
                .Include(obj => obj.Apontamento)
                .Include(obj => obj.Apontamento.Funcionario)
                .Include(obj => obj.Apontamento.Maquina)
                .Include(obj => obj.CodigoParada)
                .ToListAsync();

            list = ConvertTimeList(list);
            return list;

        }


        public async Task<RegistroParada> FindByIdAsync(int id)
        {
            var obj = await _context.RegistroParada
                .Include(obj => obj.Apontamento)
                 .Include(obj => obj.Apontamento.Funcionario)
                .Include(obj => obj.Apontamento.Maquina)
                .Include(obj => obj.CodigoParada)
                .FirstOrDefaultAsync(X => X.Id == id);

            obj = ConvertTime(obj);

            return obj;
        }
    
        
       public async Task InsertAsync(RegistroParada registroParada)
        {
            _context.RegistroParada.Add(registroParada);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(RegistroParada registroParada)
        {
            bool seExiste = await _context.RegistroParada.AnyAsync(x => x.Id == registroParada.Id);

            if (!seExiste)
            {
                throw new Exception("Elemento não encontrado!!!");
            }
            try
            {
                _context.RegistroParada.Update(registroParada);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }


        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.RegistroParada.FindAsync(id);
                if (obj == null)
                {
                    throw new ApplicationException();
                }
                _context.RegistroParada.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException(e.Message);
            }
        }


        public RegistroParada ConvertTime(RegistroParada registroParada)
        {
            if (registroParada.TempoTotal == null)
            {
                TimeSpan decorrido = DateTime.Now - registroParada.DataInicial;
                registroParada.TempoDeParada = decorrido;
                string time = (int)decorrido.TotalHours + decorrido.ToString("\\:mm\\:ss");
                registroParada.TempoDeParadaFormatado = time;
            } else
            {
                TimeSpan decorrido = TimeSpan.FromTicks(registroParada.TempoTotal.Value);
                registroParada.TempoDeParada = decorrido;
                string time = (int)decorrido.TotalHours + decorrido.ToString("\\:mm\\:ss");
                registroParada.TempoDeParadaFormatado = time;

            }

            return registroParada;

        }

        public List<RegistroParada>ConvertTimeList(List<RegistroParada> list)
        {
            foreach(var item in list)
            {
                if (item.TempoTotal == null)
                {
                    TimeSpan decorrido = DateTime.Now - item.DataInicial;
                    item.TempoDeParada = decorrido;
                    string time = (int)decorrido.TotalHours + decorrido.ToString("\\:mm\\:ss");
                    item.TempoDeParadaFormatado = time;
                }else
                {
                    TimeSpan decorrido = TimeSpan.FromTicks(item.TempoTotal.Value);
                    item.TempoDeParada = decorrido;
                    string time = (int)decorrido.TotalHours + decorrido.ToString("\\:mm\\:ss");
                    item.TempoDeParadaFormatado = time;

                }
            }

            return list;
        }
    
    }
}
