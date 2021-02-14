using System;
using System.Collections.Generic;
using System.Text;

namespace CottonCandy.Application.AppUsuario.Input
{
    public class UsuarioInput
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public int GeneroId { get; set; }
        public string FotoPerfil { get; set; }
        public string Cargo { get; set; }
        public string Cidade { get; set; }
    }
}
