using CottonCandy.Application.AppPostagem.Input;
using CottonCandy.Application.AppPostagem.Interfaces;
using CottonCandy.Domain.Core.Interfaces;
using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem
{
    public class PostagemAppService : IPostagemAppService
    {
        private readonly IPostagemRepository _postagemRepository;
        private readonly ILogado _logado;
        public PostagemAppService(IPostagemRepository postagemRepository,
        ILogado logado)
        {
            _postagemRepository = postagemRepository;
            _logado = logado;
        }

        public async Task<List<Postagem>> GetByUserIdAsync()
        {
            var usuarioId = _logado.GetUsuarioLogadoId();

            var postagens = await _postagemRepository.ObterInformacoesPorIdAsync(usuarioId)
                                    .ConfigureAwait(false);
            return postagens;
        }

        public async Task<Postagem> InsertAsync(PostagemInput input)
        {
            var usuarioId = _logado.GetUsuarioLogadoId();

            var postagem = new Postagem(input.Texto, input.FotoPost, usuarioId);

            //Validar classe com dados obrigatorios..

            int id = await _postagemRepository
                             .InsertAsync(postagem)
                             .ConfigureAwait(false);



            postagem.SetId(id);

            return postagem;
        }
    }
}
