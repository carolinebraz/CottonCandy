using CottonCandy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Interfaces
{
    public interface IPostagemRepository
    {
        Task<int> InsertAsync(Postagem postagem);
        Task<List<Postagem>> ObterInformacoesPorIdAsync(int usuarioId);
        Task<List<String>> GetByUserIdOnlyPhotosAsync(int usuarioId);
        Task<List<Postagem>> GetLinhaDoTempoIdAsync(int id);
    }
}
