using CottonCandy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> InsertAsync(Usuario usuario);
        Task<Usuario> GetByLoginAsync(string login);
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> ObterInformacoesPorIdAsync(int id);
        Task<int> SeguirAsync(int idUsuarioSeguidor, int idUsuarioSeguido);
    }
}
