using CottonCandy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Interfaces
{
    public interface IPostagemRepository
    {
        Task<int> InsertAsync(Postagem postagem);
        Task<List<Postagem>> ObterPerfil(int usuarioId);
        Task<List<string>> ObterFotos(int usuarioId);
        Task<List<Postagem>> GetLinhaDoTempoDosAmigosAsync(int id);
        Task<int> GetUsuarioIdByPostagemId(int postagemId);
        Task<List<int>> GetIdPostagensAsync();
    }
}