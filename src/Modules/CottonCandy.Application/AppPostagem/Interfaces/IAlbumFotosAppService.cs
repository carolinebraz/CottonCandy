using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem.Interfaces
{
    public interface IAlbumFotosAppService
    {
        Task<List<string>> ObterFotos(int idUsuario);
    }
}