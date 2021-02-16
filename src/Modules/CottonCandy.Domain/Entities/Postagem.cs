using System;
using System.Collections.Generic;
using System.Text;

namespace CottonCandy.Domain.Entities
{
	public class Postagem
	{
		public Postagem(string texto, string fotoPost, int usuarioId)
		{
			Texto = texto;
			FotoPost = fotoPost;
			DataPostagem = DateTime.Now;
			UsuarioId = usuarioId;
		}

		public Postagem(string texto, string fotoPost, int usuarioId, DateTime dataPostagem)
		{
			Texto = texto;
			FotoPost = fotoPost;
			DataPostagem = dataPostagem;
			UsuarioId = usuarioId;
		}

		public int Id { get; private set; }
		public int UsuarioId { get; private set; }
		public string Texto { get; private set; }
		public string FotoPost { get; private set; }
		public DateTime DataPostagem { get; private set; }

        public void SetId(int id)
        {
			Id = id;
        }
    }
}
