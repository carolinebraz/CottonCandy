using CottonCandy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Interfaces
{
    public interface IAmigosRepository
    {
        Task<int> SeguirAsync(Amigos amigo);
        Task<List<int>> GetListaAmigos(int idUsuarioSeguidor);
        Task<List<Amigos>> GetListaAmigosNomeId(int idUsuarioSeguidor);
    }
}