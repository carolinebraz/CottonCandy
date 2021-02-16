using CottonCandy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUsuario.Interfaces
{
    public interface IAmigosAppService
    {
        Task<int> SeguirAsync(int idSeguido);
        Task<List<Amigos>> GetListaAmigos();
    }
}
