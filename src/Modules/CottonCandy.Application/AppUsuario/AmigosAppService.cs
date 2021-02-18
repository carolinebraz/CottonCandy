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
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogado _logado;

        public AmigosAppService(IAmigosRepository amigosRepository,
                                IUsuarioRepository usuarioRepository,
                                ILogado logado)
        {
            _amigosRepository = amigosRepository;
            _usuarioRepository = usuarioRepository;
            _logado = logado;
        }

        public async Task<List<AmigosViewModel>> GetListaAmigos()
        {
            var idSeguidor = _logado.GetUsuarioLogadoId();

            var amigos = await _amigosRepository
                                    .GetListaAmigosNomeId(idSeguidor)
                                    .ConfigureAwait(false);

            List<AmigosViewModel> listaAmigos = new List<AmigosViewModel>();

            foreach (Amigos amigo in amigos)
            {
                listaAmigos.Add(new AmigosViewModel()
                {
                    IdAmigo = amigo.IdUsuarioSeguido,
                    NomeAmigo = amigo.NomeAmigo
                });
            }

            return listaAmigos;

        }

        public async Task<string> SeguirAsync(int idSeguido)
        {
            var idSeguidor = _logado.GetUsuarioLogadoId();

            if (idSeguido == idSeguidor)
            {
                throw new Exception("Você não pode seguir a si mesmo");
            }

            var idAmigos = await _amigosRepository
                                      .GetListaAmigos(idSeguidor)
                                      .ConfigureAwait(false);

            var usuario = await _usuarioRepository
                                      .GetNomeFotoByIdUsuarioAsync(idSeguido)
                                      .ConfigureAwait(false);

            if (idAmigos.Contains(idSeguido))
            {
                var amizades = await _amigosRepository
                                          .GetListaAmigosNomeId(idSeguidor)
                                          .ConfigureAwait(false);

                foreach (Amigos amizade in amizades)
                {
                    if (amizade.IdUsuarioSeguido == idSeguido)
                    {
                        string resultadoAmizade = await _amigosRepository
                                                             .DeixarDeSeguir(amizade)
                                                             .ConfigureAwait(false);
                        return "Você deixou de seguir " + amizade.NomeAmigo;
                    }

                }                              
            }

            var novoAmigo = new Amigos(idSeguidor, idSeguido);

            int idRelacionamento = await _amigosRepository
                                             .SeguirAsync(novoAmigo)
                                             .ConfigureAwait(false);

            var resultado = "Você está seguindo " + usuario.Nome;

            return resultado;
        }
    }
}