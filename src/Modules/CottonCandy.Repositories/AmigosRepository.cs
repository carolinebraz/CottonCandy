using CottonCandy.Domain.Entities;
using CottonCandy.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
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

        public async Task<List<int>> GetListaAmigos(int idUsuarioSeguidor)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var SqlCmd = @$"SELECT 
                                      IdSeguido
                                FROM
                                      Amigos
                                WHERE
                                      IdSeguidor= '{idUsuarioSeguidor}'";

                using (var cmd = new SqlCommand(SqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);

                    List<int> listaIdAmigos = new List<int>();

                    while (reader.Read())
                    {
                        listaIdAmigos.Add(int.Parse(reader["IdSeguido"].ToString()));
                    }

                    return listaIdAmigos;
                }
            }

        }
    }
}
