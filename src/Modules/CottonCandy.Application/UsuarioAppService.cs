using CottonCandy.Application.AppUser.Interfaces;
using CottonCandy.Application.AppUser.Output;
using CottonCandy.Application.AppUsuario.Input;
using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUsuario
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IGeneroRepository _generoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioAppService(IGeneroRepository generoRepository,
                                IUsuarioRepository usuarioRepository)
        {
            _generoRepository = generoRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<UsuarioViewModel> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository
                                .GetByIdAsync(id)
                                .ConfigureAwait(false);

            if (usuario is null)
                return default;

            return new UsuarioViewModel()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                DataNascimento = usuario.DataNascimento,
                Email = usuario.Email,
                Genero = usuario.Genero,
                FotoPerfil = usuario.FotoPerfil,
                Cargo = usuario.Cargo,
                Cidade = usuario.Cidade
            };
        }

        public async Task<UsuarioViewModel> InsertAsync(UsuarioInput input)
        {
            var genero = await _generoRepository
                                   .GetByIdAsync(input.GeneroId)
                                   .ConfigureAwait(false);

            if (genero is null)
            {
                throw new ArgumentException("O genero que está tentando associar ao usuário não existe!");
            }

            var usuario = new Usuario(input.Nome,
                                        input.Email,
                                        input.Senha,
                                        input.DataNascimento,
                                        new Genero(genero.Id, genero.Descricao),
                                        input.FotoPerfil,
                                        input.Cargo,
                                        input.Cidade);

            if (!usuario.EhValido())
            {
                throw new ArgumentException("Dados obrigatórios não preenchidos");
            }

            var id = await _usuarioRepository
                                .InsertAsync(usuario)
                                .ConfigureAwait(false);

            return new UsuarioViewModel()
            {
                Id = id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataNascimento = usuario.DataNascimento,
                Genero = usuario.Genero,
                FotoPerfil = usuario.FotoPerfil,
                Cargo = usuario.Cargo,
                Cidade = usuario.Cidade
            };
        }
    }
}