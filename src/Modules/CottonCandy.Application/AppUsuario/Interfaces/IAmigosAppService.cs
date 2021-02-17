using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUsuario.Interfaces
{
    public interface IAmigosAppService
    {
        Task<int> SeguirAsync(int idSeguido);
        Task<List<int>> GetListaAmigos();
    }
}