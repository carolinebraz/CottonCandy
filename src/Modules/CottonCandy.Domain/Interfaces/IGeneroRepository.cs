using CottonCandy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Domain.Interfaces
{
    public interface IGeneroRepository
    {
        Task<Genero> GetByIdAsync(int id);
    }
}
