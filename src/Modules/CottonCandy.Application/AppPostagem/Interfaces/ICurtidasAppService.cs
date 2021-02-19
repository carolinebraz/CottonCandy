using CottonCandy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem.Interfaces
{
    public interface ICurtidasAppService
    {
        Task<string> InsertAsync(int postagemId);
        Task<List<Curtidas>> GetByUsuarioIdAsync(int usuarioId);
        Task<List<Curtidas>> GetByPostagemIdAsync(int postagemId);
        Task<int> GetQtdeCurtidasByPostagemIdAsync(int postagemId);
    }
}