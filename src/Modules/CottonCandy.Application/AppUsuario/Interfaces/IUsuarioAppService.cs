using CottonCandy.Application.AppUsuario.Output;
using CottonCandy.Application.AppUsuario.Input;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUsuario.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<UsuarioViewModel> InsertAsync(UsuarioInput input);
        Task<UsuarioViewModel> GetByIdAsync(int id);
        Task<PerfilUsuarioViewModel> ObterInformacoesPorIdAsync(int id);
    }
}