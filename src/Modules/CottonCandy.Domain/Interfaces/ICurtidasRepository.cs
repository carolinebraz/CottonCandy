using CottonCandy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Interfaces
{
    public interface ICurtidasRepository
    {
        Task<string> Curtir(Curtidas curtidas);
        Task<string> Descurtir(int id);
        Task<int> ObterCurtidas(int postagemId);
        Task<List<Curtidas>> GetByUsuarioIdAsync(int usuarioId);
        Task<List<Curtidas>> GetByPostagemIdAsync(int postagemId);
        Task<Curtidas> GetByUsuarioIdAndPostagemIdAsync(int usuarioId, int postagemId);
    }
}