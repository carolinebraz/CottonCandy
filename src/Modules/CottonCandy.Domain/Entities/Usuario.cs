using System;
using System.Collections.Generic;
using CottonCandy.Domain.Core;

namespace CottonCandy.Domain.Entities
{
    public class Usuario
    {
        public Usuario(string nome,
                       string email,
                       string senha,
                       DateTime dataNascimento,
                       Genero genero,
                       string fotoPerfil,
                       string cargo,
                       string cidade,
                       string fotoCapa)
        {
            Nome = nome;
            Email = email;
            CriptografarSenha(senha);
            DataNascimento = dataNascimento;
            Genero = genero;
            FotoPerfil = fotoPerfil;
            Cargo = cargo;
            Cidade = cidade;
            FotoCapa = fotoCapa;
            Amigos = new List<Usuario>();
        }

        public Usuario(string nome,
                       string fotoPerfil)
        {
            Nome = nome;
            FotoPerfil = fotoPerfil;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public Genero Genero { get; private set; }
        public string FotoPerfil { get; private set; }
        public string Cargo { get; private set; }
        public string Cidade { get; private set; }
        public string FotoCapa { get; private set; }
        public List<Usuario> Amigos { get; private set; }

        public bool EhValido()
        {
            bool valido = true;
            if (string.IsNullOrEmpty(Nome) ||
                string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Senha) ||
                DataNascimento.ToShortDateString() == "01/01/0001" ||
                Genero.Id <= 0)
            {
                valido = false;
            }

            return valido;
        }
        private void CriptografarSenha(string senha)
        {
            Senha = PasswordHasher.Hash(senha);
        }

        public bool SenhaEhIgual(string senha)
        {
            return PasswordHasher.Verify(senha, Senha);
        }

        public void InformacaoLoginUsuario(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
