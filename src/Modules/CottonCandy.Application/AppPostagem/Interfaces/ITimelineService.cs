using CottonCandy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Application.AppPostagem.Interfaces
{
    interface ITimelineService
    {
        Task<List<Postagem>> GetByUserIdAsync();
        //AMIGOS???
    }
}
