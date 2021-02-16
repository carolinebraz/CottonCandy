using CottonCandy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Interfaces
{
    public interface IAmigosRepository
    {
        Task<int> SeguirAsync(Amigos amigo);
        Task<List<Amigos>> GetListaAmigos(int idUsuarioSeguidor);
    }
}
