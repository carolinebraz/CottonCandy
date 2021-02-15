using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CottonCandy.Domain.Entities;

namespace CottonCandy.Domain.Interfaces
{
   
        public interface IComentarioRepository
        {
        public Task<int> InserirAsync(Comentario comentario);
        public Task<List<Comentario>> PegarComentariosPorIdPostagemAsync(int idPostagem);

        }
    
}
