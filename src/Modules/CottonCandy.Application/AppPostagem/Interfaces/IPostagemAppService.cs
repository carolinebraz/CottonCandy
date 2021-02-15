using CottonCandy.Application.AppPostagem.Input;
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
    }
}
