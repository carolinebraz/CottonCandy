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
    public class AmigosRepository : IAmigosRepository
    {
        private readonly IConfiguration _configuration;


        public AmigosRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<int> SeguirAsync(Amigos amigo)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @"INSERT INTO
                                 Amigos (IdSeguidor,
                                         IdSeguido,
                                         DataAmizade)
                                VALUES (@idSeguidor,
                                        @idSeguido,
                                        @dataAmizade); SELECT scope_identity();";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("idSeguidor", amigo.IdUsuarioSeguidor);
                    cmd.Parameters.AddWithValue("idSeguido", amigo.IdUsuarioSeguido);
                    cmd.Parameters.AddWithValue("dataAmizade", amigo.DataAmizade);

                    con.Open();
                    var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                    return int.Parse(id.ToString());
                    //validar 

                }
            }
        }

        public async Task<List<Amigos>> GetListaAmigos(int idUsuarioSeguidor)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var SqlCmd = @$"SELECT Id,
                                       IdSeguido,
                                       DataAmizade
                                FROM
                                      Amigos
                                WHERE
                                       IdSeguidor= '{idUsuarioSeguidor}'";

                using (var cmd = new SqlCommand(SqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);

                    List<Amigos> listaAmigos = new List<Amigos>();

                    while (reader.Read())
                    {
                        var amigo = new Amigos(idUsuarioSeguidor,
                                               int.Parse(reader["IdSeguido"].ToString()),
                                               DateTime.Parse(reader["DataAmizade"].ToString()),
                                               int.Parse(reader["Id"].ToString()));

                        listaAmigos.Add(amigo);
                    }

                    return listaAmigos;
                }
            }

        }
    }
}
