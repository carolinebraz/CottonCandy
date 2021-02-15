using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CottonCandy.Application.AppPostagem.Input;
using CottonCandy.Application.AppPostagem.Interfaces;
using CottonCandy.Domain.Core.Interfaces;
using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;

namespace CottonCandy.Application.AppPostagem
{

   


    public class ComentarioAppService : IComentarioAppService
    {

        private readonly IComentarioRepository _comentarioRepository;
        private readonly ILogado _logado;


        public ComentarioAppService(IComentarioRepository comentarioRepositorio, ILogado logado)
        {
            _comentarioRepository = comentarioRepositorio;
            _logado = logado;
        }



      

        async Task<List<Comentario>> IComentarioAppService.PegarComentariosPorIdPostagemAsync(int idPostagem)
        {
            var comentarios = await _comentarioRepository
                                     .PegarComentariosPorIdPostagemAsync(idPostagem)
                                     .ConfigureAwait(false);

            return comentarios;
        }

        async Task<Comentario> IComentarioAppService.InserirAsync(int idPostagem, ComentarioInput input)
        {
            var usuarioId = _logado.GetUsuarioLogadoId();

            var comentario = new Comentario(idPostagem, usuarioId, input.Texto);

            //Validar os dados obrigatorios

            var id = await _comentarioRepository
                              .InserirAsync(comentario)
                              .ConfigureAwait(false);

            comentario.SetId(id);

            return comentario;
        }
    }
}
