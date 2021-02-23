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
        private readonly IAmigosRepository _amigosRepository;
        private readonly IPostagemRepository _postagemRepository;
        private readonly ILogado _logado;

        public ComentarioAppService(IComentarioRepository comentarioRepositorio,
                                    IAmigosRepository amigosRepository,
                                    ILogado logado,
                                    IPostagemRepository postagemRepository)
        {
            _comentarioRepository = comentarioRepositorio;
            _amigosRepository = amigosRepository;
            _postagemRepository = postagemRepository;
            _logado = logado;
        }

        public async Task<List<Comentario>> PegarComentariosPorIdPostagemAsync(int idPostagem)
        {
            var comentarios = await _comentarioRepository
                                        .PegarComentariosPorIdPostagemAsync(idPostagem)
                                        .ConfigureAwait(false);

            return comentarios;
        }

        public async Task<Comentario> InserirAsync(int idPostagem, ComentarioInput input)
        {
            var postagens = await _postagemRepository
                                    .GetIdPostagensAsync()
                                    .ConfigureAwait(false);

            if (!postagens.Contains(idPostagem))
            {
                throw new Exception("Não existe publicação com esse ID");
            }

            var usuarioId = _logado.GetUsuarioLogadoId();

            var usuarioPostagemId = await _postagemRepository
                                                .GetUsuarioIdByPostagemId(idPostagem);

            var amigosId = await _amigosRepository
                                    .ObterListaDeAmigos(usuarioId)
                                    .ConfigureAwait(false);


            if (amigosId.Contains(usuarioPostagemId) || usuarioPostagemId == usuarioId)
            {
                var comentario = new Comentario(idPostagem, usuarioId, input.Texto);

                if (!comentario.EhValido())
                {
                    throw new ArgumentException("Você não pode inserir um comentário vazio");
                }

                var id = await _comentarioRepository
                                  .InserirAsync(comentario)
                                  .ConfigureAwait(false);

                comentario.SetId(id);

                return comentario;
            }
            else
            {
                throw new Exception("Você não pode comentar essa publicação, porque você não segue este usuário");
            }
        }
    }
}