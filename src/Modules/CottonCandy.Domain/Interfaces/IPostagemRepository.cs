using CottonCandy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Interfaces
{
    public interface IPostagemRepository
    {
        Task<int> InserirPostagem(Postagem postagem);
        Task<List<int>> ObterPostagens();
        Task<List<Postagem>> ObterLinhaDoTempo(int id);
        Task<List<Postagem>> ObterPerfil(int usuarioId);
        Task<List<string>> ObterFotos(int usuarioId);
        Task<int> GetUsuarioIdByPostagemId(int postagemId);
    }
}