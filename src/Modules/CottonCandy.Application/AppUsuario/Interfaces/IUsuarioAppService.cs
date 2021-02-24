using CottonCandy.Application.AppUsuario.Output;
using CottonCandy.Application.AppUsuario.Input;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUsuario.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<UsuarioViewModel> InserirUsuario(UsuarioInput input);
        Task<UsuarioViewModel> ObterUsuario(int id);
        Task<PerfilUsuarioViewModel> ObterPerfil(int id);
    }
}