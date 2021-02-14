using CottonCandy.Application.AppPostagem.Interfaces;
using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem
{
    public class CurtidasAppService : ICurtidasAppService
    {
        private readonly ICurtidasRepository _curtidasRepository;
       // private readonly ILogged _logged;

        public CurtidasAppService(ICurtidasRepository CurtidasRepository)
                           //    ILogged logged)
        {
            _curtidasRepository = CurtidasRepository;
           // _logged = logged;
        }

        public async Task<int> GetQtdeCurtidasByPostagemIdAsync(int postagemId)
        {
            return await _curtidasRepository
                            .GetQtdeCurtidasByPostagemIdAsync(postagemId)
                            .ConfigureAwait(false);
        }

        public async Task<List<Curtidas>> GetByPostagemIdAsync(int postagemId)
        {
            return await _curtidasRepository.GetByPostagemIdAsync(postagemId)
                            .ConfigureAwait(false);
        }

        public async Task<List<Curtidas>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _curtidasRepository.GetByUsuarioIdAsync(usuarioId)
                            .ConfigureAwait(false);
        }


        public async Task<int> InsertAsync(int postagemId)
        {
            //Método que insere curtida na postagem

            var usuarioId = 1;//_logged.GetUserLoggedId();

            var curtida = await _curtidasRepository
                                                .GetByUsuarioIdAndPostagemIdAsync(usuarioId, postagemId)
                                                .ConfigureAwait(false);
            if (curtida != null)
            {
               await _curtidasRepository.DeleteAsync(curtida.Id)
                                        .ConfigureAwait(false);
                return default;
            }
            else {
                var novaCurtida = new Curtidas(usuarioId, postagemId);
                //Validar os dados obriatorios..

                return await _curtidasRepository
                          .InsertAsync(novaCurtida)
                          .ConfigureAwait(false);
            }
        }
    }
}
