using System;

namespace CottonCandy.Domain.Entities
{
    public class Genero
    {
        public Genero(string descricao)
        {
            Descricao = descricao;
        }

        public Genero(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
        public int Id { get; private set; }
        public string Descricao { get; private set; }

        public bool EhValido()
        {
            bool valido = true;
            if(string.IsNullOrEmpty(Descricao))
            {
                valido = false;
            }

            return valido;
        }

        public void SetId(int id)
        {
            Id = id; ;
        }
    }
}