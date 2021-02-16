using System;
using System.Collections.Generic;
using System.Text;

namespace CottonCandy.Application.AppUsuario.Output
{
    public class PostagemViewModel

    {
        public int Id { get; set; }
        public String NomeUsuario { get; set; }
        public String FotoUsuario { get; set; }
        public String TextoPost { get; set; }
        public String FotoPost { get; set; }
        public DateTime DataPostagem { get; set; }
    }
}
