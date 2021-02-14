using CottonCandy.Application.AppPostagem.Input;
using CottonCandy.Application.AppPostagem.Interfaces;
using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Application
{
    public class PostagemAppService : IPostagemAppService
    {
        private readonly IPostagemRepository _postagemRepository;
        //private readonly ILogged _logged;
        public PostagemAppService(IPostagemRepository postagemRepository)
                               //  ILogged logged)
        {
            _postagemRepository = postagemRepository;
           // _logged = logged;
        }

        public async Task<List<Postagem>> GetByUserIdAsync()
        {
            var userId = 1; //_logged.GetUserLoggedId();

            var postages = await _postagemRepository.ObterInformacoesPorIdAsync(userId)
                                    .ConfigureAwait(false);
            return postages;
        }

        public async Task<Postagem> InsertAsync(PostagemInput input)
        {
            var userId = 1; // _logged.GetUserLoggedId();

            var postagem = new Postagem(input.Texto, input.FotoPost, userId);

            //Validar classe com dados obrigatorios..

            int id = await _postagemRepository
                             .InsertAsync(postagem)
                             .ConfigureAwait(false);



            postagem.SetId(id);

            return postagem;
        }
    }
}
