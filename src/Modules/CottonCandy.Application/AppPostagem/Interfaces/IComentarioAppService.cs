﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CottonCandy.Application.AppPostagem.Input;
using CottonCandy.Domain.Entities;

namespace CottonCandy.Application.AppPostagem.Interfaces
{
   
        public interface IComentarioAppService
        {
            Task<Comentario> InserirAsync(int idPostagem, ComentarioInput input); //ComentarioInput ainda nao existe
            Task<List<Comentario>> PegarComentariosPorIdPostagemAsync(int idPostagem);

        }
    
}
