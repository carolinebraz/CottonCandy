using CottonCandy.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CottonCandy.Domain.Core
{
    public class Logado : ILogado
    {
        private readonly IHttpContextAccessor _accessor;

        public Logado(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public int GetUsuarioLogadoId()
        {
            var id = _accessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "jti").Value;

            return int.Parse(id);
        }
    }
}
