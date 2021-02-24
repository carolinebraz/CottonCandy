using CottonCandy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Interfaces
{
    public interface IAmigosRepository
    {
        Task<int> SeguirUsuario(Amigos amigo);
        Task<List<int>> ObterListaDeAmigos(int idUsuarioSeguidor);
        Task<List<Amigos>> ObterNomeListaDeAmigos(int idUsuarioSeguidor);
        Task<string> DeixarDeSeguir(Amigos amizade);
    }
}