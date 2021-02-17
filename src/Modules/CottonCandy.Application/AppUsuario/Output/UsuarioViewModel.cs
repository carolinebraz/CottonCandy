using CottonCandy.Domain.Entities;
using System;

namespace CottonCandy.Application.AppUsuario.Output
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public Genero Genero { get; set; }
        public string FotoPerfil { get; set; }
        public string Cargo { get; set; }
        public string Cidade { get; set; }
        public string FotoCapa { get; set; }
    }
}