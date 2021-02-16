using CottonCandy.Application.AppPostagem.Input;
using CottonCandy.Application.AppUser.Output;
using CottonCandy.Application.AppUsuario.Output;
using CottonCandy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem.Interfaces
{
    public interface IPostagemAppService
    {
        Task<Postagem> InsertAsync(PostagemInput input);
        Task<List<Postagem>> GetByUserIdAsync();
        Task<List<PostagemViewModel>> ObterLinhaDoTempoAsync();
    }
}
