using CottonCandy.Application.AppUsuario.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUsuario.Interfaces
{
    public interface IAmigosAppService
    {
        Task<string> SeguirAsync(int idSeguido);
        Task<List<AmigosViewModel>> GetListaAmigos();
    }
}