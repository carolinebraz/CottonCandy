using CottonCandy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem.Interfaces
{
    public interface ICurtidasAppService
    {
        Task<string> Curtir(int postagemId);
        Task<int> ObterCurtidas(int postagemId);
        Task<List<Curtidas>> GetByUsuarioIdAsync(int usuarioId);
        Task<List<Curtidas>> GetByPostagemIdAsync(int postagemId);
    }
}