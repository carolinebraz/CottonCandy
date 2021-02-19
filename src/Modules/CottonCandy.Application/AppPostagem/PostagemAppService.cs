using CottonCandy.Application.AppPostagem.Input;
using CottonCandy.Application.AppPostagem.Interfaces;
using CottonCandy.Application.AppUsuario.Output;
using CottonCandy.Domain.Core.Interfaces;
using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem
{
    public class PostagemAppService : IPostagemAppService
    {
        private readonly IPostagemRepository _postagemRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogado _logado;
        public PostagemAppService(IPostagemRepository postagemRepository,
                                         ILogado logado,
                                         IUsuarioRepository usuarioRepository)
        {
            _postagemRepository = postagemRepository;
            _usuarioRepository = usuarioRepository;
            _logado = logado;
        }

        public async Task<List<Postagem>> GetByUserIdAsync()
        {
            var usuarioId = _logado.GetUsuarioLogadoId();

            var postagens = await _postagemRepository
                                     .ObterPerfil(usuarioId)
                                     .ConfigureAwait(false);
            return postagens;
        }

        public async Task<Postagem> InsertAsync(PostagemInput input)
        {
            var usuarioId = _logado.GetUsuarioLogadoId();

            var postagem = new Postagem(input.Texto, input.FotoPost, usuarioId);

            if (!postagem.EhValido())
            {
                throw new ArgumentException("Você não pode inserir uma postagem sem texto");
            }

            int id = await _postagemRepository
                              .InsertAsync(postagem)
                              .ConfigureAwait(false);

            postagem.SetId(id);

            return postagem;
        }

        public async Task<List<PostagemViewModel>> ObterLinhaDoTempoAsync()
        {

            var idUsuarioLogado = _logado.GetUsuarioLogadoId();

            var postagensDosAmigos = await _postagemRepository
                                              .GetLinhaDoTempoDosAmigosAsync(idUsuarioLogado)
                                              .ConfigureAwait(false);

            var postagensUsuario = await _postagemRepository
                                            .ObterPerfil(idUsuarioLogado)
                                            .ConfigureAwait(false);

            List<Postagem> listaTodasPostagens = new List<Postagem>();

            listaTodasPostagens.AddRange(postagensDosAmigos);
            listaTodasPostagens.AddRange(postagensUsuario);

            List<PostagemViewModel> listaPostagens = new List<PostagemViewModel>();

            foreach (Postagem postagem in listaTodasPostagens)
            {
                var usuarioId = await _postagemRepository
                                         .GetUsuarioIdByPostagemId(postagem.Id)
                                         .ConfigureAwait(false);

                var usuarioFotoNome = await _usuarioRepository
                                               .GetNomeFotoByIdUsuarioAsync(usuarioId)
                                               .ConfigureAwait(false);

                listaPostagens.Add(new PostagemViewModel()
                {
                    Id = postagem.Id,
                    NomeUsuario = usuarioFotoNome.Nome,
                    FotoUsuario = usuarioFotoNome.FotoPerfil,
                    TextoPost = postagem.Texto,
                    FotoPost = postagem.FotoPost,
                    DataPostagem = postagem.DataPostagem
                });

            } //for each

            List<PostagemViewModel> listaOrdenada = listaPostagens
                                                        .OrderBy(o => o.DataPostagem)
                                                        .ToList();

            return listaOrdenada;
        }
    }
}