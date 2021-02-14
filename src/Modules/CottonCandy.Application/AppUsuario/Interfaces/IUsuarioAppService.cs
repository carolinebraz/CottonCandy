using CottonCandy.Application.AppUser.Output;
using CottonCandy.Application.AppUsuario.Input;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUser.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<UsuarioViewModel> InserirAsync(UsuarioInput input);
        Task<UsuarioViewModel> ObterPorIdAsync(int id);
    }
}
