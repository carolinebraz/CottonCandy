using CottonCandy.Application.AppUsuario.Interfaces;
using CottonCandy.Application.AppUsuario.Output;
using CottonCandy.Domain.Core.Interfaces;
using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUsuario
{
    public class AmigosAppService : IAmigosAppService
    {
        private readonly IAmigosRepository _amigosRepository;
        private readonly ILogado _logado;

        public AmigosAppService(IAmigosRepository amigosRepository,
                                ILogado logado)
        {
            _amigosRepository = amigosRepository;
            _logado = logado;
        }

        public async Task<List<AmigosViewModel>> GetListaAmigos()
        {
            var idSeguidor = _logado.GetUsuarioLogadoId();

            var amigos = await _amigosRepository
                                    .GetListaAmigosNomeId(idSeguidor)
                                    .ConfigureAwait(false);

            List<AmigosViewModel> listaAmigos = new List<AmigosViewModel>();

            foreach(Amigos amigo in amigos)
            {
                listaAmigos.Add(new AmigosViewModel()
                {
                     IdAmigo = amigo.IdUsuarioSeguido, 
                     NomeAmigo = amigo.NomeAmigo
                });
            }

            return listaAmigos;

        }

        public async Task<int> SeguirAsync(int idSeguido)
        {
            var idSeguidor = _logado.GetUsuarioLogadoId();

            var idAmigos = await _amigosRepository
                                      .GetListaAmigos(idSeguidor)
                                      .ConfigureAwait(false);

            if (idAmigos.Contains(idSeguido))
            {
                throw new Exception("Você já segue esse usuário");
            }

            var novoAmigo = new Amigos(idSeguidor, idSeguido);

            int idRelacionamento = await _amigosRepository
                                              .SeguirAsync(novoAmigo)
                                              .ConfigureAwait(false);

            return idRelacionamento;
        }
    }
}