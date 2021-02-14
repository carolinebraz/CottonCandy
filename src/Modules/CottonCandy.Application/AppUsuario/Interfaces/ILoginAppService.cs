using CottonCandy.Application.AppUser.Output;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUsuario.Interfaces
{
    public interface ILoginAppService
    {
        Task<UsuarioViewModel> LoginAsync(string email, string senha);
    }
}
