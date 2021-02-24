using CottonCandy.Application.AppUsuario.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUsuario.Interfaces
{
    public interface IAmigosAppService
    {
        Task<string> SeguirUsuario(int idSeguido);
        Task<List<AmigosViewModel>> ObterListaDeAmigos();
    }
}