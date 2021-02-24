using CottonCandy.Application.AppPostagem.Input;
using CottonCandy.Application.AppUsuario.Output;
using CottonCandy.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem.Interfaces
{
    public interface IPostagemAppService
    {
        Task<Postagem> InserirPostagem(PostagemInput input);
        Task<List<Postagem>> ObterPostagens();
        Task<List<PostagemViewModel>> ObterLinhaDoTempo();
    }
}