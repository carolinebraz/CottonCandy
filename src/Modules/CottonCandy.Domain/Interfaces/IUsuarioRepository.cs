using CottonCandy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> InserirUsuario(Usuario usuario);
        Task<Usuario> ObterUsuario(int id);
        Task<Usuario> ObterUsuarioPorLogin(string login);
        Task<Usuario> ObterPerfil(int id);
        Task<Usuario> GetNomeFotoByIdUsuarioAsync(int idUsuario);
        Task<List<String>> GetEmail();
    }
}