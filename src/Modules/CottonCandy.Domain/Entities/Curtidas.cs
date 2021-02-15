using System;
using System.Collections.Generic;
using System.Text;

namespace CottonCandy.Domain.Entities
{
    public class Curtidas
    {
        public Curtidas(int usuarioId, int postagemId)
        {
            UsuarioId = usuarioId;
            PostagemId = postagemId;

        }

        public Curtidas(int id, int usuarioId, int postagemId)
        {
            Id = id;
            UsuarioId = usuarioId;
            PostagemId = postagemId;

        }

        public int Id { get; private set; }
        public int UsuarioId { get; private set; }
        public int PostagemId { get; private set; }
        public string Tipo { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }

    }
}
