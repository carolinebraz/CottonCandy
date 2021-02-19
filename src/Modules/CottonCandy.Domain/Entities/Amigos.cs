using System;

namespace CottonCandy.Domain.Entities
{
    public class Amigos
    {
        public Amigos(int idUsuarioSeguidor,
                      int idUsuarioSeguido)
        {
            IdUsuarioSeguidor = idUsuarioSeguidor;
            IdUsuarioSeguido = idUsuarioSeguido;
            DataAmizade = DateTime.Now;
        }

        public Amigos(int idUsuarioSeguidor,
                      int idUsuarioSeguido,
                      DateTime dataAmizade,
                      int id)
        {
            IdUsuarioSeguidor = idUsuarioSeguidor;
            IdUsuarioSeguido = idUsuarioSeguido;
            DataAmizade = dataAmizade;
            Id = id;
        }

        public Amigos(int idUsuarioSeguido, string nomeAmigo)
        {
            IdUsuarioSeguido = idUsuarioSeguido;
            NomeAmigo = nomeAmigo;
        }

        public int Id { get; private set; }
        public int IdUsuarioSeguidor { get; private set; }
        public int IdUsuarioSeguido { get; private set; }
        public String NomeAmigo { get; private set; }
        public DateTime DataAmizade { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}