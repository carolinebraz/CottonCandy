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

        public int Id { get; private set; }
        public int IdUsuarioSeguidor { get; private set; }
        public int IdUsuarioSeguido { get; private set; }
        public DateTime DataAmizade { get; private set; }
    }
}