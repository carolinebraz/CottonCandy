using CottonCandy.Domain.Entities;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> InserirAsync(Usuario usuario);
        Task<Usuario> ObterPorLoginAsync(string login);
        Task<Usuario> ObterPorIdAsync(int id);
    }
}
