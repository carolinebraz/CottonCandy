using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace CottonCandy.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly IConfiguration _configuration;

        public GeneroRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Genero> GetByIdAsync(int id)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var SqlCmd = @$"SELECT Id, 
                                       Descricao
                                FROM
                                      Genero
                                WHERE
                                       Id= '{id}'";
                using (var cmd = new SqlCommand(SqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    while (reader.Read())
                    {
                        var genero = new Genero(reader["Descricao"].ToString());
                        genero.SetId(int.Parse(reader["Id"].ToString()));

                        return genero;
                    }
                }

                return default;
            }
        }
    }
}
