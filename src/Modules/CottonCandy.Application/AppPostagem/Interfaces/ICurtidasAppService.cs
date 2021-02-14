using CottonCandy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem.Interfaces
{
    public interface ICurtidasAppService
    {
        Task<int> InsertAsync(int postagemId);
        Task<List<Curtidas>> GetByUsuarioIdAsync(int usuarioId);
        Task<List<Curtidas>> GetByPostagemIdAsync(int postagemId);
        Task<int> GetQtdeCurtidasByPostagemIdAsync(int postagemId);
    }
}
