using System;

namespace CottonCandy.Application.AppUsuario.Output
{
    public class PostagemViewModel
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public string FotoUsuario { get; set; }
        public string TextoPost { get; set; }
        public string FotoPost { get; set; }
        public DateTime DataPostagem { get; set; }
    }
}