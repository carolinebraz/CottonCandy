using System;
namespace CottonCandy.Domain.Entities
{
    public class Comentario
    {
        public Comentario(int idPostagem,
                          int idUsuario,
                          string texto)
        {
            IdPostagem = idPostagem;
            IdUsuario = idUsuario;
            Texto = texto;
            DataCriacao = DateTime.Now;
        }

        public Comentario(int id,
                          int idPostagem,
                          int idUsuario,
                          string texto,
                          DateTime dataCriacao)
        {
            Id = id;
            IdPostagem = idPostagem;
            IdUsuario = idUsuario;
            Texto = texto;
            DataCriacao = dataCriacao;
        }

        public int Id { get; private set; }
        public int IdPostagem { get; private set; }
        public int IdUsuario { get; private set; }
        public string Texto { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}