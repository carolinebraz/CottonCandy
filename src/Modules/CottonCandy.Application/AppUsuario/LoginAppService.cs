using CottonCandy.Application.AppUsuario.Output;
using CottonCandy.Application.AppUsuario.Interfaces;
using CottonCandy.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUsuario
{
    public class LoginAppService : ILoginAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginAppService(IUsuarioRepository UsuarioRepository)
        {
            _usuarioRepository = UsuarioRepository;
        }
        public async Task<UsuarioViewModel> LoginAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository
                                    .ObterUsuarioPorLogin(email)
                                    .ConfigureAwait(false);

            if (usuario is null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            if (!usuario.SenhaEhIgual(senha))
            {
                throw new ArgumentException("Senha incorreta");
            }

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
    }
}