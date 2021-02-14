using CottonCandy.Application.AppUser.Output;
using CottonCandy.Application.AppUsuario.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppUser.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<UsuarioViewModel> InsertAsync(UsuarioInput input);
        Task<UsuarioViewModel> GetByIdAsync(int id);
    }
}
