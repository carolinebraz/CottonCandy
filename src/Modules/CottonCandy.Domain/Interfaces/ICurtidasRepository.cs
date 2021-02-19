using CottonCandy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Interfaces
{
    public interface ICurtidasRepository
    {
        Task<string> InsertAsync(Curtidas curtidas);
        Task<List<Curtidas>> GetByUsuarioIdAsync(int usuarioId);
        Task<List<Curtidas>> GetByPostagemIdAsync(int postagemId);
        Task <string> DeleteAsync(int id);
        Task<int> GetQtdeCurtidasByPostagemIdAsync(int postagemId);
        Task<Curtidas> GetByUsuarioIdAndPostagemIdAsync(int usuarioId, int postagemId);
    }
}