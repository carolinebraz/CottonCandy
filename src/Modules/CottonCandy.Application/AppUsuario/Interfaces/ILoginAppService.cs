using CottonCandy.Application.AppUsuario.Output;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUsuario.Interfaces
{
    public interface ILoginAppService
    {
        Task<UsuarioViewModel> LoginAsync(string email, string senha);
    }
}